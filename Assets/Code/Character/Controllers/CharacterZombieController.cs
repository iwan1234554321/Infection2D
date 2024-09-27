using Notteam.World;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
[RequireComponent(typeof(ItemSpawner))]
public class CharacterZombieController : CharacterControll
{
    [SerializeField] private int damage = 1000;

    private const string PlayerTag = "Player";

    private Transform _playerTarget;

    private PoolObject  _poolObject;
    private ItemSpawner _itemSpawner;

    protected override void OnStart()
    {
        base.OnStart();

        _poolObject  = GetComponent<PoolObject>();
        _itemSpawner = GetComponent<ItemSpawner>();

        _poolObject.onCreateObject += OnCreateObject;
        _poolObject.onDestroyObject += OnDestroyObject;

        characterHealth.onDead += CharacterHealth_onDead;

        characterHealth.onRemoveHealth += CharacterHealth_onRemoveHealth;
    }

    private void CharacterHealth_onRemoveHealth(CharacterRemoveHealth @event)
    {
        if (@event.Ñontact.HasValue)
        {
            World.Instance.GetSystem<PoolObjectSystem>().Create(
                settings.BloodParticle,
                @event.Ñontact.Value.point,
                Quaternion.LookRotation(@event.Ñontact.Value.normal));
        }
    }

    private void CharacterHealth_onDead()
    {
        _itemSpawner.Spawn();

        _poolObject.Destroy();
    }

    protected override void OnUpdate()
    {
        if (_playerTarget)
        {
            var directionToPlayer = _playerTarget.position - transform.position;

            var dotDirection = Vector3.Dot(Vector3.right, directionToPlayer.normalized);

            characterMover.SetDirection(dotDirection * settings.MoveSpeed);
            characterRotator.SetDirection(dotDirection);
        }
    }

    protected override void OnFinal()
    {
        _poolObject.onCreateObject -= OnCreateObject;
        _poolObject.onDestroyObject -= OnDestroyObject;

        characterHealth.onDead -= CharacterHealth_onDead;

        characterHealth.onRemoveHealth -= CharacterHealth_onRemoveHealth;
    }

    private void OnCreateObject()
    {
        if (World.Instance.GetSystem<TagSystem>().TryGetObject(PlayerTag, out var gameObject))
        {
            _playerTarget = gameObject.transform;

            spriteAnimator.SetAnimation(settings.WalkAnimationIndex);

            Debug.Log($"READY ZOMBIE");
        }
    }

    private void OnDestroyObject()
    {
        _playerTarget = null;

        spriteAnimator.SetAnimation(settings.IdleAnimationIndex);

        Debug.Log($"DEAD ZOMBIE");

        World.Instance.GetSystem<PoolObjectSystem>().Create(settings.DeadSound, transform.position, transform.rotation);

        characterHealth.ResetHealthValues();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _playerTarget.gameObject)
        {
            World.Instance.GetSystem<EventDispatcherSystem>().InvokeEvent(new CharacterRemoveHealth(collision.gameObject, damage));
        }
    }
}
