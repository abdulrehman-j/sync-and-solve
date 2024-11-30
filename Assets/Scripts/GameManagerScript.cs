using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Transform spawnPos;  // Reference to the spawn position in the scene

    public void SpawnPlayer(PlayerScript playerScript)
    {
        // Get the saved color name from PlayerScript
        string colorName = playerScript.playerColour;  // e.g., "Red", "Blue", "Green"
        Debug.Log("Player prefab: " + colorName);

        // Try to load the prefab from the Resources folder
        GameObject playerPrefab = Resources.Load<GameObject>($"PlayerPrefab/{colorName}Player");

        if (playerPrefab != null)
        {
            // Instantiate the player prefab at a specific position and rotation
            Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);  // Use spawnPos for position and rotation
        }
        else
        {
            Debug.LogWarning("Player prefab not found: " + colorName);
        }
    }

    private void Start()
    {
        // Get the PlayerScript from the player GameObject in the scene
        PlayerScript playerScript = GetComponent<PlayerScript>(); // This gets the first PlayerScript in the scene

        if (playerScript != null)
        {
            // Call SpawnPlayer with the playerScript
            SpawnPlayer(playerScript);
        }
        else
        {
            Debug.LogWarning("PlayerScript not found in the scene.");
        }
    }
}
