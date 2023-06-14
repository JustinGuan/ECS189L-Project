using UnityEngine;
using UnityEngine.AI;

namespace Embers
{
	public class ChaseBehavior : MonoBehaviour
	{
		[SerializeField] private EnemyController enemyController;
		[SerializeField] public float chaseSpeed = 4f;
		[SerializeField] public float outOfSightDuration = 3f;
		private float timeSincePlayerOutOfSight = 0f;


		private void Start()
		{
			//agent = GetComponent<NavMeshAgent>();
		}

		public void EnemyChase()
		{
			if (enemyController.currentState == EnemyState.Chasing)
			{
				enemyController.agent.destination = enemyController.playerTransform.position;
				enemyController.agent.speed = chaseSpeed;


				if (enemyController.IsPlayerInSight())
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
						enemyController.currentState = EnemyState.Patrolling;
					}
				}
			}
		}
	}
}