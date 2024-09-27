using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleObject : PoolObject
{
    [SerializeField] private float lifeTime = 2;

    private ParticleSystem _particle;

    protected override void OnInitialize()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy();
    }

    protected override void OnCreateObject()
    {
        _particle.Play();

        StartCoroutine(LifeCycle());
    }

    protected override void OnDestroyObject()
    {
        _particle.Stop();
    }
}
