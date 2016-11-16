using UnityEngine;
using System.Collections.Generic;

public class SelectManager : MonoBehaviour {

    private List<selectable> Selections = new List<selectable>();

    public TerrainCollider mapCollider;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //FOR SELECTING
        if (Input.GetMouseButtonDown(0))
        {
            if (!Input.GetKey(KeyCode.LeftShift) &&
                !Input.GetKey(KeyCode.RightShift))
            {
                if (Selections.Count > 0)
                {
                    foreach (selectable sel in Selections)
                    {
                        if (sel != null)
                            sel.Deselect();
                    }
                    Selections.Clear();
                }
            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
                return;
            selectable selectionHit = hit.transform.GetComponent<selectable>();
            if (selectionHit != null)
            {
                Selections.Add(selectionHit);
                selectionHit.Select();
            }
        }


        //FOR MOVING
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Check for if a unit is the target
            if (!Physics.Raycast(ray, out hit))
                return;
            selectable targetSelected = hit.transform.GetComponent<selectable>();
            if (targetSelected != null)
            {
                foreach(selectable s in Selections)
                {
                    s.Move(targetSelected.transform);
                }
                return;
            }


            if (!mapCollider.Raycast(ray, out hit, Mathf.Infinity))
                return;
            foreach(selectable s in Selections)
            {
                s.Move(hit.point);
            }
        }

	}
}
