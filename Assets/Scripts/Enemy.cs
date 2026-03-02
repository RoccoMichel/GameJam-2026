using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity
{
    [Header("Enemy Attributes")]
    public float damage = 10;
    public float attackSpeedSeconds = 1f;
    public float speed = 5;
    public float range = 2.5f;

    private float distanceFromPlayer
    {
        get
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
    private float attackTimer = 0;
    private Player player;
    private NavMeshAgent agent;
    protected override void OnStart()
    {
        base.OnStart();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        agent.destination = player.transform.position;

        attackTimer += Time.deltaTime;
        if (range > distanceFromPlayer && attackTimer > attackSpeedSeconds) Attack();
    }

    public void Attack()
    {
        attackTimer = 0;
        player.Health -= damage;
    }
}