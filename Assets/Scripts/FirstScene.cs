using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    [SerializeField]
    private float delaybeforeloading = 5f;
    [SerializeField]
    private string scenetoload;

    private float timeelapsed;

    void Update()
    {
        timeelapsed += Time.deltaTime;
       
        if (timeelapsed >= delaybeforeloading)
        {
            SceneManager.LoadScene(scenetoload);
        }
        
    }
}
