using Notteam.World;
using UnityEngine;

public static class TweenUtils
{
    public static Tween CreateTween(this GameObject gameObject, Tween tween)
    {
        tween.AdditiveIDByGameObjectInstance(gameObject);

        return World.Instance.GetSystem<TweenSystem>().CreateTween(tween);
    }
}
