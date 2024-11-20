using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterCustomization : MonoBehaviour
{
    [System.Serializable]
    public class CharacterOption
    {
        public string name;           // Character name
        public Color buttonColor;     // Button color
        public GameObject characterPrefab; // Prefab (with SpriteRenderer)
    }

    public CharacterOption[] characterOptions; // Array of character options
    public Image previewImage;             // Image UI element for previewing sprite
    public Transform buttonParent;         // Parent object for the buttons
    public GameObject buttonPrefab;        // Prefab for the buttons
    public TextMeshProUGUI previewText;           // Text UI element for displaying character name on preview screen


    void Start()
    {
        // Set the first character as the default preview
        if (characterOptions.Length > 0)
        {
            ShowCharacterPreview(characterOptions[0].characterPrefab);
            DisplayCharacterName(characterOptions[0].name, characterOptions[0].buttonColor);
        }

        foreach (var option in characterOptions)
        {
            CreateColorButton(option);
        }
    }

    void CreateColorButton(CharacterOption option)
    {
        // Instantiate the button
        GameObject button = Instantiate(buttonPrefab, buttonParent);

        // Set button color
        Button btn = button.GetComponent<Button>();
        Image btnImage = button.GetComponent<Image>();
        btnImage.color = option.buttonColor;


        // Get the button's ColorBlock
        ColorBlock colorBlock = btn.colors;

        // Set the colors for different states
        colorBlock.normalColor = option.buttonColor;           // Normal state color
        colorBlock.highlightedColor = option.buttonColor * 0.6f; // Hover/Highlighted state color (lighten it a bit)
        colorBlock.pressedColor = option.buttonColor * 1.4f;    // Pressed state color (darken it a bit)
        colorBlock.selectedColor=colorBlock.pressedColor * 1.6f;
        colorBlock.disabledColor = Color.gray;                  // Disabled state color (optional)

        // Apply the color block back to the button
        btn.colors = colorBlock;

        // Add button click functionality
        btn.onClick.AddListener(() => {
            Debug.Log("Button clicked for " + option.name);  // Debug log to confirm click

            // Show character preview and update button color
            ShowCharacterPreview(option.characterPrefab);
            DisplayCharacterName(option.name, option.buttonColor);
        });
    }

    void ShowCharacterPreview(GameObject characterPrefab)
    {
        // Extract the sprite from the prefab's SpriteRenderer component
        SpriteRenderer spriteRenderer = characterPrefab.GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Set the sprite of the preview image
            previewImage.sprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not found in prefab!");
        }
    }

    void DisplayCharacterName(string name, Color textColour)
    {
        // Display the character name on top of the preview screen
        previewText.text = name;
        previewText.color = textColour;
    }

}
