using UnityEngine;
using System.Collections;

public class HomeMenu : MonoBehaviour {

    public GameObject loadingScreen;

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
