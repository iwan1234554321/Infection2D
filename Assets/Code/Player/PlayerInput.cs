using Notteam.World;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterPlayerController))]
public class PlayerInput : InputObject
{
    private CharacterPlayerController _characterPlayerController;

    protected override void OnStart()
    {
        _characterPlayerController = GetComponent<CharacterPlayerController>();

        World.Instance.GetSystem<InputSystem>().SubscribeToAction("Player", "Move",
            started: SetMoveInputStart,
            performed: SetMoveInputPerformed,
            canceled: SetMoveInputCancel);

        World.Instance.GetSystem<InputSystem>().SubscribeToAction("Player", "Fire",
           started: SetFireInputStart,
           canceled: SetFireInputCancel);
    }

    protected override void OnFinal()
    {
        World.Instance.GetSystem<InputSystem>().UnSubscribeFromAction("Player", "Move",
            started: SetMoveInputStart,
            performed: SetMoveInputPerformed,
            canceled: SetMoveInputCancel);

        World.Instance.GetSystem<InputSystem>().UnSubscribeFromAction("Player", "Fire",
           started: SetFireInputStart,
           canceled: SetFireInputCancel);
    }

    private void SetFireInputStart(CallbackContext context)
    {
        _characterPlayerController.FireStart();
    }

    private void SetFireInputCancel(CallbackContext context)
    {
        _characterPlayerController.FireFinal();
    }

    private void SetMoveInputStart(CallbackContext context)
    {
        _characterPlayerController.MoveStart(context.ReadValue<float>());
    }

    private void SetMoveInputPerformed(CallbackContext context)
    {
        _characterPlayerController.Move(context.ReadValue<float>());
    }

    private void SetMoveInputCancel(CallbackContext context)
    {
        _characterPlayerController.MoveFinal(context.ReadValue<float>());
    }
}
