using UnityEngine;

public class CharacterRemoveHealth
{
    private GameObject      _character;
    private int             _removeHealth;
    private ContactPoint2D? _contact;

    public CharacterRemoveHealth(GameObject character, int removeHealth)
    {
        _character    = character;
        _removeHealth = removeHealth;
    }

    public CharacterRemoveHealth(GameObject character, int removeHealth, ContactPoint2D contact)
    {
        _character    = character;
        _removeHealth = removeHealth;
        _contact      = contact;
    }

    public GameObject      Character    => _character;
    public int             RemoveHealth => _removeHealth;
    public ContactPoint2D? Ñontact      => _contact;
}
