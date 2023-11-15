using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public MovingTarget Asteroid;

    private float spawnCooldown;

    private float x;
    private float y;
    private float z;

    void Start()
    {
        spawnCooldown = 1.0f;

        y = 15;
    }

    void Update()
    {
        if (spawnCooldown > 0){
            spawnCooldown -= Time.deltaTime;
        } else {
            x = Random.Range(-5, 5);
            z = Random.Range(-5, 5);

            GameObject newAsteroid = Instantiate(Asteroid.gameObject,
                                            new Vector3(x, y,z),
                                            new Quaternion(0,0,0,0));

            spawnCooldown = 5.0f;
        }
    }
}
