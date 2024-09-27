using Notteam.World;
using UnityEngine;

public class ItemAddBullets
{
    private GameObject _gameObject;
    private int        _countBullets;

    public ItemAddBullets(GameObject gameObject, int countBullets)
    {
        _gameObject   = gameObject;
        _countBullets = countBullets;
    }

    public GameObject GameObject   => _gameObject;
    public int        CountBullets => _countBullets;
}

[CreateAssetMenu(fileName = "ItemBulletsData", menuName = "Notteam/Item/Create ItemBullets", order = 0)]
public class ItemBullets : ItemLogic
{
    [SerializeField] private int minBullets;
    [SerializeField] private int maxBullets;

    private int _countBullets;

    internal override void Execute(GameObject interactObject)
    {
        _countBullets = Random.Range(minBullets, maxBullets);

        World.Instance.GetSystem<EventDispatcherSystem>().InvokeEvent(new ItemAddBullets(interactObject, _countBullets));

        World.Instance.GetSystem<PoolObjectSystem>().Create(executeSound, interactObject.transform.position, interactObject.transform.rotation);
    }
}
