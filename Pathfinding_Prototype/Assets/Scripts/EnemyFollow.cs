using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{

    public NavMeshAgent Enemy;
    Transform Player;
    public Animator animator;

    public LayerMask GroundMask;
    public LayerMask PlayerMask;

    // Patrolling State
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attack State
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float Damage;

    // States
    public float sightRange;
    public float attackRange;

    public bool playerInSightRange;
    public bool playerInAttackRange;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            Enemy.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float RandomZ = Random.Range(-walkPointRange, walkPointRange);
        float RandomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, GroundMask))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        Enemy.SetDestination(Player.position);
    }

    void AttackPlayer()
    {
        Enemy.SetDestination(transform.position);

        transform.LookAt(Player);

        if (!alreadyAttacked)
        {

            animator.SetTrigger("Attack");

            Player.GetComponent<PlayerHealth>().TakeDamage(Damage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}