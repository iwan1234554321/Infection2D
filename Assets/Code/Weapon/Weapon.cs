using Notteam.World;
using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;

    private int _bullets;

    private bool _isFireStarted;

    private WeaponData _weaponData;

    private SpriteAnimator _spriteAnimator;

    public event Action<int> onUpdateBulletCount;

    public int Bullets => _bullets;

    private void Awake()
    {
        _spriteAnimator = GetComponentInChildren<SpriteAnimator>();

        StopAnimation();
    }

    private void StartAnimation()
    {
        _spriteAnimator.enabled = true;
        _spriteAnimator.SpriteRenderer.enabled = true;
    }

    private void StopAnimation()
    {
        _spriteAnimator.enabled = false;
        _spriteAnimator.SpriteRenderer.enabled = false;
    }

    private void Fire()
    {
        StartAnimation();

        var instance = World.Instance.GetSystem<PoolObjectSystem>().Create<WeaponBullet>(_weaponData.Bullet.name, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        if (instance)
            instance.AddForce(bulletSpawnPoint.right * _weaponData.BulletForce);

        World.Instance.GetSystem<PoolObjectSystem>().Create(_weaponData.FireSoundName, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        _bullets--;

        onUpdateBulletCount?.Invoke(_bullets);

        World.Instance.GetSystem<CameraSystem>().Shake();
    }

    private IEnumerator FireCycle(Action fireBreak)
    {
        while (_isFireStarted)
        {
            if (!_isFireStarted || _bullets <= 0)
            {
                FireFinal();

                fireBreak?.Invoke();

                break;
            }

            Fire();

            yield return new WaitForSeconds(_weaponData.FireTime);
        }
    }

    public void FireStart(Action fireBreak)
    {
        if (!_isFireStarted && _bullets > 0)
        {
            StopAllCoroutines();

            _isFireStarted = true;

            StartCoroutine(FireCycle(fireBreak));
        }
    }

    public void FireFinal()
    {
        if (_isFireStarted)
        {
            StopAnimation();

            _isFireStarted = false;
        }
    }

    public void Setup(WeaponData data)
    {
        _weaponData = data;

        _bullets = data.Bullets;
    }

    public void AddBullets(int count)
    {
        _bullets += count;

        onUpdateBulletCount?.Invoke(_bullets);
    }
}
