using Notteam.World;
using System.Collections.Generic;
using UnityEngine;

public class TweenSystem : WorldSystem
{
    private Dictionary<string, Tween> _tweens = new Dictionary<string, Tween>();

    private List<Tween> _tweensTrash = new List<Tween>();

    private void ClearCompletedTweens()
    {
        foreach (var tweenTrash in _tweensTrash)
        {
            _tweens.Remove(tweenTrash.ID);
        }

        _tweensTrash.Clear();
    }

    protected override void OnUpdate()
    {
        ClearCompletedTweens();

        foreach (var tween in _tweens)
        {
            if (!tween.Value.IsCompleted)
            {
                tween.Value.UpdateTween(Time.unscaledDeltaTime);
            }
            else
            {
                _tweensTrash.Add(tween.Value);
            }
        }
    }

    public Tween CreateTween(Tween tween)
    {
        if (_tweens.TryGetValue(tween.ID, out var existTween))
        {
            if (existTween.IsInterrupted)
            {
                _tweensTrash.Add(existTween);

                ClearCompletedTweens();

                _tweens.Add(tween.ID, tween);
            }
            else
                return existTween;
        }
        else
            _tweens.Add(tween.ID, tween);

        return _tweens[tween.ID];
    }
}
