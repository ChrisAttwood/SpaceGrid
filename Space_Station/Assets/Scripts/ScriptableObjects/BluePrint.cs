using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BluePrint : ScriptableObject {

    public GameObject Structure;
    
    public BuildType BuildType;

    //True is it can be walked on, false for stuff like walls.
    public bool Space;
}
