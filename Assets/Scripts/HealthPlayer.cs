using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public float health;
    public float healthMax = 100;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {     
        slider = GetComponent<Slider>();
        health = healthMax;
        // slider.maxValue = healthMax;
        // slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Remove takeDamage call from Update, it's not needed here.
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0; // Ensure health doesn't go below 0
            Die();
        }
        // Update the health slider if necessary
        if (slider != null)
        {
            slider.value = health;
        }
    }

    public void Die()
    {
        Debug.Log("Ooof player died");
        Destroy(gameObject);
    }
}