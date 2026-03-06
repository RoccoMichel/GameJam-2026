using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRefill : MonoBehaviour
{
    [SerializeField] private int refillIndex;
    [SerializeField] private GameObject fillVisual;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float restockDurationSeconds = 60;
    private bool canRefill; // if the player is close enough to interact
    private bool filled = true; // if station is stocked enough to be used
    private WeaponSwitching weaponManager;
    private InputAction interactAction;
    private GameObject tutorial;

    // add player input to refill and timer?
    private void Start()
    {
        filled = true;
        interactAction = InputSystem.actions.FindAction("Interact");
        weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitching>();
    }
    private void Update()
    {
        if (canRefill && interactAction.WasPressedThisFrame()) Refill();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (filled) canRefill = true;

        tutorial = CanvasController.instance.InstantiateTutorial("Refill");
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        canRefill = false;

        if (tutorial != null) Destroy(tutorial);
    }
    public void Refill()
    {
        if (effect != null) effect.Play();
        fillVisual.SetActive(false);

        filled = false;
        canRefill = false;
        weaponManager.weapons[refillIndex].Refill();
        Invoke(nameof(Restock), restockDurationSeconds);
    }
    public void Restock()
    {
        filled = true;
        fillVisual.SetActive(true);
    }
}