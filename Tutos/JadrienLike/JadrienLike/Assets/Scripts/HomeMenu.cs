using UnityEngine;
using System.Collections;

public class HomeMenu : MonoBehaviour {

    public GameObject loadingScreen;

    void Start()
    {
        AudioSource intro = GetComponentInChildren<AudioSource>();
        intro.Play();
    }

    public void LoadScene(int id)
    {
        loadingScreen.SetActive(true);
        Application.LoadLevel(id);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
