using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlane : MonoBehaviour {


    public static ClickPlane instance;
    int mask;

    void Awake()
    {
        mask = LayerMask.NameToLayer("ClickPlane");
        instance = this;
    }


    void Start () {
       

    }


    public Vector2Int? GetGridPosition()
    {
        var pos = GetPosition();
        if (pos == null) return null;

        return new Vector2Int(Mathf.RoundToInt(pos.Value.x), Mathf.RoundToInt(pos.Value.y));
    }

    public Vector3? GetGridWorldPosition()
    {
        var pos = GetGridPosition();
        if (pos == null) return null;

        return new Vector3(pos.Value.x,0f,pos.Value.y);
    }

    public Vector2? GetPosition()
    {
        
       
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            var pos = hit.point;
            return new Vector2(pos.x, pos.z);
        }

        return null;
     }
}
