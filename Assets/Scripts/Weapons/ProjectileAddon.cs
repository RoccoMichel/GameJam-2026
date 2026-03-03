using UnityEngine;
using UnityEngine.Timeline;

public class ProjectileAddon : MonoBehaviour
{
    public int damage;

    private Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make sure only to stick to the first target you hit
        if (targetHit)
        {
            return;
        }
        else
        {
            targetHit = true;
        }

        // check if you hit an enemy
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.Damage(damage);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Make sure projectiles sticks to surface
        rb.isKinematic = true;

        // Make sure projectile moves with target
        transform.SetParent(collision.transform);   
    }
}
