using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Color playerColour;
    private int deathCount = 0;

    public bool hasKey = false;
    public bool isDead = false;
    public Animator animator;

    private void Update()
    {
        if (isDead)
        {
            animator.SetBool("isDead", true);
        }
    }

}
