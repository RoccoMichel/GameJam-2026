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

    private int zombieRandomizer;

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

        zombieRandomizer = Random.Range(0, 20);
    }

    private void FixedUpdate()
    {
        if (agent.enabled)
        {
            agent.destination = player.transform.position;
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (range > distanceFromPlayer && attackTimer > attackSpeedSeconds) 
            Attack();

        ZombieMoan();
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
        GameController.Instance.ReportEnemyDeath();
        animator.Play(Random.Range(0, 10) > 7 ? "Deadflip" : "Dead");
        Destroy(gameObject, 0.7f);
    }

    private void OnDestroy()
    {
        Instantiate(Resources.Load<GameObject>("Effects/Zombie Dead"), 
            transform.position + Vector3.down * 0.8f, 
            Quaternion.Euler(new Vector3(270, 0, 0) + transform.rotation.eulerAngles));
    }

    private void ZombieMoan()
    {
        if (zombieRandomizer == Random.Range(0, 20))
        {
            zombieRandomizer = Random.Range(0, 2);
            switch (zombieRandomizer)
            {
                case 0:
                    GameController.Instance.SFX("zombie-moan-1");
                    zombieRandomizer = Random.Range(10, 50);
                    break;
                case 1:
                    GameController.Instance.SFX("zombie-moan-2");
                    zombieRandomizer = Random.Range(10, 50);
                    break;
                case 2:
                    GameController.Instance.SFX("zombie-moan-3");
                    zombieRandomizer = Random.Range(10, 50);
                    break;
            }
        }
    }


}