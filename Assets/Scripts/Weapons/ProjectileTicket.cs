using UnityEngine;

public class ProjectileTicket : ProjectileAddon
{
    [SerializeField] private int penetrationAmount;
    [SerializeField] private int maxPenetration;

    private void OnTriggerEnter(Collider other)
    {
        // check if you hit an enemy

        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damage(damage);
            Instantiate(Resources.Load<GameObject>("Effects/Blood"), transform.position, Quaternion.identity);

            penetrationAmount++;
            if (penetrationAmount >= maxPenetration)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Projectile hit player");
            targetHit = false;
            return;
        }
        else if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }

        // Make sure projectiles sticks to surface
        // rb.isKinematic = true;

        // Make sure projectile moves with target
        transform.SetParent(other.transform);
    }
}
