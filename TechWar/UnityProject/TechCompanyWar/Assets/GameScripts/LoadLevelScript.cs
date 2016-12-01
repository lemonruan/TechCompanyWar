using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour {


	public void loadGameLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
