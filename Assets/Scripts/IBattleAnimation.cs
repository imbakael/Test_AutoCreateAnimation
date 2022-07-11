using System;

public interface IBattleAnimation
{
    event Action OnAttackFrame;

    bool IsPlay { get; set; }

    void SetAnimation(int x, int y, bool isActive, AnimationType animationType);

#region ¶¯»­ÊÂ¼þ
    void StartAnimation();

    void AttackFrame();

    void EndAnimation();
#endregion
}
