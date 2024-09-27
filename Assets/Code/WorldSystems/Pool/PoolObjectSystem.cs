using Notteam.World;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectSystem : WorldSystem
{
    [SerializeField] private PoolObjectData[] poolObjectsData;

    private Dictionary<string, List<PoolObject>> _objects = new Dictionary<string, List<PoolObject>>();

    protected override void OnStart()
    {
        foreach (var data in poolObjectsData)
        {
            var poolParent = new GameObject($"PoolParent : {data.PoolObject}");

            poolParent.transform.SetParent(transform);

            var objects = new List<PoolObject>();

            for (var i = 0; i < data.MaxCount; i++)
            {
                var instance = Instantiate(data.PoolObject, poolParent.transform);

                instance.Initialize(poolParent.transform);

                objects.Add(instance);
            }

            _objects.Add(data.PoolObject.name, objects);
        }
    }

    public T Create<T>(string name, Vector3 position, Quaternion rotation) where T : PoolObject
    {
        if (_objects.TryGetValue(name, out var poolObjects))
        {
            foreach (var poolObject in poolObjects)
            {
                if (!poolObject.IsUsed)
                {
                    return poolObject.Create(position, rotation) as T;
                }
            }
        }

        return null;
    }

    public PoolObject Create(string name, Vector3 position, Quaternion rotation)
    {
        return Create<PoolObject>(name, position, rotation);
    }
}
