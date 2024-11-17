using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour
{
    public static sound_manager instance;

    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
}
