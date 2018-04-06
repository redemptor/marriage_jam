using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene scene;

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
        GameObject[] players = GameObject.FindGameObjectsWithTag(GlobalFields.TAGS.Player.ToString());
        GameObject player = players[Random.Range(0, players.Length)];
        return player.GetComponent<ActionActor>();
    }

}
