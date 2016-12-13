using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour {

    public Button Text1;
    public Button Text2;
    public Button Text3;
    public Button Text4;
    public Button Text5;
    public Button Text6;
    public Button Text7;

    // Use this for initialization
    void Start () {
        //Text1.gameObject.SetActive(false);
        Text2.gameObject.SetActive(false);
        Text3.gameObject.SetActive(false);
        Text4.gameObject.SetActive(false);
        Text5.gameObject.SetActive(false);
        Text6.gameObject.SetActive(false);
        Text7.gameObject.SetActive(false);
    }

    public void goToNextText()
    {
        if (Text1.gameObject.activeSelf)
        {
            Text2.gameObject.SetActive(true);
            Text1.gameObject.SetActive(false);
        } else if (Text2.gameObject.activeSelf)
        {
            Text3.gameObject.SetActive(true);
            Text2.gameObject.SetActive(false);
        } else if (Text3.gameObject.activeSelf)
        {
            Text4.gameObject.SetActive(true);
            Text3.gameObject.SetActive(false);
        }
        else if (Text4.gameObject.activeSelf)
        {
            Text5.gameObject.SetActive(true);
            Text4.gameObject.SetActive(false);
        }
        else if (Text5.gameObject.activeSelf)
        {
            Text6.gameObject.SetActive(true);
            Text5.gameObject.SetActive(false);
        }
        else if (Text6.gameObject.activeSelf)
        {
            Text7.gameObject.SetActive(true);
            Text6.gameObject.SetActive(false);
        } else if (Text7.gameObject.activeSelf)
        {

            Text7.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
