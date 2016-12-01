using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour {

    // Use this for initialization

    public string GameOverSceneName;

    public void goToGameOverScreen()
    {
        SceneManager.LoadScene(GameOverSceneName, LoadSceneMode.Single);
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
