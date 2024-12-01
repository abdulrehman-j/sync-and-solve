using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public Image keyImage;
    public Sprite keyObtainedSprite;
    public Sprite keyMissingSprite;

    private PlayerScript playerScript;

    private IEnumerator Start()
    {
        // Wait until the player is spawned in the scene
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null; // Wait one frame
        }

        // Once the player is found, get the PlayerScript component
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.OnKeyChanged += UpdateKeyUI;
            UpdateKeyUI(playerScript.hasKey);
        }
        else
        {
            Debug.LogError("PlayerScript not found in the scene.");
        }
    }
 
    private void OnDestroy()
    {
        if (playerScript != null)
        {
            playerScript.OnKeyChanged -= UpdateKeyUI;
        }
    }

    private void UpdateKeyUI(bool hasKey)
    {
        keyImage.sprite = hasKey ? keyObtainedSprite : keyMissingSprite;
    }
}
