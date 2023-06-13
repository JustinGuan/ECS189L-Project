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
        public NavMeshAgent agent;

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

        private void Start()
        {
            // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
            patrolBehavior = GetComponent<PatrolBehavior>();
            chaseBehavior = GetComponent<ChaseBehavior>();
            attackBehavior = GetComponent<AttackBehavior>();

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
        private void SetPatrolPoints(GameObject enemy)
        {
            // List to hold our new patrol points.
            Transform[] patrolPoints = new Transform[4];
            // get our spawner's and fire's x and z coords.
            float spawnerX = this.transform.position.x;
            float spawnerZ = this.transform.position.z;
            float fireX = tracker.GetFireplacePos().x;
            float fireZ = tracker.GetFireplacePos().z;
            // Get the fire's radius.
            float curFireRadius = fireMechanic.GetFireRadius();
            // Set the 4 patrol points around the spawner.
            // The radii will be set at 0, pi/2, pi, and 3pi/2 degrees.
            for(int i = 0; i < 4; i++)
            {
                // There will be 4 patrol points, each at 2/3 of its max radius spawn.
                float patrolRadius = this.maxRadius * 0.67f;
                float theta = ((float)i * Mathf.PI) / 2.0f;
                float x = spawnerX + (patrolRadius * Mathf.Cos(theta));
                float z = spawnerZ + (patrolRadius * Mathf.Sin(theta));
                // Edge case: patrol point is within the fire radius, change the value accordingly.
                Vector3 newPatrolPoint = new Vector3(x, this.transform.position.y, z);
                // If our patrol point falls within the fire's safe zone, change either x or z value.
                if(Vector3.Distance(newPatrolPoint, tracker.GetFireplacePos()) <= curFireRadius)
                {
                    // Update the patrol radius, so that it's at the edge of the fire radius.
                    patrolRadius -= curFireRadius;
                    // Determine which value (x or z) needs to be modfied.
                    if(Mathf.Abs(fireX - spawnerX) > Mathf.Abs(fireZ - spawnerZ))
                    {
                        x = spawnerX + (patrolRadius * Mathf.Cos(theta));
                    }
                    else
                    {
                        z = spawnerZ + (patrolRadius * Mathf.Sin(theta));
                    }
                    // Re-initialize one of our patrol points.
                    newPatrolPoint = new Vector3(x, this.transform.position.y, z);
                }
                // Create a new gameobject, and grab it's transform.
                var patrolPoint = new GameObject().transform;
                // Set the new game object's transform to the newly created patrol point.
                patrolPoint.localPosition = newPatrolPoint;
                // Store that value into our Transform[].
                patrolPoints[i] = patrolPoint;
            }
            // Initialize the enemy's patrol points.
            enemy.GetComponent<EnemyController>().patrolBehavior.SetPatrol(patrolPoints);
        }
        */
    }
}
