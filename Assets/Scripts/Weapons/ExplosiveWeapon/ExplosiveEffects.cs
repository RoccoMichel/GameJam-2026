using System.Collections;
using UnityEngine;

public class ExplosiveEffects : MonoBehaviour
{
    private int damage;
    private ProjectileAddonExplosive connectedProjectile;
    private void Start()
    {
        connectedProjectile = FindAnyObjectByType<ProjectileAddonExplosive>();
        this.damage = connectedProjectile.damage;
        Destroy(connectedProjectile);
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if you hit an enemy
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.Damage(damage);

            StartCoroutine(DestroyObject());
        }
        else if (other.gameObject.layer != 3)
        {
            StartCoroutine(DestroyObject());
        }


    }

    IEnumerator DestroyObject()
    {

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
