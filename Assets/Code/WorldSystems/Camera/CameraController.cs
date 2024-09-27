using Notteam.World;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : WorldEntity
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetBounds;
    [Space]
    [SerializeField] private CameraRelativeTransform[] relativeTransforms;
    [Space]
    [SerializeField] private float          shakeTime = 0.25f;
    [SerializeField] private float          shakeAngle = 30.0f;
    [SerializeField] private AnimationCurve shakeCurve;

    private float _height;
    private float _width;

    private float _limitWidthMax;
    private float _limitHeightMax;

    private float _limitWidthMin;
    private float _limitHeightMin;

    private Camera _camera;

    protected override void OnStart()
    {
        _camera = GetComponent<Camera>();
    }

    protected override void OnUpdate()
    {
        _height = _camera.orthographicSize;
        _width  = _height * _camera.aspect;

        if (targetBounds)
        {
            var limitWidth  = ((targetBounds.localScale.x / 2) - _width);
            var limitHeight = ((targetBounds.localScale.y / 2) - _height);

            _limitWidthMax  = targetBounds.transform.position.x + limitWidth;
            _limitHeightMax = targetBounds.transform.position.y + limitHeight;

            _limitWidthMin  = targetBounds.transform.position.x - limitWidth;
            _limitHeightMin = targetBounds.transform.position.y - limitHeight;
        }

        if (target)
        {
            SetPosition(target.position);
        }

        var xPos = Mathf.Clamp(transform.position.x, _limitWidthMin, _limitWidthMax);
        var yPos = Mathf.Clamp(transform.position.y, _limitHeightMin, _limitHeightMax);

        transform.position = new Vector3(xPos, yPos, transform.position.z);

        foreach (var relativeTransform in relativeTransforms)
        {
            if (relativeTransform.Transform)
            {
                var position = transform.TransformPoint(relativeTransform.Offset.x * _width, relativeTransform.Offset.y * _height, 0);

                relativeTransform.Transform.position = new Vector3(position.x, position.y, relativeTransform.Transform.position.z);
            }
        }
    }

    public void Shake()
    {
        gameObject.CreateTween(new Tween("CameraShake", shakeTime, true,
            onUpdate: (t) =>
            {
                var shakeRotation = Quaternion.AngleAxis(shakeAngle * shakeCurve.Evaluate(t), Vector3.forward);

                transform.rotation = shakeRotation;
            }));
    }

    private void SetPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        foreach (var relativeTransform in relativeTransforms)
        {
            Gizmos.DrawSphere(transform.TransformPoint(relativeTransform.Offset.x * _width, relativeTransform.Offset.y * _height, 0), 0.2f);
        }
    }
}
