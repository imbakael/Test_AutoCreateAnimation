using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MenuItems {
    [MenuItem("Tools/Build Animation")]
    public static void CreateAnimation() {
        BuildAnimation.Init();
    }
}
