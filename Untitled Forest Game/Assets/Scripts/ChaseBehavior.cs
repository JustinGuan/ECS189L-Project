using UnityEngine;
using UnityEngine.AI;

namespace Embers
{
    public class ChaseBehavior : EnemyController
    {
        [SerializeField] public float chaseSpeed = 4f;
        [SerializeField] public float outOfSightDuration = 3f;
        private float timeSincePlayerOutOfSight = 0f;


        private void Start()
        {
            //agent = GetComponent<NavMeshAgent>();
        }

        public void EnemyChase()
        {
            if (currentState == EnemyState.Chasing)
            {
                agent.destination = playerTransform.position;
                agent.speed = chaseSpeed;


                if (IsPlayerInSight())
                {
                    timeSincePlayerOutOfSight = 0f;
                }
                else
                {
                    timeSincePlayerOutOfSight += Time.deltaTime;
                    // Check if the player has been out of sight for the specified duration
                    if (timeSincePlayerOutOfSight >= outOfSightDuration)
                    {
                        // Deactivate chase behavior
                        currentState = EnemyState.Patrolling;
                    }
                }
            }
        }
    }
}