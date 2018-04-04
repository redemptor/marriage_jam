using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Musica duplicada é deletada!");
		} else {
			print ("Se é a primeira música a torna persistente!");
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
	}
}
