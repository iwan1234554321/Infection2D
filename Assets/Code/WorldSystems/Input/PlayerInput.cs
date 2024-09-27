using Notteam.World;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterPlayerController))]
public class PlayerInput : WorldEntity
{
    private CharacterPlayerController _characterPlayerController;

    protected override void OnStart()
    {
        _characterPlayerController = GetComponent<CharacterPlayerController>();

        World.Instance.GetSystem<PlayerInputSystem>().SubscribeToAction("Player", "Move",
            started: SetMoveInputStart,
            performed: SetMoveInputPerformed,
            canceled: SetMoveInputCancel);

        World.Instance.GetSystem<PlayerInputSystem>().SubscribeToAction("Player", "Fire",
           started: SetFireInputStart,
           canceled: SetFireInputCancel);
    }

    //protected override void OnUpdate()
    //{
    //    _characterMover.SetDirection(_moveInput);

    //    if (Mathf.Abs(_moveInput) > 0.0f)
    //        _characterRotator.SetDirection(_moveInput);
    //}

    protected override void OnFinal()
    {
        World.Instance.GetSystem<PlayerInputSystem>().UnSubscribeFromAction("Player", "Move",
            started: SetMoveInputStart,
            performed: SetMoveInputPerformed,
            canceled: SetMoveInputCancel);

        World.Instance.GetSystem<PlayerInputSystem>().UnSubscribeFromAction("Player", "Fire",
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
