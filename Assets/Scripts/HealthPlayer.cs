using UnityEngine.UI;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public float health;
    public float healthMax = 100;
    private Slider Slider;

    private Bullet Bullet;
    // Start is called before the first frame update
    void Start()
    {     
        Slider = GetComponent<Slider>();
        health = healthMax;
       // Slider.maxValue = healthMax;
//        Slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        takeDamage(0);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            healthMax = 0;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Ooof player died");
        Destroy(gameObject);
    }
}
