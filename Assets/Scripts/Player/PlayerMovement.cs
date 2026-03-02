using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = walkSpeed;
    public float gravity = -9f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private const float walkSpeed = 5f; //dela upp speed i två olika värden så vi har ett får att gå och ett för att sprinta
    private const float runSpeed = 10f;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //minus två så den inte registrerar innan vi nått marken
        }
        float x = Input.GetAxis("Horizontal"); //Gå med WASD
        float z = Input.GetAxis("Vertical"); //Gå med WASD
        Vector3 move = transform.right * x + transform.forward * z; //Rör sig i den riktningen som player också tittar i
        controller.Move(move * moveSpeed * Time.deltaTime);
        //Ref till vår character controller som driver vår player + låter oss röra på oss
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Så vi kan hoppa
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        { //rör oss snabbare med shift

            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, .3f);
    }
}
