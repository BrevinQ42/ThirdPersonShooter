using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    private Transform objTransform;
    private float y;

    void Start()
    {
        objTransform = this.gameObject.transform;
        y = objTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (y > -48){
            StartCoroutine(deleteObject());
        }
    }

    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(2.4f);
        Destroy(this.gameObject);
    }
}
