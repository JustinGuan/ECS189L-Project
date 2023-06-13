using UnityEngine;

namespace Embers
{
    public class AttackBehavior : EnemyController
    {
        [SerializeField] public int attackDamage = 10;
        [SerializeField] public float attackCooldown = 2f;
        [SerializeField] public float attackRange = 2f;
        private bool canAttack = true;

        public bool CanAttack()
        {
            // Check if the Evil Spirit is currently able to attack based on cooldown
            return canAttack;
        }

        public bool IsPlayerInRange()
        {
            // Check if the player is within attack range
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            return distanceToPlayer <= attackRange;
        }

        public void EnemyAttack()
        {
            // Apply damage to the player or trigger any desired attack behavior
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().TakeDamage(attackDamage);

            // Set attack on cooldown
            canAttack = false;
            Invoke(nameof(ResetAttackCooldown), attackCooldown);
        }

        private void ResetAttackCooldown()
        {
            // Reset attack cooldown
            canAttack = true;
        }
    }
}