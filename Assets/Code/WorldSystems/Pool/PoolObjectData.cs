using UnityEngine;

[CreateAssetMenu(fileName = "PoolObjectData", menuName = "Notteam/Create PoolObjectData", order = 0)]
public class PoolObjectData : ScriptableObject
{
    [SerializeField] private PoolObject poolObject;
    [SerializeField] private int        maxCount;

    public PoolObject PoolObject => poolObject;
    public int        MaxCount   => maxCount;
}
