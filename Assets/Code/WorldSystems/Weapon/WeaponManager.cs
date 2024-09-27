using Notteam.World;
using System;
using UnityEngine;

public class WeaponManager : WorldEntity
{
    [SerializeField] private int          index;
    [SerializeField] private WeaponData[] weapons;

    private WeaponData _currentWeapon;

    private Weapon _weapon;

    public Weapon Weapon => _weapon;

    protected override void OnStart()
    {
        _weapon = GetComponentInChildren<Weapon>();

        SetCurrentWeapon(weapons[index]);

        World.Instance.GetSystem<EventDispatcherSystem>().SubscribeToEvent<ItemAddBullets>(AddBullets);
    }

    protected override void OnFinal()
    {
        World.Instance.GetSystem<EventDispatcherSystem>().UnSubscribeFromEvent<ItemAddBullets>(AddBullets);
    }

    private void AddBullets(ItemAddBullets @event)
    {
        if (@event.GameObject == gameObject)
        {
            _weapon.AddBullets(@event.CountBullets);
        }
    }

    private void SetCurrentWeapon(WeaponData data)
    {
        _currentWeapon = data;

        _weapon.Setup(_currentWeapon);
    }

    public void FireStart(Action fireBreak) => _weapon.FireStart(fireBreak);

    public void FireFinal() => _weapon.FireFinal();
}
