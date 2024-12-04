using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject TutorialUI;  // UI for "Score"
    public GameObject GameManager;  


    public void loadTutorialScene()
    {
        TutorialUI.SetActive(false);
        GameManager.SetActive(true);
    }

}

