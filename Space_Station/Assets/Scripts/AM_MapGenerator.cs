using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AM_MapGenerator : MonoBehaviour {

    public GameObject Asteroid;
    public int numberGenerated;
    public float minBound;
    public float maxBound;
    public float depth;
    public GameObject planet;
    private Vector3 spawnOrigin;
    public float asteroidSizeVariance;

	void Start () {

        GenerateAsteroidBelt();
	}

    // Centres the spawn origin on the planet
    void Centre()
    {
        spawnOrigin = planet.transform.position;
    }

    // Generates a number of asteroids in a ring around the planet
    void GenerateAsteroidBelt()
    {
        for (int i = 0; i < numberGenerated; i++)
        {
            // Starting at the centre, find a random direction in which to spawn the asteroid
            float randomDirection = Random.Range(0.0f, 1.0f) * 360f;
            // The decide a random distance
            float randomDistance = Random.Range(minBound, maxBound);
            // Maths stuff that I looked up but don't really understand why it works
            float newX = spawnOrigin.x + randomDistance * Mathf.Cos(randomDirection);
            float newZ = spawnOrigin.z + randomDistance * Mathf.Sin(randomDirection);
            // Then add some depth and spawn the asteroid
            float newY = Random.Range(-depth / 2f, depth / 2f);
            GameObject goAsteroid = GameObject.Instantiate(Asteroid, new Vector3(newX, newY, newZ), Random.rotation);
            MixAsteroidUp(goAsteroid);
        }
    }

    // Just mixes up the asteroid
    void MixAsteroidUp(GameObject thisAsteroid)
    {
        float sizeMod = 1f + Random.Range(-asteroidSizeVariance / 2, asteroidSizeVariance / 2);
        thisAsteroid.transform.localScale = thisAsteroid.transform.localScale * sizeMod;
    }

}
