using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlane : MonoBehaviour {

    LayerMask mask;

    // Use this for initialization
    void Start () {
        mask = LayerMask.NameToLayer("ClickPlane");

    }
	
	// Update is called once per frame
	void Update () {
        CheckClick();

    }


    void CheckClick()
    {
        RaycastHit hit;
       //Ray ray = Camera..ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
    }
}
