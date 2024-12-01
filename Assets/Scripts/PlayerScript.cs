using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public string playerColour="Blue";
   //private int deathCount = 0;

    public bool hasKey = false;
    public bool isDead = false;
    public Animator animator;
    // Event for when the key state changes
    public event Action<bool> OnKeyChanged;

    //private Vector3 startPosition;
    //PlayerMovementScript playerMovementScript;

    //void Start()
    //{
    //    startPosition = transform.position; // Store the starting position
    //}


    private void Update()
    {
        if (isDead)
        {
            if (animator.GetBool("isDead") == false)
            {
                animator.SetBool("isDead", true);
                //deathCount++;
                //Debug.Log(deathCount);
                StartCoroutine(WaitForDeathAnimation());
            }
        }
    }

    // Coroutine to wait for the "isDead" animation to complete
    private IEnumerator WaitForDeathAnimation()
    {
        // Wait until the animation has finished
        yield return new WaitForSeconds(2f);
         // After the animation finishes, reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Method to invoke the event (optional wrapper for better encapsulation)
    public void TriggerKeyChange(bool newKeyState)
    {
        hasKey = newKeyState;
        OnKeyChanged?.Invoke(hasKey);  // Notify listeners
    }
}
