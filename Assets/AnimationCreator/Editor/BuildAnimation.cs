using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;
using System;

public class BuildAnimation : EditorWindow {

    private static readonly string resPath = "Assets/AnimationRes/BuildAnimation"; // 美术给的原始图片路径
    private static readonly string animationPath = "Assets/AnimationCreate/Animation"; // 生成的Animation的路径
    private static readonly string animationControllerPath = "Assets/AnimationCreate/AnimationController"; // 生成的AnimationController的路径
    private static readonly string prefabPath = "Assets/AnimationCreate/Prefabs"; // 生成的Prefab的路径
    private float frameRate = 60; // 1秒有多少帧
    private int frame = 1; // 每张图持续多少帧

    public static void Init() {
        BuildAnimation window = (BuildAnimation)GetWindow(typeof(BuildAnimation), true, "BuildAnimation");
        window.Show();
    }

    private void OnGUI() {
        GUILayout.Label("美术资源存放方式：");
        GUILayout.Label(resPath);

        GUILayout.Space(10);
        if (GUILayout.Button("生成动画", GUILayout.Height(50))) {
            BuildAniamtions();
        }
    }

    private void BuildAniamtions() {
        if (!Directory.Exists(resPath)) {
            Debug.LogError("请创建资源文件夹,并按照格式放入动画资源!!!");
            return;
        }
        var raw = new DirectoryInfo(resPath);
        foreach (DirectoryInfo dictorys in raw.GetDirectories()) {
            if (PrefabContain(dictorys.Name + ".prefab")) {
                Debug.Log("contain + " + dictorys.Name + ", 所以不创建此动画prefab");
                continue;
            }
            BuildCompleteAnimation(dictorys);
        }
    }

    private bool PrefabContain(string name) {
        if (!Directory.Exists(prefabPath)) {
            return false;
        }
        var prefabDirectoryInfo = new DirectoryInfo(prefabPath);
        FileInfo[] fileInfos = prefabDirectoryInfo.GetFiles("*.prefab");
        return fileInfos.Any(t => t.Name == name);
    }

    private void BuildCompleteAnimation(DirectoryInfo dictorys) {
        var clips = new List<AnimationClip>();
        foreach (DirectoryInfo dictoryAnimations in dictorys.GetDirectories()) {
            clips.Add(BuildAnimationClip(dictoryAnimations));
        }
        AnimatorController controller = BuildAnimationController(clips, dictorys.Name);
        BuildPrefab(dictorys, controller);
        AddEvent(clips);
    }

    private AnimationClip BuildAnimationClip(DirectoryInfo dictorys) {
        frameRate = Convert.ToInt32(dictorys.Name.Split('_')[1].Replace("fps", ""));
        AnimationClip clip = GetAnimationClip();
        SetObjectReferenceCurve(dictorys, clip, dictorys.Name);
        string parentName = Directory.GetParent(dictorys.FullName).Name;
        Directory.CreateDirectory(animationPath + "/" + parentName);
        AssetDatabase.CreateAsset(clip, animationPath + "/" + parentName + "/" + dictorys.Name + ".anim");
        AssetDatabase.SaveAssets();
        return clip;
    }

    private AnimationClip GetAnimationClip() {
        var clip = new AnimationClip {
            frameRate = frameRate
        };
        LoopClip(clip);
        return clip;
    }

    private void LoopClip(AnimationClip clip) {
        AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
        clipSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(clip, clipSettings);
    }

    private void SetObjectReferenceCurve(DirectoryInfo directoryInfo, AnimationClip clip, string clipName) {
        FileInfo[] images = directoryInfo.GetFiles("*.png");
        Array.Sort(images, CompareFileInfo);
        var curveBinding = new EditorCurveBinding {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };
        ObjectReferenceKeyframe[] keyFrames = GetKeyFrames(images);
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
    }

    private int CompareFileInfo(FileInfo a, FileInfo b) {
        int aNum = GetNumberInString(a.Name);
        int bNum = GetNumberInString(b.Name);
        return aNum < bNum ? -1 : 1;
    }

    private int GetNumberInString(string target) {
        int firstNumberIndex = -1;
        int numberTailIndex = -1;
        for (int i = 0; i < target.Length; i++) {
            string s = target[i].ToString();
            if (firstNumberIndex == -1 && int.TryParse(s, out _)) {
                firstNumberIndex = i;
            }
            if (firstNumberIndex != -1 && !int.TryParse(s, out _)) {
                numberTailIndex = i;
                break;
            }
        }
        return Convert.ToInt32(target.Substring(firstNumberIndex, numberTailIndex - firstNumberIndex));
    }

    private ObjectReferenceKeyframe[] GetKeyFrames(FileInfo[] images) {
        var keyFrames = new ObjectReferenceKeyframe[images.Length];
        float frameTime = 1f * frame / frameRate;
        for (int i = 0; i < images.Length; i++) {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images[i].FullName));
            keyFrames[i] = new ObjectReferenceKeyframe {
                time = frameTime * i,
                value = sprite
            };
        }
        return keyFrames;
    }

    private string DataPathToAssetPath(string path) {
        return
            Application.platform == RuntimePlatform.WindowsEditor ? path.Substring(path.IndexOf("Assets\\"))
            : path.Substring(path.IndexOf("Assets/"));
    }

    private AnimatorController BuildAnimationController(List<AnimationClip> clips, string name) {
        Directory.CreateDirectory(animationControllerPath);
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(animationControllerPath + "/" + name + ".controller");
        animatorController.AddParameter("AnimationType", AnimatorControllerParameterType.Int);
        AnimatorControllerLayer layer = animatorController.layers[0];
        AnimatorStateMachine sm = layer.stateMachine;
        foreach (AnimationClip currentClip in clips) {
            AnimatorState state = sm.AddState(currentClip.name);
            state.motion = currentClip;
            if (sm.defaultState == null) {
                sm.defaultState = state;
            }
            AnimatorStateTransition ast = sm.AddAnyStateTransition(state);
            ast.hasExitTime = false;
            ast.canTransitionToSelf = false;
            ast.duration = 0f;
            ast.AddCondition(AnimatorConditionMode.Equals, -1, "AnimationType");
        }
        AssetDatabase.SaveAssets();
        return animatorController;
    }

    private void BuildPrefab(DirectoryInfo dictorys, AnimatorController animatorCountorller) {
        FileInfo[] images = dictorys.GetDirectories().FirstOrDefault().GetFiles("*.png");
        Array.Sort(images, CompareFileInfo);
        var go = new GameObject(dictorys.Name);
        go.AddComponent<UnitAnimation>();
        SpriteRenderer spriteRender = go.AddComponent<SpriteRenderer>();
        Debug.Log("prefab默认图片 = " + DataPathToAssetPath(images[0].FullName));
        spriteRender.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images[0].FullName));
        Animator animator = go.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorCountorller;
        Directory.CreateDirectory(prefabPath);
        PrefabUtility.SaveAsPrefabAsset(go, prefabPath + "/" + go.name + ".prefab");
        DestroyImmediate(go);
    }

    private void AddEvent(List<AnimationClip> clips) {
        for (int i = 0; i < clips.Count; i++) {
            AnimationClip clip = clips[i];
            EditorCurveBinding[] bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, bindings[0]);
            //if (!IsLoop(clip.name)) {
            //    var startEvent = new AnimationEvent {
            //        functionName = "StartAnimation",
            //        time = 0f
            //    };
            //    var attackEvent = new AnimationEvent {
            //        functionName = "AttackFrame",
            //        time = keyframes[keyframes.Length / 2].time
            //    };
            //    var endEvent = new AnimationEvent {
            //        functionName = "EndAnimation",
            //        time = keyframes[keyframes.Length - 1].time
            //    };
            //    AnimationUtility.SetAnimationEvents(clip, new AnimationEvent[] { startEvent, attackEvent, endEvent });
            //}
        }
    }

}
