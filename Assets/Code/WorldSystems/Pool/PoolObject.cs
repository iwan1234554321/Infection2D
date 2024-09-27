using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Transform _parent;

    private bool _isUsed;

    private bool _isInitialized;

    public bool IsUsed        => _isUsed;
    public bool IsInitialized => _isInitialized;

    public event Action onInitialize;
    public event Action onCreateObject;
    public event Action onDestroyObject;

    protected virtual void OnInitialize() { }
    protected virtual void OnCreateObject() { }
    protected virtual void OnDestroyObject() { }

    public void Initialize(Transform parent)
    {
        _parent = parent;

        SetActiveByUsed();

        OnInitialize();

        onInitialize?.Invoke();

        _isInitialized = true;
    }

    private void SetActiveByUsed()
    {
        gameObject.SetActive(_isUsed);
    }

    public PoolObject Create(Vector3 position, Quaternion rotation)
    {
        _isUsed = true;

        transform.SetParent(null);
        transform.SetPositionAndRotation(position, rotation);

        SetActiveByUsed();

        OnCreateObject();

        onCreateObject?.Invoke();

        return this;
    }

    public void Destroy()
    {
        _isUsed = false;

        if (_isInitialized)
        {
            transform.SetParent(_parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            SetActiveByUsed();
        }

        OnDestroyObject();

        onDestroyObject?.Invoke();

        if (!_isInitialized)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
