using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string playerColour="Blue";
    private int deathCount = 0;

    public bool hasKey = false;
    public bool isDead = false;
    public Animator animator;


    // Event for when the key state changes
    public event Action<bool> OnKeyChanged;
    private void Update()
    {
        if (isDead)
        {
            animator.SetBool("isDead", true);
        }
    }

    // Method to invoke the event (optional wrapper for better encapsulation)
    public void TriggerKeyChange(bool newKeyState)
    {
        hasKey = newKeyState;
        OnKeyChanged?.Invoke(hasKey);  // Notify listeners
    }


}
