/*using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage = 40;
    public LayerMask targetLayer;
    public float attackRange = 0.5f;

    private bool isAttacking = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
        foreach (Collider enemy in hitEnemies)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}*/


using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	public int damage = 40;
	public LayerMask targetLayer;
	public float attackRange = 0.5f;

	private void Attack()
	{
		this.GetComponent<Animator>().Play("Attack");
		Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
		foreach (Collider enemy in hitEnemies)
		{
			Health health = enemy.GetComponent<Health>();
			if (health != null)
			{
				health.TakeDamage(damage);
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Attack();
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
