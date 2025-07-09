using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMover : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjustable in Inspector

    private Vector2 moveInput;    // Stores the current movement input
    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        // Hook up the Move action
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}

