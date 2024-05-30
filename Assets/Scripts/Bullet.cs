using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("logical")]
    public float bulletSpeed = 10f;
    public string AI = "AI";
    public Rigidbody2D rb;
    public Player player;
    public GameObject bullet;
    public Rigidbody2D bulletRb;
    public GameObject[] enemies;
    
    
    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        bulletRb.velocity = transform.forward * bulletSpeed;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 1000);
        if (Physics2D.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            HandleHit(hit.collider);
        }
    }

    private void HandleHit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //maak deze hit functieaf nadat je de enemies script heb geschreven met health
            Debug.Log("Hit enemy");
        }
    }

    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction.normalized * bulletSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
