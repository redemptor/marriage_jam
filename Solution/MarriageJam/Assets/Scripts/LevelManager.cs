using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene scene;

    public void LoadLevel(string name)
    {
        Debug.Log("Scene carregada: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Sair");
        Application.Quit();
    }

}
