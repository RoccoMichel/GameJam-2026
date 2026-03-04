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
        StartCoroutine(DestroyObject());
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

    }

    IEnumerator DestroyObject()
    {

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ScreenEffects.Instance.panels[0].SetActive(false);
    }
}
