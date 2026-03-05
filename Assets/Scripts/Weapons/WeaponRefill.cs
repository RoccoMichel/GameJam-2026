using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRefill : MonoBehaviour
{
    [SerializeField] private int refillIndex;
    [SerializeField] private ParticleSystem effect;
    private WeaponSwitching weaponManager;
    private InputAction interactAction;
    // add player input to refill and timer?
    private void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitching>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (effect != null) effect.Play();
        weaponManager.weapons[refillIndex].Refill();
    }
}
