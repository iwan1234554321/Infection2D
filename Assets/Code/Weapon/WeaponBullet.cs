using Notteam.World;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponBullet : PoolObject
{
    [SerializeField] private float lifeTime = 5.0f;
    [SerializeField] private int   damage = 15;

    private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;

    protected override void OnInitialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Disable();
    }

    protected override void OnCreateObject()
    {
        Activate();

        StartCoroutine(LifeTimer());
    }

    protected override void OnDestroyObject()
    {
        Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        World.Instance.GetSystem<EventDispatcherSystem>().InvokeEvent(new CharacterRemoveHealth(collision.gameObject, damage, collision.GetContact(0)));

        Destroy();
    }

    private void Activate() => _rigidbody.isKinematic = false;
    private void Disable() => _rigidbody.isKinematic = true;

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy();
    }

    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction);
    }
}
