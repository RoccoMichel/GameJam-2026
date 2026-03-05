using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    [SerializeField] private Sprite crosshair;

    [Header("Settings")]
    public bool infiniteThrows;
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;
    private bool readyToThrow;

    private InputAction attackAction;
    private Animator animator;

    private void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        readyToThrow = true;
    }

    private void Update()
    {
        if (attackAction.WasPressedThisFrame() && readyToThrow)
        {
            Throw();
        }
    }

    private void Throw()
    {
        if (totalThrows <= 0 && !infiniteThrows) return; // out of ammo

        //animator.Play("Attack");
        readyToThrow = false;

        // Instantiate objects to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // Get rigid body component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate directions
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        if (!infiniteThrows) totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void OnEnable()
    {
        if (crosshair != null) CanvasController.instance.UpdateCrosshair(crosshair.name);
    }
}
