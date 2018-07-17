using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public GameObject satelliteOf; // The object which this will orbit around
    public float orbitSpeed;

    void Update () {
        transform.RotateAround(satelliteOf.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
	}
}
