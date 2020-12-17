using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); //Go back to Main Menu
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
