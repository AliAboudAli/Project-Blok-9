using UnityEngine;

// Klasse voor kogels
public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public Rigidbody2D rb;


    public void Start()
    {
        
        print(rb);
        if (rb == null)
        {
            Debug.Log("we need a rigidbody");
        }
    }
    public void SetDirection(Vector3 direction)
    {
        // Apply force in the specified direction
        //rb = GetComponent<Rigidbody2D>();
        print(direction);
        print(rb);
        rb.AddForce(direction.normalized * speed);
    }
    
    // Methode die wordt aangeroepen wanneer de kogel een collider raakt
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controleer of de kogel een vijand raakt
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Pas aan de schade toe aan de vijand
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // Vernietig de kogel
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            // Vernietig de kogel als deze iets anders raakt dan de speler
            Destroy(gameObject);
        }
    }
}