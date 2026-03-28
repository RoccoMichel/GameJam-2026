using UnityEngine;

public class ProjectileAddonExplosive : ProjectileAddon
{
    public GameObject explosiveEffect;
    public virtual void Awake()
    {
        //GameController.Instance.SFX("weapon-shoot-cola");
    }
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

        // spawns explosive effects
        Instantiate(explosiveEffect, transform.position, Quaternion.identity);
        GameController.Instance.SFX("cola-floor");
        Destroy(gameObject);
    }
}
