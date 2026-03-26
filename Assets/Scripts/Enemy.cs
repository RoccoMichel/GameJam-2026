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

    public AudioSource zombieMoan1;
    public AudioSource zombieMoan2;
    public AudioSource zombieMoan3;
    private float currentTime;
    private float defaultTime;

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

        zombieRandomizer = Random.Range(0, 30);
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
        RandomizeZombieRandomizer();
        ZombieMoan();
        RandomzieAttackSound();
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

    // Used in update
    private void ZombieMoan()
    {
        currentTime += 1 * Time.deltaTime;

        if (currentTime >= defaultTime)
        {
            zombieRandomizer = Random.Range(0, 2);
            switch (zombieRandomizer)
            {
                case 0:
                    zombieMoan1.Play();
                    zombieRandomizer = Random.Range(10, 50);
                    defaultTime = Random.Range(5, 25);
                    break;
                case 1:
                    zombieMoan2.Play();
                    zombieRandomizer = Random.Range(10, 50);
                    defaultTime = Random.Range(5, 25);
                    break;
                case 2:
                    zombieMoan3.Play();
                    zombieRandomizer = Random.Range(10, 50);
                    defaultTime = Random.Range(5, 25);
                    break;
            }
            Debug.Log("Switch broken");
            currentTime = 0;
        }
    }

    private void RandomizeZombieRandomizer()
    {
        zombieRandomizer = Random.Range(0, 20);
    }

    private void RandomzieAttackSound()
    {
        zombieRandomizer = Random.Range(0, 6);
        switch (zombieRandomizer)
        {
            case 0:
                GameController.Instance.SFX("sound-hurt-1");
                zombieRandomizer = Random.Range(10, 50);
                break;
            case 1:
                GameController.Instance.SFX("sound-hurt-2");
                zombieRandomizer = Random.Range(10, 50);
                break;
            case 2:
                GameController.Instance.SFX("sound-hurt-3");
                zombieRandomizer = Random.Range(10, 50);
                break;
            case 3:
                GameController.Instance.SFX("sound-hurt-4");
                zombieRandomizer = Random.Range(10, 50);
                break;
            case 4:
                GameController.Instance.SFX("sound-hurt-5");
                zombieRandomizer = Random.Range(10, 50);
                break;
            case 5:
                GameController.Instance.SFX("sound-hurt-6");
                zombieRandomizer = Random.Range(10, 50);
                break;
            case 6:
                GameController.Instance.SFX("sound-hurt-27");
                zombieRandomizer = Random.Range(10, 50);
                break;
        }
    }
}


