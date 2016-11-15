using UnityEngine;
using System.Collections;

public class selectable : MonoBehaviour {

    public bool selected = false;
    public GameObject selectEffect;

    public float agentSpeed = 5;
    NavMeshAgent nav;
	// Use this for initialization

	void Start () {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = agentSpeed;
        selectEffect.SetActive(false);

	}
	
	public void Select()
    {
        selected = true;
        selectEffect.SetActive(true);
    }

    public void Deselect()
    {
        selected = false;
        selectEffect.SetActive(false);
    }

    public void Move(Vector3 pos)
    {
        nav.SetDestination(pos);
    }

    
	
}
