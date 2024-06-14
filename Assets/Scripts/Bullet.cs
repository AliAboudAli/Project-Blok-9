using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifetime = 5f; // Time after which the bullet destroys itself

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
        }
    }

    void Start()
    {
        // Start the coroutine to destroy the bullet after 'lifetime' seconds
        StartCoroutine(DestroyAfterLifetime());
    }

    public void SetDirection(Vector2 direction)
    {
        if (rb != null)
        {
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is missing when trying to set direction!");
        }
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    // Method that gets called when the bullet hits a collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet hits a player
        HealthPlayer healthPlayer = other.GetComponent<HealthPlayer>();
        if (healthPlayer != null)
        {
            healthPlayer.takeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Check if the bullet hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }

        // Destroy the bullet if it hits anything other than the player or enemy
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}