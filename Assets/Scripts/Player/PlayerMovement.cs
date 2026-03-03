using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string isWalkingRef = "isWalking";
    private const string isRunningRef = "isRunning";

    [SerializeField] private float runSpeedDefault;
    [SerializeField] private float walkSpeedDefault;
    [SerializeField] private float moveSpeed;
    private PlayerStamina playerStamina;


    private CharacterController myCC;
    //private MouseLook mouseLook;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;
    public float momentumDampening = 5f;

    [SerializeField] private Animator cameraAnimator;
    private bool isWalking;
    public bool isRunning;

    private float staminaDepletion = 0.0009f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = walkSpeedDefault;
        myCC = GetComponent<CharacterController>();
        //mouseLook = GetComponent<MouseLook>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {
        HasStaminaCheck();
        GetInput();
        MovePlayer();
        CheckShiftRelease();

        // Får kameran att röra på sig när spelaren rör på sig
        cameraAnimator.SetBool(isWalkingRef, isWalking);
        cameraAnimator.SetBool(isRunningRef, isRunning);
    }

    // Hanterar imput
    void GetInput()
    {

        // Kollar om spelaren rör på sig eller står still, påverkar kameran rörelsen och momentum
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift) && playerStamina.canRun == true)
            {
                moveSpeed = runSpeedDefault;
                isRunning = true;
                playerStamina.staminaAmount -= staminaDepletion;

            }

            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);
            isWalking = true;
        }
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDampening * Time.deltaTime);
            isWalking = false;
        }


        // ändrar på spelarens rörelsevärde från input och gravitation
        movementVector = (inputVector * moveSpeed) + (Vector3.up * myGravity);
    }

    /*private void MouseLook_OnPlayerRotate()
    {
        moveSpeed = 0;
    }*/

    // Får spelaren att röra på sig
    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }

    void CheckShiftRelease()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeedDefault;
            isRunning = false;
        }
    }

    void HasStaminaCheck()
    {
        if (playerStamina.canRun == false)
        {
            isRunning = false;
        }
        if (isRunning == false)
        {
            moveSpeed = walkSpeedDefault;
        }
    }
}
