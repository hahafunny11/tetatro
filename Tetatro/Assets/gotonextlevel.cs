using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class gotonextlevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //code for going between levels (will need to be updated
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "title")
            {
                SceneManager.LoadSceneAsync("Tetris");
            }
            else if (scene.name == "1to2")
            {
                SceneManager.LoadSceneAsync("Tet2");
            }
            else if (scene.name == "2to3")
            {
                SceneManager.LoadSceneAsync("Tetboss");
            }
            else if (scene.name == "3to1")
            {
                SceneManager.LoadSceneAsync("Tetris");
            }
            else if (scene.name == "GameOver")
            {
                SceneManager.LoadSceneAsync("title");
            }
        }
    }
}
