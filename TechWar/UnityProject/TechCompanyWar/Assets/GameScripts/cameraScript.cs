using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {
    public float speed;
    public float height;

    //3 for up, 4 for down, 2 for left, 1 for right
    int move;

	// Use this for initialization
	void Start () {
        var pos = GetComponent<Transform>().position;
        pos.y = height;
        GetComponent<Transform>().position = pos;
        move = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(
            Input.GetAxis("Horizontal")*speed*Time.deltaTime,
            Input.GetAxis("Vertical") * speed * Time.deltaTime,
            0
            );
        if (move > 0)
        {
            if(move == 1)
            {
                transform.Translate(1*speed*Time.deltaTime, 0,0);
            }else if(move == 2)
            {
                transform.Translate((-1) * speed * Time.deltaTime, 0, 0);
            }else if(move == 3)
            {
                transform.Translate(0,1 * speed * Time.deltaTime, 0);
            }else if(move == 4)
            {
                transform.Translate(0, (-1) * speed * Time.deltaTime, 0);
            }
        }
	}

    public void moveCamera(int direction)
    {
        move = direction;
    }

    public void MoveStop()
    {
        move = 0;
    }
}
