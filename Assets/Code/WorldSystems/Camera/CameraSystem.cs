using Notteam.World;
using UnityEngine;

public class CameraSystem : WorldSystem<CameraController>
{
    private CameraController _camera;

    protected override void OnStart()
    {
        _camera = entities[0];
    }

    public void Shake()
    {
        _camera.Shake();
    }
}
