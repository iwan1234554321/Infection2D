using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteAnimationData[] spriteAnimations;

    private float _animationSpeed = 1.0f;

    private SpriteAnimationData _currentSpriteAnimation;

    private float _currentAnimationTime;
    private int   _currentAnimationFrame;

    private SpriteRenderer _spriteRenderer;

    public float AnimationSpeed { get => _animationSpeed; set => _animationSpeed = Mathf.Clamp01(value); }

    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetAnimation(0);
    }

    private void UpdateTime()
    {
        _currentAnimationTime += _currentSpriteAnimation.Speed * Time.fixedDeltaTime * _animationSpeed;

        if (_currentAnimationTime > _currentSpriteAnimation.Sprites.Length)
            _currentAnimationTime = 0.0f;
        else if (_currentAnimationTime < 0)
            _currentAnimationTime = _currentSpriteAnimation.Sprites.Length;

        _currentAnimationFrame = Mathf.FloorToInt(_currentAnimationTime);
    }

    private void FixedUpdate()
    {
        UpdateTime();

        _spriteRenderer.sprite = _currentSpriteAnimation.Sprites[_currentAnimationFrame];
    }

    public void SetAnimation(int index)
    {
        _currentSpriteAnimation = spriteAnimations[index];
    }
}
