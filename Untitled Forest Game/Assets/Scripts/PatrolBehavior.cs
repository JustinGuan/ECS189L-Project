using UnityEngine;
using UnityEngine.AI;

namespace Embers
{
    public class PatrolBehavior : EnemyController
    {
        [SerializeField] private Transform[] patrolPoints;
        private int currentPatrolIndex = 0;
        [SerializeField] private float patrolSpeed = 2f;
        private NavMeshAgent agent;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            SetDestinationToNextPatrolPoint();
        }

        public void EnemyPatrol()
        {
            //Debug.Log(currentState);
            if (currentState == EnemyState.Patrolling && !agent.pathPending && agent.remainingDistance < 0.1f)
            {
                SetDestinationToNextPatrolPoint();
            }
        }

        private void SetDestinationToNextPatrolPoint()
        {
            if (this.patrolPoints.Length == 0)
            {
                Debug.LogWarning("No patrol points assigned to the PatrolBehavior!");
                return;
            }

            agent.destination = patrolPoints[currentPatrolIndex].position;
            // Loops through the patrol points
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.speed = patrolSpeed;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player entered the trigger collider
                // Check if the player is within sight
                if (IsPlayerInSight())
                {
                    currentState = EnemyState.Chasing;
                    agent.destination = playerTransform.position;
                }
            }
        }
    }
}