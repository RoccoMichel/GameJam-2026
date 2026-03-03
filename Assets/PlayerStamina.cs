using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float staminaAmount;
    private float maxStamina = 1f;
   // [SerializeField] private Slider staminaBar;
    private const string staminaBarName = "StaminaBar";
    public bool canRun;
    private PlayerMovement playerMovement;

    private float staminaRecoveryRate = 0.003f;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        staminaAmount = maxStamina;
        //staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        //staminaBar.value = maxStamina;

        //staminaBar.maxValue = maxStamina;
        //staminaBar.minValue = 0f;
    }

    private void Update()
    {
        StaminaCheck();
        StaminaRegeneration();
    }

    private void StaminaCheck()
    {
       // staminaBar.value = staminaAmount;

        if (staminaAmount <= 0)
        {
            staminaAmount = 0;
            canRun = false;
        }
        else
        {
            canRun = true;
        }
    }

    private void StaminaRegeneration()
    {
        if (staminaAmount < 1 && playerMovement.isRunning == false)
        {
            staminaAmount += staminaRecoveryRate;
        }
    }
}
