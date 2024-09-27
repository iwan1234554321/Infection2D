using Notteam.World;
using System;
using UnityEngine;

public class CharacterHealth : WorldEntity
{
    [SerializeField] private bool  useDamageHighlight;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float damageHighlightTime = 0.25f;

    private int _health;
    private int _maxHealth;

    private SpriteRenderer    _spriteRenderer;
    private CharacterControll _characterControll;

    public event Action<int> onUpdateHealth;
    public event Action onDead;
    public event Action<CharacterAddHealth>    onAddHealth;
    public event Action<CharacterRemoveHealth> onRemoveHealth;

    public int Health    => _health;
    public int MaxHealth => _maxHealth;

    private void AddHealth(CharacterAddHealth @event)
    {
        if (@event.Character == gameObject)
        {
            _health += @event.AddHealth;

            onUpdateHealth?.Invoke(_health);

            onAddHealth?.Invoke(@event);
        }
    }

    private void RemoveHealth(CharacterRemoveHealth @event)
    {
        if (@event.Character == gameObject)
        {
            _health -= @event.RemoveHealth;

            onUpdateHealth?.Invoke(_health);

            onRemoveHealth?.Invoke(@event);

            if (_health <= 0.0f)
                onDead?.Invoke();

            if (useDamageHighlight)
            {
                var startColor = Color.white;

                gameObject.CreateTween(new Tween("DamageHighlight", damageHighlightTime, true,
                    onUpdate: (t) =>
                    {
                        var startLerp = Mathf.InverseLerp(0, 0.5f, t);
                        var finalLerp = Mathf.InverseLerp(0.5f, 1.0f, t);

                        var pingPongLerp = t - (1.0f * finalLerp);

                        _spriteRenderer.color = Color.Lerp(startColor, damageColor, pingPongLerp);
                    }));
            }
        }
    }

    protected override void OnStart()
    {
        _spriteRenderer    = GetComponentInChildren<SpriteRenderer>();
        _characterControll = GetComponent<CharacterControll>();

        ResetHealthValues();

        World.Instance.GetSystem<EventDispatcherSystem>().SubscribeToEvent<CharacterAddHealth>(AddHealth);
        World.Instance.GetSystem<EventDispatcherSystem>().SubscribeToEvent<CharacterRemoveHealth>(RemoveHealth);
    }

    protected override void OnFinal()
    {
        World.Instance.GetSystem<EventDispatcherSystem>().UnSubscribeFromEvent<CharacterAddHealth>(AddHealth);
        World.Instance.GetSystem<EventDispatcherSystem>().UnSubscribeFromEvent<CharacterRemoveHealth>(RemoveHealth);
    }

    public void ResetHealthValues()
    {
        _maxHealth = _characterControll.Settings.Health;
        _health    = _maxHealth;

        onUpdateHealth?.Invoke(_health);
    }
}
