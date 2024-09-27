using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Notteam/Create WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float fireTime = 0.5f;
    [SerializeField] private float bulletForce = 2000.0f;
    [SerializeField] private int   bullets = 40;

    [Header("Audio")]
    [SerializeField] private string fireSoundName;

    [Header("References")]
    [SerializeField] private WeaponBullet bullet;

    public float        FireTime    => fireTime;
    public float        BulletForce => bulletForce;
    public int          Bullets     => bullets;

    public string       FireSoundName => fireSoundName;

    public WeaponBullet Bullet      => bullet;
}
