using System;
using UnityEngine;

public class Tween
{
    private string _id;

    private float _time;
    private float _timeline;

    private bool _isInterrupted;
    private bool _isCompleted;

    private Action        _onStart;
    private Action<float> _onUpdate;
    private Action        _onFinal;

    public Tween(string id, float time, Action onStart = null, Action<float> onUpdate = null, Action onFinal = null)
    {
        _id   = id;
        _time = time;

        _onStart  = onStart;
        _onUpdate = onUpdate;
        _onFinal  = onFinal;
    }

    public Tween(string id, float time, bool isInterrupted, Action onStart = null, Action<float> onUpdate = null, Action onFinal = null)
    {
        _id            = id;
        _time          = time;
        _isInterrupted = isInterrupted;

        _onStart  = onStart;
        _onUpdate = onUpdate;
        _onFinal  = onFinal;
    }

    public string ID => _id;

    public bool   IsInterrupted => _isInterrupted;
    public bool   IsCompleted   => _isCompleted;

    internal void UpdateTween(float deltaTime)
    {
        if (!_isCompleted)
        {
            if (_timeline <= 0.0f)
            {
                _onStart?.Invoke();
            }

            _onUpdate?.Invoke(_timeline);

            _timeline += (1 / _time) * deltaTime;

            if (_timeline >= 1.0f)
            {
                _onUpdate?.Invoke(_timeline);

                _onFinal?.Invoke();

                _isCompleted = true;
            }
        }
    }

    internal void AdditiveIDByGameObjectInstance(GameObject gameObject)
    {
        _id += gameObject.GetInstanceID();
    }
}
