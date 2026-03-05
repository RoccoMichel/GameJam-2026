using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRefill : MonoBehaviour
{
    [SerializeField] private int refillIndex;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float restockDurationSeconds = 60;
    private bool canRefill; // if the player is close enough to interact
    private bool stocked; // if station is stocked enough to be used
    private WeaponSwitching weaponManager;
    private InputAction interactAction;
    private GameObject tutorial;

    // add player input to refill and timer?
    private void Start()
    {
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
        if (stocked) canRefill = true;

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
        weaponManager.weapons[refillIndex].Refill();

        canRefill = false;
        Invoke(nameof(Restock), restockDurationSeconds);

        if (effect != null) effect.Play();
    }
    public void Restock() => stocked = true;
}