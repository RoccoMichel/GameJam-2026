using System.Collections;
using UnityEngine;

public class ExplosiveEffects : MonoBehaviour
{
    private int damage;
    private ProjectileAddonExplosive connectedProjectile;
    [SerializeField] float lastingTime;
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
        if (other.gameObject.GetComponent<Entity>() != null)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Damage(damage);
            }
            else if (other.gameObject.GetComponent<Entity>() != null)
            {
                Entity enemy = other.gameObject.GetComponent<Entity>();
                enemy.Damage(damage/10);
            }

            StartCoroutine(DestroyObject());
        }

    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(lastingTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        CanvasController.Instance.sodaPanel.SetActive(false);
    }
}
