using Embers;
using UnityEngine;

namespace Embers
{
    public class MeleeAttack : MonoBehaviour
    {
        public Animator animator;
        [SerializeField] private KeyCode meleeKeyboard = KeyCode.Mouse0;

        public Transform attackPoint;
        public LayerMask enemyLayers;
        private bool isAttack = false;

        public float attackRange = 0.5f;
        public int attackDamage = 40;

        public void Attack()
        {
            // Attack animation
            animator.Play("Attack");

            // Detect enemies in attack range
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            // Damage enemies
            foreach (Collider enemy in hitEnemies)
            {
                // Perform damage on the enemy
                /*enemy.GetComponent<Enemy>().TakeDamage(attackDamage);*/
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            // Draw a wire sphere to visualize the attack range
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        private void Update()
        {
            if (Input.GetKeyDown(meleeKeyboard))
            {
                //isAttack = true;
                Attack();
            }

            else
            {
                //isAttack = false;
                //animator.SetBool("isAttack", isAttack);
            }
        }
    }
}
