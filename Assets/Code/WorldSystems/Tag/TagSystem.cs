using Notteam.World;
using System.Collections.Generic;
using UnityEngine;

public class TagSystem : WorldSystem<TagObject>
{
    private Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();

    protected override void OnStart()
    {
        foreach (var entity in entities)
            gameObjects.Add(entity.Tag, entity.gameObject);
    }

    public bool TryGetObject(string tag, out GameObject gameObject)
    {
        if (gameObjects.TryGetValue(tag, out gameObject))
            return true;

        return false;
    }
}
