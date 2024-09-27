using Notteam.World;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
[RequireComponent(typeof(AudioSource))]
public class SoundObject : WorldEntity
{
    [SerializeField] private float lifeTime = 3;

    private PoolObject  _poolObject;
    private AudioSource _audioSource;

    private IEnumerator LifeCycle()
    {
        while (_poolObject.IsUsed)
        {
            yield return new WaitForSeconds(lifeTime);

            _poolObject.Destroy();
        }
    }

    protected override void OnStart()
    {
        _poolObject  = GetComponent<PoolObject>();
        _audioSource = GetComponent<AudioSource>();

        _poolObject.onCreateObject += PoolObject_onCreateObject;
        _poolObject.onDestroyObject += PoolObject_onDestroyObject;
    }

    protected override void OnFinal()
    {
        _poolObject.onCreateObject -= PoolObject_onCreateObject;
        _poolObject.onDestroyObject -= PoolObject_onDestroyObject;
    }

    private void PoolObject_onCreateObject()
    {
        _audioSource.Play();

        if (lifeTime > 0.0f)
            StartCoroutine(LifeCycle());
    }

    private void PoolObject_onDestroyObject()
    {
        _audioSource.Stop();
    }
}
