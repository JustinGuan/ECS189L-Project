using UnityEngine;
using UnityEngine.AI;

namespace Embers
{
	public class PatrolBehavior : MonoBehaviour
	{
		[SerializeField] private EnemyController enemyController;
		[SerializeField] private Transform[] patrolPoints;
		private int currentPatrolIndex = 0;
		[SerializeField] private float patrolSpeed = 2f;
		private LocationTracker tracker;

		private void Awake()
		{
			tracker = GameObject.Find("World Generator").GetComponent<LocationTracker>();
		}

		private void Start()
		{
			if (enemyController.agent == null || tracker == null)
			{
				tracker = GameObject.Find("World Generator").GetComponent<LocationTracker>();
			}
			SetPatrolPoints();
			Invoke("SetDestinationToNextPatrolPoint", 5);
		}

		public void EnemyPatrol()
		{
			// Check if the player is within sight
			if (enemyController.IsPlayerInSight())
			{
				enemyController.currentState = EnemyState.Chasing;
				enemyController.agent.destination = enemyController.playerTransform.position;
				return;
			}

			if (enemyController.currentState == EnemyState.Patrolling && !enemyController.agent.pathPending && enemyController.agent.remainingDistance < 0.1f)
			{
				SetDestinationToNextPatrolPoint();
			}

			if (enemyController.patrolLock)
			{
				if (enemyController.patrolLockCurrent >= enemyController.patrolLockDuration)
				{
					enemyController.patrolLock = false;
					enemyController.patrolLockCurrent = 0f;
				}
				else
				{
					enemyController.patrolLockCurrent += Time.deltaTime;
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

			enemyController.agent.destination = patrolPoints[currentPatrolIndex].position;
			// Loops through the patrol points
			currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
			enemyController.agent.speed = patrolSpeed;
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.CompareTag("Player") && !enemyController.patrolLock)
			{
				// Player entered the trigger collider
				// Check if the player is within sight
				if (enemyController.IsPlayerInSight())
				{
					enemyController.currentState = EnemyState.Chasing;
					enemyController.agent.destination = enemyController.playerTransform.position;
				}
			}
		}

		private void SetPatrolPoints()
		{
			// List to hold our new patrol points.
			this.patrolPoints = new Transform[4];
			// get our spawner's and fire's x and z coords.
			float spawnerX = transform.parent.position.x;
			float spawnerZ = transform.parent.position.z;
			float fireX = tracker.GetFireplacePos().x;
			float fireZ = tracker.GetFireplacePos().z;
			// Get the fire's radius.
			float curFireRadius = enemyController.flameRadius;
			// Set the 4 patrol points around the spawner.
			// The radii will be set at 0, pi/2, pi, and 3pi/2 degrees.
			float patrolRadius = 20.0f;
			for (int i = 0; i < 4; i++)
			{
				// There will be 4 patrol points, each at 2/3 of its max radius spawn.
				float theta = ((float)i * Mathf.PI) / 2.0f;
				float x = spawnerX + (patrolRadius * Mathf.Cos(theta));
				float z = spawnerZ + (patrolRadius * Mathf.Sin(theta));
				// Edge case: patrol point is within the fire radius, change the value accordingly.
				Vector3 newPatrolPoint = new Vector3(x, transform.parent.position.y, z);
				// If our patrol point falls within the fire's safe zone, change either x or z value.
				if (Vector3.Distance(newPatrolPoint, tracker.GetFireplacePos()) <= curFireRadius)
				{
					// Update the patrol radius, so that it's at the edge of the fire radius.
					patrolRadius -= curFireRadius;
					// Determine which value (x or z) needs to be modfied.
					if (Mathf.Abs(fireX - spawnerX) > Mathf.Abs(fireZ - spawnerZ))
					{
						x = spawnerX + (patrolRadius * Mathf.Cos(theta));
					}
					else
					{
						z = spawnerZ + (patrolRadius * Mathf.Sin(theta));
					}
					// Re-initialize one of our patrol points.
					newPatrolPoint = new Vector3(x, transform.parent.position.y, z);
				}
				// Create a new gameobject, and grab it's transform.
				var patrolPoint = new GameObject().transform;
				// Set the new game object's transform to the newly created patrol point.
				patrolPoint.localPosition = newPatrolPoint;
				Debug.Log(i);
				// Store that value into our Transform[].
				this.patrolPoints[i] = patrolPoint;
			}
		}
	}
}