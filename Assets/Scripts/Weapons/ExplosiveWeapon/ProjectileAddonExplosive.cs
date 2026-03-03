using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ProjectileAddonExplosive : ProjectileAddon
{
    public override void ProjectileLogic(Collision collision)
    {
        base.ProjectileLogic(collision);

        // adds gravity to make the object fall
        rb.useGravity = true;
    }
}
