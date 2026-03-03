using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSen = 2.5f;
    [SerializeField] private Transform player;
    private float xRotation = 0f;
    private InputAction lookAction;
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked; //Låser vår mus till skärmen, och så den inte syns. 
    }
    void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        Vector2 input = lookAction.ReadValue<Vector2>() * mouseSen * Time.deltaTime;

        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * input.x);
    }
}
