using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // Destroy the bullet projectile after it hits the player
            Destroy(gameObject);
        }
    }
}
