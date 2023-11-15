using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objTransform;

    private float y;

    private float speed;

    private int damage;

    public ParticleSystem particleImpact;
    public AudioSource bulletHit;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        objTransform = this.gameObject.transform;

        if (y > -48){
            speed = 10.0f;
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
        y = objTransform.position.y;

        // if this is not the original,
        if (y > -48){
            StartCoroutine(deleteObject());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);

        ParticleSystem newParticles = Instantiate(particleImpact.gameObject, objTransform.position, 
            objTransform.rotation).GetComponent<ParticleSystem>();

        newParticles.Emit(8);
        if (newParticles.gameObject != null){
            (newParticles.GetComponent<AudioSource>()).Play();
        }

        Target target = other.gameObject.GetComponent<Target>();
        target.TakeDamage(damage);
    }

    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(2.4f);
        Destroy(this.gameObject);
    }
}
