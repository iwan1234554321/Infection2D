using Notteam.World;
using UnityEngine;

public class SpawnPoint : WorldEntity
{
    public Vector3    Position => transform.position;
    public Quaternion Rotation => transform.rotation;
}
