using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverSceneScript : MonoBehaviour {

    public Text WinnerText;
    PersistentDataScript data;

	// Use this for initialization
	void Start () {
        data = GameObject.FindGameObjectWithTag("GameOverData").GetComponent<PersistentDataScript>();

        if (data.getWinner() == 0)
        {
            WinnerText.text = "Winner is: Player";
        }else if(data.getWinner() == 1)
        {
            WinnerText.text = "Winner is AI";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
