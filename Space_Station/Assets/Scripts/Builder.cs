using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

    public GameObject Block;

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
           
            var pos = ClickPlane.instance.GetGridWorldPosition();
            if (pos != null)
            {
                var block = Instantiate(Block);
                block.transform.position = pos.Value;
            }
        }
	}
}
