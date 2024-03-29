using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static UnityEvent OnInteract = new();
    public static UnityEvent OnReleaseInteract = new();

    public void OnInteractInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnInteract?.Invoke();
        }

        if (ctx.canceled)
        {
            OnReleaseInteract?.Invoke();
        }
    }
}