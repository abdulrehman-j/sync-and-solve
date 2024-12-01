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

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store the starting position
    }

    private void Update()
    {
        if (isDead)
        {
            if (animator.GetBool("isDead") == false)
            {
                animator.SetBool("isDead", true);
                deathCount++;
            }
        }
    }

}
