using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objTransform;

    private float x;
    private float width;
    private float y;
    private float height;
    private float z;
    private float length;

    private float speed;

    private int damage;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        objTransform = this.gameObject.transform;

        width = objTransform.localScale.x;
        height = objTransform.localScale.y;
        length = objTransform.localScale.z;

        if (y > -2){
            speed = 7.5f;
        } else {
            // if this is the original bullet, don't let it move
            speed = 0.0f;
        }

        damage = 5;
    }

    public void Rotate(float x, float y)
    {
        Setup();

        objTransform.Rotate(0, y, 0, Space.World);
        objTransform.Rotate(x, 0, 0, Space.Self);

        rb.velocity = objTransform.TransformDirection(Vector3.forward) * speed;
    }

    void Update()
    {
        x = objTransform.position.x;
        y = objTransform.position.y;
        z = objTransform.position.z;

        // if this is not the original,
        if (y > -2){
            if (x - width / 2 < -12.5f || x + width / 2 > 12.5f ||
                y - height / 2 < 0 || y + height / 2 > 10 ||
                z - length / 2 < -10 || z + length / 2 > 16){
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);

        Target target = other.gameObject.GetComponent<Target>();
        target.TakeDamage(damage);
    }
}
