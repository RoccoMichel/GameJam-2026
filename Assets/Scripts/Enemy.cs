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

    [SerializeField] private AudioClip[] sounds;
    private float currentTime;
    private float defaultTime;

    private Animator animator;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        defaultTime = Random.Range(5, 10);
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

        ZombieMoan();
        RandomAttackSound();
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

    // Used in update
    private void ZombieMoan()
    {
        currentTime += Time.deltaTime;

        if (currentTime < defaultTime) return;

        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.Play();
        defaultTime = Random.Range(5, 25);
        currentTime = 0;
    }

    private void RandomAttackSound()
    {
        string[] hurtFileNames =
        {
            "hurt-1",
            "hurt-2",
            "hurt-3",
            "hurt-4",
            "hurt-5",
            "hurt-6",
            "hurt-7"
        };

        GameController.Instance.SFX(hurtFileNames[Random.Range(0, hurtFileNames.Length)]);
    }
}


