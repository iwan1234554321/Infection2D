using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Notteam/Create CharacterSettings", order = 0)]
public class CharacterSettings : ScriptableObject
{
    [Header("Characteristics")]
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private int   health    = 50;

    [Header("Animation")]
    [SerializeField] private int idleAnimationIndex;
    [SerializeField] private int walkAnimationIndex;

    [Header("Sound")]
    [SerializeField] private string deadSound;

    [Header("Particles")]
    [SerializeField] private string bloodParticle;

    public float MoveSpeed => moveSpeed;
    public int   Health    => health;

    public int IdleAnimationIndex => idleAnimationIndex;
    public int WalkAnimationIndex => walkAnimationIndex;

    public string DeadSound     => deadSound;
    public string BloodParticle => bloodParticle;
}
