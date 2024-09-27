using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    protected SpriteAnimator spriteAnimator;

    private void Awake()
    {
        spriteAnimator = GetComponentInChildren<SpriteAnimator>();
    }
}
