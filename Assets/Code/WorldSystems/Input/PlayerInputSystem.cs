using Notteam.World;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputSystem : WorldSystem<PlayerInput>
{
    [SerializeField] private InputActionAsset asset;

    protected override void OnStart()
    {
        asset.Enable();

        World.Instance.GetSystem<EventDispatcherSystem>().SubscribeToEvent<GameOverEvent>(GameOver);
    }

    protected override void OnFinal()
    {
        asset.Disable();

        World.Instance.GetSystem<EventDispatcherSystem>().UnSubscribeFromEvent<GameOverEvent>(GameOver);
    }

    private void GameOver(GameOverEvent @event)
    {
        asset.Disable();
    }

    private bool TryGetAction(string nameMap, string nameAction, out InputAction inputAction)
    {
        inputAction = null;

        var map = asset.FindActionMap(nameMap);

        if (map != null)
        {
            var action = map.FindAction(nameAction);

            if (action != null)
            {
                inputAction = action;

                return true;
            }
        }

        return false;
    }

    public void SubscribeToAction(string nameMap, string nameAction, Action<CallbackContext> started = null, Action<CallbackContext> performed = null, Action<CallbackContext> canceled = null)
    {
        if (TryGetAction(nameMap, nameAction, out var action))
        {
            if (started != null)
                action.started += started;

            if (performed != null)
                action.performed += performed;

            if (canceled != null)
                action.canceled  += canceled;
        }
    }

    public void UnSubscribeFromAction(string nameMap, string nameAction, Action<CallbackContext> started = null, Action<CallbackContext> performed = null, Action<CallbackContext> canceled = null)
    {
        if (TryGetAction(nameMap, nameAction, out var action))
        {
            if (started != null)
                action.started -= started;

            if (performed != null)
                action.performed -= performed;

            if (canceled != null)
                action.canceled -= canceled;
        }
    }
}
