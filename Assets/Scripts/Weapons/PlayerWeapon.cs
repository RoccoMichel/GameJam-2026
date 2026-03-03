using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform gunTransfrom;
    [SerializeField] private GameObject bullet;

    [Header("Stats")]
    [SerializeField] private float fireRate;
    private float timer = 1000f;


    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (timer >= fireRate)
            {
                Shoot();
                timer = 0f;
            }
        }

    }

    private void Shoot()
    {
        Instantiate(bullet, gunTransfrom.position, gunTransfrom.rotation);

    }
}
