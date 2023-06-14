using UnityEngine;
using UnityEngine.AI;

namespace Embers
{
    public class EnemyController : MonoBehaviour
    {
        // States
        public PatrolBehavior patrolBehavior;
        public ChaseBehavior chaseBehavior;
        public AttackBehavior attackBehavior;
        public enum EnemyState
        {
            Patrolling,
            Chasing,
            Idle
        }
        public EnemyState currentState;

        // For patrol points
        public GameObject[] spawners;
        public int enemyType;

        // Flame detection
        public float flameHeath;
        public float distanceToFlame;
        public bool patrolLock = false;
        [SerializeField] public float patrolLockDuration = 3f;
        public float patrolLockCurrent = 0f;
        public Transform flameTransform;
        public float flameRadius;

        // Player detection
        [SerializeField] public float detectionRange = 10f;
        [SerializeField] public float fieldOfViewAngle = 60f;
        public Transform playerTransform;
        public LayerMask playerLayer;

        public NavMeshAgent agent;

        private void Start()
        {
            // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
            patrolBehavior = GetComponent<PatrolBehavior>();
            chaseBehavior = GetComponent<ChaseBehavior>();
            attackBehavior = GetComponent<AttackBehavior>();

            // Get enemy type
            spawners = GameObject.FindGameObjectWithTag("World Generator").GetComponent<WorldGenerator>().enemySpawners;
            enemyType = GetEnemyType();

            agent = GetComponent<NavMeshAgent>();

            // Find the player and flame references
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // Activate the patrol behavior by default
            currentState = EnemyState.Patrolling;
        }

        private void Update()
        {
            // If enemy is close to flame, it is locked into patrol behavior
            flameHeath = GameObject.FindGameObjectWithTag("FlameUI").GetComponent<FlameHealth>().health;
            flameRadius = flameHeath / 5f;
            distanceToFlame = Vector3.Distance(transform.position, flameTransform.position);
            if (distanceToFlame <= flameRadius)
            {
                patrolLock = true;
                currentState = EnemyState.Patrolling;
            }
            if(patrolBehavior == null || chaseBehavior == null || attackBehavior == null || agent == null)
            {
                // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
                patrolBehavior = GetComponent<PatrolBehavior>();
                chaseBehavior = GetComponent<ChaseBehavior>();
                attackBehavior = GetComponent<AttackBehavior>();
                agent = GetComponent<NavMeshAgent>();
                return;
            }

            if(patrolBehavior == null || chaseBehavior == null || attackBehavior == null || agent == null)
            {
                // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
                patrolBehavior = GetComponent<PatrolBehavior>();
                chaseBehavior = GetComponent<ChaseBehavior>();
                attackBehavior = GetComponent<AttackBehavior>();
                agent = GetComponent<NavMeshAgent>();
                return;
            }

            switch (currentState)
            {
                case EnemyState.Patrolling:
                    // Handle patrolling behavior
                    patrolBehavior.EnemyPatrol();
                    break;

                case EnemyState.Chasing:
                    // Handle chasing behavior
                    chaseBehavior.EnemyChase();
                    break;

                case EnemyState.Idle:
                    // Handle idle behavior
                    break;
            }

            // Possibly unnecessary
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // Attack when enemy is chasing and player is in range
            if (currentState == EnemyState.Chasing && attackBehavior.CanAttack() && attackBehavior.IsPlayerInRange())
            {
                attackBehavior.EnemyAttack();
            }
        }

        public int GetEnemyType()
        {
            float shortestDistance = Vector3.Distance(transform.position, spawners[0].transform.position);
            int shortestDistanceIndex = 0;
            for (int i = 1; i < spawners.Length; i++)
            {
                if (Vector3.Distance(transform.position, spawners[i].transform.position) < shortestDistance)
                {
                    shortestDistance = Vector3.Distance(transform.position, spawners[i].transform.position);
                    shortestDistanceIndex = i;
                }
            }

            enemyType = shortestDistanceIndex;

            return enemyType;
        }

        public bool IsPlayerInSight()
        {
            // Check if the player is within detection range
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                // Calculate direction to the player
                Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
                // Check if the player is within the field of view angle
                if (Vector3.Angle(transform.forward, directionToPlayer) <= fieldOfViewAngle / 2f)
                {
                    // Cast a ray towards the player to check for any obstructions
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, playerLayer))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            // Player is within line of sight
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
