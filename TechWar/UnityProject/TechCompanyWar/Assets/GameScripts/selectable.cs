using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class selectable : MonoBehaviour {

    public bool selected = false;
    public GameObject selectEffect;
    public bool movable;

    public float agentSpeed = 5;
    NavMeshAgent nav;
    public Transform followTarget;
	// Use this for initialization

	void Start () {
       
        nav = GetComponent<NavMeshAgent>();
        nav.speed = agentSpeed;
        selectEffect.SetActive(false);
        followTarget = null;
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

    public void setMovable(bool m)
    {
        movable = m;
    }

    public void Move(Vector3 pos)
    {
        if (movable)
        {
            followTarget = null;
            nav.SetDestination(pos);
        }
    }

    public void Move(Transform target)
    {
        if (movable)
        {
            followTarget = target;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //print("Enter trigger");
        if(other.transform == followTarget)
        {
            if (movable)
            {
                CitizenScript targetCitizen = other.GetComponent<CitizenScript>();
                if (targetCitizen != null)
                {
                    //Start convincing process
                    //Simply change color atm
                    GetComponent<SalesmanScript>().convince(targetCitizen);
                }
            }

            followTarget = null;
            nav.SetDestination(transform.position);
            Vector3 v = new Vector3(0,0,0);
            GetComponent<Rigidbody>().velocity = v;
        }
    }

    void Update()
    {
        if (followTarget != null)
        {
            nav.SetDestination(followTarget.position);
        }
    }
    
	
}
