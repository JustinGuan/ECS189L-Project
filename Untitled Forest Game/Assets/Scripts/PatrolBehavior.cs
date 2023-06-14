using UnityEngine;
using UnityEngine.AI;

namespace Embers
{
    public class PatrolBehavior : EnemyController
    {
        [SerializeField] private Transform[] patrolPoints;
        private int currentPatrolIndex = 0;
        [SerializeField] private float patrolSpeed = 2f;


        private void Start()
        {
            patrolPoints = new Transform[4];
            for (int i = 0; i < 4; i++)
            {
                patrolPoints[i] = GameObject.FindGameObjectWithTag("World Generator").GetComponent<WorldGenerator>().patrolPoints[enemyType, i];
            }

            SetDestinationToNextPatrolPoint();
        }

        public void EnemyPatrol()
        {
            if (currentState == EnemyState.Patrolling && !agent.pathPending && agent.remainingDistance < 0.1f)
            {
                SetDestinationToNextPatrolPoint();
            }

            if (patrolLock)
            {
                if (patrolLockCurrent >= patrolLockDuration)
                {
                    patrolLock = false;
                    patrolLockCurrent = 0f;
                }
                else
                {
                    patrolLockCurrent += Time.deltaTime;
                }
            }
        }

        private void SetDestinationToNextPatrolPoint()
        {
            if (patrolPoints.Length == 0)
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
            if (other.CompareTag("Player") && !patrolLock)
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

        // Sets the patrol points for the enemy.
        public void SetPatrol(Transform[] tf)
        {
            // Copy and pastes array contents from tf into patrolPoints.
            System.Array.Copy(tf, this.patrolPoints, 4);
        }
    }
}