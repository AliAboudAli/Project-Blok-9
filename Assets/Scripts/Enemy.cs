using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float patrolWaitTime = 2f;
    public float shootCooldown = 1f;
    public int maxAmmo = 20;
    public float detectRange = 5f;
    public int maxHealth = 100;
    public Rigidbody2D rb;
    private Transform player;
    private Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private int currentAmmo;
    private int health;

    private bool patrolling = true;
    private bool playerDetected = false;
    private float shootTimer = 0f;

    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        patrolPoints = new Transform[2]; // Voorbeeld: twee patrouillepunten instellen
        patrolPoints[0] = new GameObject().transform;
        patrolPoints[0].position = new Vector3(0, 0, 0);
        patrolPoints[1] = new GameObject().transform;
        patrolPoints[1].position = new Vector3(10, 0, 0);

        currentAmmo = maxAmmo;
        health = maxHealth;

        StartCoroutine(Patrol());
    }

    void Update()
    {
        if (playerDetected)
        {
            // Richting naar de speler
            Vector3 direction = (player.position - transform.position).normalized;

            // Schiet op de speler als de cooldown voorbij is en er kogels zijn
            if (shootTimer <= 0 && currentAmmo > 0)
            {
                Shoot(direction);
                shootTimer = shootCooldown;
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }

            // Beweeg richting de speler
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
        else if (patrolling)
        {
            // Patrouille richting volgend patrouille punt
            Vector3 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
            transform.position += direction * patrolSpeed * Time.deltaTime;

            // Als de vijand bijna bij het patrouille punt is, wacht dan even
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
            {
                StartCoroutine(WaitAtPatrolPoint());
            }
        }
    }

    void Shoot(Vector3 direction)
    {
        Debug.Log("Pew! Pew!");
        currentAmmo--;
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            patrolling = true;
            playerDetected = false;
            yield return null;

            // Wissel van patrouille punt
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    IEnumerator WaitAtPatrolPoint()
    {
        patrolling = false;
        yield return new WaitForSeconds(patrolWaitTime);
        StartCoroutine(Patrol());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(damage);
        }
    }

    public void Die(int damage)
    {
        health -= damage;

        if (gameObject.CompareTag("Enemy"))
        {
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}