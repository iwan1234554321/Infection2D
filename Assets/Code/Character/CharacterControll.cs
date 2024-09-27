using Notteam.World;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotator))]
[RequireComponent(typeof(CharacterHealth))]
public class CharacterControll : WorldEntity
{
    [SerializeField] protected CharacterSettings settings;

    protected SpriteAnimator   spriteAnimator;
    protected CharacterMover   characterMover;
    protected CharacterRotator characterRotator;
    protected CharacterHealth  characterHealth;

    public CharacterSettings Settings => settings;

    protected override void OnStart()
    {
        spriteAnimator   = GetComponentInChildren<SpriteAnimator>();
        characterMover   = GetComponent<CharacterMover>();
        characterRotator = GetComponent<CharacterRotator>();
        characterHealth  = GetComponent<CharacterHealth>();
    }
}
