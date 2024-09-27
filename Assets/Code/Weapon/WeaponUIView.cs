using Notteam.World;
using TMPro;
using UnityEngine;

public class WeaponUIView : View
{
    [SerializeField] private string weaponTag;
    [SerializeField] private TMP_Text bulletCountText;

    private Weapon _weapon;

    protected override void OnStart()
    {
        if (World.Instance.GetSystem<TagSystem>().TryGetObject(weaponTag, out var weaponObject))
        {
            _weapon = weaponObject.GetComponent<Weapon>();
        }

        _weapon.onUpdateBulletCount += Weapon_onUpdateBulletCount;
    }

    protected override void OnFinal()
    {
        _weapon.onUpdateBulletCount -= Weapon_onUpdateBulletCount;
    }

    private void Weapon_onUpdateBulletCount(int bullets)
    {
        bulletCountText.text = bullets.ToString();
    }
}
