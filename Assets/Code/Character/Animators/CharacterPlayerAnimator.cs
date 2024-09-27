using UnityEngine;

public class CharacterPlayerAnimator : CharacterAnimator
{
    [SerializeField] private int idleAnimationIndex;
    [SerializeField] private int runAnimationIndex;
    [SerializeField] private int fireAnimationIndex;

    public void SetIdle()
    {
        spriteAnimator.SetAnimation(idleAnimationIndex);
    }

    public void SetRun()
    {
        spriteAnimator.SetAnimation(runAnimationIndex);
    }

    public void SetFire()
    {
        spriteAnimator.SetAnimation(fireAnimationIndex);
    }
}
