using Notteam.World;
using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
public class CharacterPlayerController : CharacterControll
{
    [SerializeField] private int fireAnimationIndex;

    private bool _isFire;
    private bool _isMoved;

    private float _rememberedAnimationSpeed;

    private WeaponManager _weaponManager;

    protected override void OnStart()
    {
        base.OnStart();

        _weaponManager = GetComponent<WeaponManager>();

        _weaponManager.Weapon.onUpdateBulletCount += Weapon_onUpdateBulletCount;

        characterHealth.onDead += CharacterHealth_onDead;
    }

    private void Weapon_onUpdateBulletCount(int count)
    {
        if (count == 0)
            CharacterHealth_onDead();
    }

    protected override void OnFinal()
    {
        _weaponManager.Weapon.onUpdateBulletCount -= Weapon_onUpdateBulletCount;

        characterHealth.onDead -= CharacterHealth_onDead;
    }

    private void CharacterHealth_onDead()
    {
        World.Instance.GetSystem<PoolObjectSystem>().Create(settings.DeadSound, transform.position, transform.rotation);

        World.Instance.GetSystem<EventDispatcherSystem>().InvokeEvent(new GameOverEvent());
    }

    public void FireStart()
    {
        if (_weaponManager.Weapon.Bullets > 0)
        {
            _isFire = true;

            if (!_isMoved)
                spriteAnimator.SetAnimation(fireAnimationIndex);

            _weaponManager.FireStart(() =>
            {
                FireFinal();
            });
        }
    }

    public void FireFinal()
    {
        _weaponManager.FireFinal();

        if (!_isMoved)
            spriteAnimator.SetAnimation(settings.IdleAnimationIndex);

        _isFire = false;
    }

    public void MoveStart(float input)
    {
        _isMoved = true;

        _rememberedAnimationSpeed = spriteAnimator.AnimationSpeed;

        spriteAnimator.SetAnimation(settings.WalkAnimationIndex);

        Move(input);
    }

    public void Move(float input)
    {
        characterMover.SetDirection(input * settings.MoveSpeed);

        if (Mathf.Abs(input) > 0.0f)
            characterRotator.SetDirection(input);

        spriteAnimator.AnimationSpeed = Mathf.Abs(input);
    }

    public void MoveFinal(float input)
    {
        Move(input);

        spriteAnimator.AnimationSpeed = _rememberedAnimationSpeed;

        if (!_isFire)
            spriteAnimator.SetAnimation(settings.IdleAnimationIndex);
        else
            spriteAnimator.SetAnimation(fireAnimationIndex);

        _isMoved = false;
    }
}
