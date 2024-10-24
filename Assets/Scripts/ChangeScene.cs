using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void go_to_scene_2()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}