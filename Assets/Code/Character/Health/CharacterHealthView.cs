using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthView : View
{
    [SerializeField] private Slider slider;

    private CharacterHealth _characterHealth;

    protected override void OnStart()
    {
        _characterHealth = GetComponentInParent<CharacterHealth>(true);

        slider.maxValue = _characterHealth.MaxHealth;

        CharacterHealth_onUpdateHealth(_characterHealth.Health);

        _characterHealth.onUpdateHealth += CharacterHealth_onUpdateHealth;
    }

    protected override void OnUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    protected override void OnFinal()
    {
        _characterHealth.onUpdateHealth -= CharacterHealth_onUpdateHealth;
    }

    private void CharacterHealth_onUpdateHealth(int health)
    {
        slider.value = health;
    }
}
