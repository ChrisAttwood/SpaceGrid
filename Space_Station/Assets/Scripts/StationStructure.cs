using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//One per scene. Using static instance for easy access.
public class StationStructure : MonoBehaviour {

    public static StationStructure instance;

    public Dictionary<Vector2Int, bool> Spaces;

    private void Awake()
    {
        instance = this;
        Spaces = new Dictionary<Vector2Int, bool>();
    }


}
