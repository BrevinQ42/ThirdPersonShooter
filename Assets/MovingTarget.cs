using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : Target
{
    private Transform objTransform;
    private float y;

    private float xVelocity;
    private float yVelocity;
    private float zVelocity;

    void Start()
    {
        HP = 5 * numberOfHitsToDestroy;

        objTransform = this.gameObject.transform;

        xVelocity = Random.Range(-1, 1);
        yVelocity = -0.75f;
        zVelocity = Random.Range(-1, 1);
    }

    void Update()
    {
        y = objTransform.position.y;

        if (y > -48){
            objTransform.position += new Vector3(xVelocity * Time.deltaTime, yVelocity * Time.deltaTime, zVelocity * Time.deltaTime);

            if (y <= 3){
                this.TakeDamage(HP);
            }
        }
    }
}
