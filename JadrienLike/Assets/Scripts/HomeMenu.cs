using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour {

    public GameObject loadingScreen;
    private GameController _gameController;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        AudioSource intro = GetComponentInChildren<AudioSource>();
        intro.Play();
    }

    public void LoadScene(int id)
    {
        loadingScreen.SetActive(true);
        //Application.LoadLevel(id);
        SceneManager.LoadScene(1);
        _gameController.DisplayUI();
        _gameController.DisplayPlayer();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
