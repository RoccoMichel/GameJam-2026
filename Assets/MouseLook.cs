using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float xMousePos;
    private float smoothedMousePos;

    public float sensitivty = 1.5f;
    private float smoothing = 1.5f;

    private float currentLookingPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Låser musen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    // Får kameran att kolla höger eller vänster
    void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    // Camera sensitivity och smoothing
    void ModifyInput()
    {
        xMousePos *= sensitivty * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f / smoothing);
    }

    // Ändrar på player modellens rotation
    void MovePlayer()
    {
        currentLookingPos += smoothedMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}
