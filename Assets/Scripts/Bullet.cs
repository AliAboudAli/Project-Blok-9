using UnityEngine;

// Klasse voor kogels
public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    private Vector3 direction;

    public void SetDirection(Vector3 direction)
    {
        // de richting van de kogel (genormaliseerd)
        this.direction = direction.normalized;
    }

    // Start is called before the first frame update
    private void Update()
    {
        // Richting doorgeven van de kogel berekenen
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Methode die wordt aangeroepen wanneer de kogel een trigger collider raakt
    private void OnTriggerEnter(Collider other)
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