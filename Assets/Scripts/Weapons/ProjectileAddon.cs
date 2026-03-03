using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ProjectileAddon : MonoBehaviour
{
    public int damage;

    public Rigidbody rb;

    public bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ProjectileLogic(collision);
    }

    public virtual void ProjectileLogic(Collision collision)
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
        else if (LayerMask.GetMask("Entity") != 3)
        {
            Destroy(gameObject);
        }

        // Make sure projectiles sticks to surface
        rb.isKinematic = true;

        // Make sure projectile moves with target
        transform.SetParent(collision.transform);
    }
}
