using UnityEngine;

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

        // Sight detection
        [SerializeField] public float detectionRange = 10f;
        [SerializeField] public float fieldOfViewAngle = 60f;
        public Transform playerTransform;
        public LayerMask playerLayer;

        private void Start()
        {
            // Get references to the PatrolBehavior, ChaseBehavior, and AttackBehavior components
            //this.patrolBehavior = GameObject.FindGameObjectWithTag("Script Home").GetComponent<PatrolBehavior>();
            this.patrolBehavior = GetComponent<PatrolBehavior>();
            this.chaseBehavior = GetComponent<ChaseBehavior>();
            this.attackBehavior = GetComponent<AttackBehavior>();

            // Find the player reference
            this.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // Activate the patrol behavior by default
            this.currentState = EnemyState.Patrolling;
        }

        private void Update()
        {
            switch (this.currentState)
            {
                case EnemyState.Patrolling:
                    // Handle patrolling behavior
                    this.patrolBehavior.EnemyPatrol();
                    break;

                case EnemyState.Chasing:
                    // Handle chasing behavior
                    this.chaseBehavior.EnemyChase();
                    break;

                case EnemyState.Idle:
                    // Handle idle behavior
                    break;
            }

            // Check if the player is within attack range and activate attack behavior
            if (this.attackBehavior.CanAttack() && this.attackBehavior.IsPlayerInRange() && IsPlayerInSight())
            {
                this.attackBehavior.EnemyAttack();
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
    }
}
