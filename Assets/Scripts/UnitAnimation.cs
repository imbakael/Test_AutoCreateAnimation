using System;
using UnityEngine;

public enum AnimationType {
    Idle, // ����
    Move, // �ƶ�
    Attack, // �չ�
    Crit // ��ɱ
}

public class UnitAnimation : MonoBehaviour, IBattleAnimation {

    public event Action OnAttackFrame;

    public bool IsPlay { get; set; } = false;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation() {
        IsPlay = true;
    }

    public void AttackFrame() {
        OnAttackFrame?.Invoke();
    }

    public void EndAnimation() {
        IsPlay = false;
    }

    public void SetAnimation(int x, int y, bool isActive, AnimationType animationType) {
        animator.SetInteger("AnimationType", (int)animationType);
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
        animator.SetBool("IsActive", isActive);
    }

}
