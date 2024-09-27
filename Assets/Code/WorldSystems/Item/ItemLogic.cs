using UnityEngine;

public abstract class ItemLogic : ScriptableObject
{
    [SerializeField] protected string executeSound;

    internal abstract void Execute(GameObject interactObject);
}
