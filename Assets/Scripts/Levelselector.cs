using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelselector : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
