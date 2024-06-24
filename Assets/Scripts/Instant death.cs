using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    // Reference to the spike GameObject
    public GameObject spike;
    // Reference to the player GameObject
    public GameObject player;

    // This method is called once per frame
    private void Update()
    {
        // Check if the player's Collider2D is overlapping with the spike's Collider2D
        if (spike.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            // Destroy the player object if they touch the spike
            Destroy(player);
            Debug.Log("Player died");
        }
    }
}