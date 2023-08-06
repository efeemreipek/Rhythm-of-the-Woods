using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private bool isGameOver = false;
    private OrganController organController;

    private void Start()
    {
        Time.timeScale = 1f;
        organController = GameObject.Find("OrganController").GetComponent<OrganController>();
    }

    private void Update()
    {
        isGameOver = organController.GetIsGameOver();

        GameOver(isGameOver);
    }

    public void GameOver(bool condition)
    {
        if (condition)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print("esc");
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                print("res");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
