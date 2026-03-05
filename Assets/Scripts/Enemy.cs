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

    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (agent.enabled) 
            agent.destination = player.transform.position;
        animator.SetFloat("speed", agent.velocity.magnitude);

        attackTimer += Time.deltaTime;
        if (range > distanceFromPlayer && attackTimer > attackSpeedSeconds) Attack();
    }

    public void Attack()
    {
        if (Health <= 0) return;

        attackTimer = 0;
        player.Damage(damage);

        animator.Play("Attack");
    }

    public override void Die()
    {
        agent.enabled = false;
        gameObject.layer = 0;
        animator.Play(Random.Range(0, 10) > 7 ? "Deadflip" : "Dead");
        Destroy(gameObject, 0.7f);
    }
}