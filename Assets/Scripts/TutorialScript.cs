using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public GameObject TutorialUI;  // UI for "Score"
    public GameObject GameManager;
    public GameObject Background;

    public void loadTutorialScene()
    {
        TutorialUI.SetActive(false);
        GameManager.SetActive(true);
        Background.SetActive(true);
    }

}

