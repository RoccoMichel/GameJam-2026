using UnityEngine;

public class ProjectileAddon3DExplosive : ProjectileAddonExplosive
{
    public GameObject explosiveEffect2;

    public override void ProjectileLogic(Collision collision)
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

        // Make sure projectiles sticks to surface
        rb.isKinematic = true;

        // Make sure projectile moves with target
        transform.SetParent(collision.transform);

        // adds gravity to make the object fall
        rb.useGravity = true;

        int seperation = 1;
        // spawns explosive effects
        Instantiate(explosiveEffect, transform.position + (-transform.right * seperation), transform.rotation);
        Instantiate(explosiveEffect2, transform.position + (transform.right * seperation), transform.rotation);   
        Destroy(gameObject);
    }
}
