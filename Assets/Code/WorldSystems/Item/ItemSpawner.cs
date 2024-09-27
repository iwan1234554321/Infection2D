using Notteam.World;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemObject spawnedItem;

    public void Spawn()
    {
        World.Instance.GetSystem<PoolObjectSystem>().Create(spawnedItem.name, transform.position, transform.rotation);
    }
}
