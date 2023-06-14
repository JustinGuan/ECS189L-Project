using UnityEngine;
using UnityEngine.AI;

/*
To-do:
(Fixed?) Enemy doesn't switch to chase speed in chase mode.
(Fixed?  (Need reference to flame health.) Enemy doesn't avoid flame radius.
(Fixed?) (Needs reference to player health.  Put this in player controller.) Enemy doesn't do damage.  (Create setter function.)
The patrol points need to be placed around the spawners.
*/

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

        //[SerializeField] public float enemyHealth = 100;
        public NavMeshAgent agent;

        // Flame detection
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

        private void Start()
        {
            // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
            patrolBehavior = GetComponent<PatrolBehavior>();
            chaseBehavior = GetComponent<ChaseBehavior>();
            attackBehavior = GetComponent<AttackBehavior>();

            agent = GetComponent<NavMeshAgent>();

            // Find the player and flame references
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            //flameRadius = GameObject.FindGameObjectWithTag("Flame").currentCapacity / 2f; (Implement)

            // Activate the patrol behavior by default
            currentState = EnemyState.Patrolling;
        }

        private void Update()
        {
            // If enemy is close to flame, it is locked into patrol behavior
            //flameRadius = GameObject.FindGameObjectWithTag("Flame").currentCapacity / 2f; (Implement)
            //distanceToFlame = Vector3.Distance(transform.position, flameTransform.position);  (Implement)
            //if (distanceToFlame <= flameRadius)   (Implement)
            //{ (Implement)
                //patrolLock = true;    (Implement)
                //currentState = EnemyState.Patrolling; (Implement)
            //} (Implement)
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

        /*
        public void DamageEnemy(int damageAmount)
        {
            enemyHealth -= damageAmount;
            if (enemyHealth <= 0)
            {
                // Call a method to handle enemy death or despawning
                Die();
            }
        }

        public void Die()
        {
            // Perform any necessary cleanup or death-related logic
            // For example, we might play an animation, trigger particle effects, or update the game state

            // Destroy the enemy object
            Destroy(gameObject);
        }
        */


    }
}
