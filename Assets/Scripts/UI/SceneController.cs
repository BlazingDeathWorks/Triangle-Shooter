using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject mainmenu;
    // Start is called before the first frame update

    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings() {
        mainmenu.SetActive(false);
    }
}
