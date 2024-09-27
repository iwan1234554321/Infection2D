using UnityEngine;

public class CharacterAddHealth
{
    private GameObject _character;
    private int        _addHealth;

    public CharacterAddHealth(GameObject character, int addHealth)
    {
        _character = character;
        _addHealth = addHealth;
    }

    public GameObject Character => _character;
    public int        AddHealth => _addHealth;
}
