using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene scene;

    public void LoadStartScreen()
    {
        LoadLevel("Start Screen");
    }

    public void LoadLevel(string name)
    {
        Debug.Log("Scene loaded: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("close...");
        Application.Quit();
    }

    public static ActionActor GetRandomPlayerAtScene()
    {
        var players = FindObjectsOfType<Player>().Where(x => x.Alive).ToArray();

        if (players.Length == 0)
        {
            return null;
        }

        var player = players[Random.Range(0, players.Length)];
        return player.GetComponent<ActionActor>();
    }

}
