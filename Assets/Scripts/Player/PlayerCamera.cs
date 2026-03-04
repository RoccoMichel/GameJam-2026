using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSen = 2.5f;
    [SerializeField] private Transform player;
    private float xRotation = 0f;
    private InputAction lookAction;
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        mouseSen = PlayerPrefs.GetFloat("sensitivity", 1);
        Cursor.lockState = CursorLockMode.Locked; //Låser vår mus till skärmen, och så den inte syns. 
    }
    void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        Vector2 input = lookAction.ReadValue<Vector2>() * mouseSen;

        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * input.x);
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
