using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimationData", menuName = "Notteam/Create SpriteAnimationData",  order = 0)]
public class SpriteAnimationData : ScriptableObject
{
    [SerializeField] private float    speed = 1.0f;
    [SerializeField] private Sprite[] sprites;

    public float    Speed   => speed;
    public Sprite[] Sprites => sprites;
}
