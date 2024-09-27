using Notteam.World;
using UnityEngine;

public class TagObject : WorldEntity
{
    [SerializeField] private new string tag;

    public string Tag => tag;
}
