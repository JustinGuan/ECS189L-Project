using Embers;
using UnityEngine;

namespace embers
{
	public class MeleeAttack : MonoBehaviour
	{
		public Animator animator;

		public Transform attackPoint;
		public LayerMask enemyLayers;

		public float attackRange = 0.5f;
		public int attackDamage = 40;
		public void Attack()
		{
			//Attack animation
			animator.SetTrigger("Attack");

			//Detect enemies in attack range
			Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

			//Damage
			foreach (Collider enemy in hitEnemies)
			{
				// enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
			}

		}

		private void OnDrawGizmosSelected()
		{
			if (attackPoint == null)
				return;

			Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Attack();
			}
		}
	}

}