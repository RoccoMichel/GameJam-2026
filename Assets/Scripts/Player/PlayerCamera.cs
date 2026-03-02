using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSen = 100f;
    [SerializeField] private Transform player;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Låser vår mus till skärmen, och så den inte syns. 
    }
    void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        //Hämtar vår axis från input manager under project settings
        float mouseX = Input.GetAxis("Mouse X") * mouseSen;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen;
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Vi använder inte rotate för att vi ska kunna stoppa rotationen från att gå för långt (max 90 grader)
        player.Rotate(Vector3.up * mouseX);
    }
}
