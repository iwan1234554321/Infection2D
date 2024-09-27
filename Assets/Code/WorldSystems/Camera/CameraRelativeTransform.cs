using System;
using UnityEngine;

[Serializable]
public class CameraRelativeTransform
{
    [SerializeField] private Transform transform;
    [SerializeField] private Vector3   offset;

    public Transform Transform => transform;
    public Vector3   Offset    => offset;
}
