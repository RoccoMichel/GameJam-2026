using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] private float staminaDepletion = 0.1f;

    private InputAction moveAction;
    private InputAction sprintAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");

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
        if (moveAction.IsPressed())
        {
            if (sprintAction.IsPressed() && playerStamina.canRun == true)
            {
                moveSpeed = runSpeedDefault;
                isRunning = true;
                playerStamina.StaminaAmount -= staminaDepletion * Time.deltaTime;

            }
            else if (sprintAction.WasPressedThisFrame() && playerStamina.canRun == false)
                CanvasController.Instance.InstantiateTutorial("Stamina", false);

            inputVector = new Vector3(moveAction.ReadValue<Vector2>().x, 0f, moveAction.ReadValue<Vector2>().y);
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
        if (sprintAction.WasReleasedThisFrame())
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
