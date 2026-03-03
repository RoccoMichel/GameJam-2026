
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objecToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("THrowing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        // Instantiate objects to throw
        GameObject projectile = Instantiate(objecToThrow, attackPoint.position, cam.rotation);

        // Get rigid body component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Add force
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
