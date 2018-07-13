using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlane : MonoBehaviour {


    public static ClickPlane instance;
    LayerMask mask;

    void Awake()
    {
        instance = this;
    }


    void Start () {
        mask = LayerMask.NameToLayer("ClickPlane");

    }
	
	// Update is called once per frame
	void Update () {
        CheckClick();

    }

    //public Vector2Int GetGridPosition()
    //{

    //}


    void CheckClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
           
        }
        
     }
}
