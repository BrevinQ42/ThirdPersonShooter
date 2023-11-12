using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet Bullet;
    private Transform objTransform;
    private Transform GunTip;

    private Camera cam;
    private RaycastHit aimedTarget;
    private bool hasTarget;

    void Start()
    {
        objTransform = this.gameObject.transform;
        GunTip = objTransform.GetChild(0);

        cam = Camera.main;

        hasTarget = false;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out aimedTarget)){
            Vector3 vector = aimedTarget.point - objTransform.position;

            float rotX = Vector3.Angle(new Vector3(vector.x, 0, vector.z), vector);

            if (vector.y > 0){
                rotX *= -1;
            }

            float rotY = Vector3.Angle(Vector3.forward, new Vector3(vector.x, 0, vector.z));

            if (vector.x < 0){
                rotY *= -1;
            }

            objTransform.rotation = Quaternion.Euler(rotX, rotY, 0);

            hasTarget = true;
        } else {
            objTransform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, 0);

            hasTarget = false;
        }
    }

    public void Shoot()
    {
        Vector3 bulletPos = new Vector3(GunTip.position.x, GunTip.position.y, GunTip.position.z);
        bulletPos += GunTip.TransformDirection(Vector3.forward) *
                    (Bullet.gameObject.transform.localScale.z * 0.75f);

        Bullet newBullet = Instantiate(Bullet.gameObject, bulletPos, 
                            new Quaternion(0,0,0,0)).GetComponent<Bullet>();

        if (hasTarget){
            newBullet.Rotate(objTransform.eulerAngles.x, objTransform.eulerAngles.y);
        } else {
            newBullet.Rotate(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y);
        }
    }
}
