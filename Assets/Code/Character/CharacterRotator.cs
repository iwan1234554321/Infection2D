using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField] private float _direction;

    private void Update()
    {
        transform.rotation = Quaternion.AngleAxis(_direction >= 0.0f ? 0.0f : 180.0f, Vector3.up);
    }

    public void SetDirection(float value)
    {
        _direction = value;
    }
}
