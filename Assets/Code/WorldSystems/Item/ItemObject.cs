using Notteam.World;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PoolObject))]
public class ItemObject : WorldEntity
{
    [SerializeField] private float radius = 1;
    [SerializeField] private string tagObjectInteract;
    [SerializeField] private ItemLogic itemLogic;

    private GameObject _interactObject;

    private PoolObject _poolObject;

    protected override void OnStart()
    {
        _poolObject = GetComponent<PoolObject>();

        World.Instance.GetSystem<TagSystem>().TryGetObject(tagObjectInteract, out _interactObject);
    }

    protected override void OnUpdate()
    {
        if (_poolObject.IsUsed)
        {
            var directionToInteractor = _interactObject.transform.position - transform.position;

            if (directionToInteractor.magnitude <= radius)
            {
                if (itemLogic)
                {
                    itemLogic.Execute(_interactObject);

                    _poolObject.Destroy();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
