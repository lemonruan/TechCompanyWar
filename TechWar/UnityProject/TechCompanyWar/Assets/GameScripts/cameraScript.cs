using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {
    public float speed;
    public float height;
	// Use this for initialization
	void Start () {
        var pos = GetComponent<Transform>().position;
        pos.y = height;
        GetComponent<Transform>().position = pos;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(
            Input.GetAxis("Horizontal")*speed*Time.deltaTime,
            Input.GetAxis("Vertical") * speed * Time.deltaTime,
            0
            );
	}
}
