using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && checkpoint != null)
        {
            // Load the next scene
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the player and spawn at the checkpoint
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null && checkpoint != null)
        {
            Player player = playerObject.GetComponent<Player>();
            player.transform.position = checkpoint.transform.position;
            player.transform.rotation = checkpoint.transform.rotation;
        }

        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}