using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float StaminaAmount
    {
        get => _staminaAmount;
        set { _staminaAmount = Mathf.Clamp01(value); }
    }
    private float _staminaAmount;

    public bool canRun;
    private float staminaRecoveryRate = 0.25f;
    private PlayerMovement playerMovement;
    private Slider staminaBarSlider;
    private Image staminaBarImage;

    private void Start()
    {
        StaminaAmount = 1;
        playerMovement = GetComponent<PlayerMovement>();
        staminaBarSlider = CanvasController.instance.staminaBar;
        staminaBarImage = staminaBarSlider.fillRect.GetComponent<Image>();
    }

    private void Update()
    {
        StaminaCheck();
        StaminaRegeneration();

        staminaBarSlider.value = StaminaAmount;
        staminaBarImage.color = canRun ? Color.white : Color.red;
    }

    private void StaminaCheck()
    {
        if (StaminaAmount <= 0)
        {
            StaminaAmount = 0;
            canRun = false;
        }
        else if (!canRun && StaminaAmount == 1)
        {
            canRun = true;
        }
    }

    private void StaminaRegeneration()
    {
        if (StaminaAmount < 1 && playerMovement.isRunning == false)
        {
            StaminaAmount += staminaRecoveryRate * Time.deltaTime;
        }
    }
}
