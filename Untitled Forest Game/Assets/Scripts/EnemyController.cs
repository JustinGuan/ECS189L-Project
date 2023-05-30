using UnityEngine;

namespace Embers
{
    public class EnemyController : MonoBehaviour
    {
        // States
        private PatrolBehavior patrolBehavior;
        private ChaseBehavior chaseBehavior;
        protected AttackBehavior attackBehavior;
        public enum EnemyState
        {
            Patrolling,
            Chasing,
            Idle
        }
        protected EnemyState currentState;

        // Sight detection
        [SerializeField] public float detectionRange = 10f;
        [SerializeField] public float fieldOfViewAngle = 60f;
        protected Transform playerTransform;
        public LayerMask playerLayer;


        // Potentially change this to Awake()
        private void Start()
        {
            // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
            patrolBehavior = GetComponent<PatrolBehavior>();
            chaseBehavior = GetComponent<ChaseBehavior>();
            attackBehavior = GetComponent<AttackBehavior>();

            // Find the player reference
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // Activate the patrol behavior by default
            //ActivatePatrolBehavior();
            currentState = EnemyState.Patrolling;
        }

        private void Update()
        {
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

            // Check if the player is within attack range and activate attack behavior
            if (attackBehavior.CanAttack() && attackBehavior.IsPlayerInRange())
            {
                attackBehavior.EnemyAttack();
            }
        }

        protected bool IsPlayerInSight()
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