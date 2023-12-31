using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objTransform;
    private float speed;

    private Vector3 originalMousePos;
    public float mouseSens;

    public Gun gun;
    private float shotCooldown;
    private float walkCooldown;

    public AudioSource walk1, walk2, walk3, walk4, walk5, walk6, walk7;
    private List<AudioSource>  walkSounds;
    private AudioSource randomWalk;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        objTransform = this.gameObject.transform;

        speed = 5.0f;

        originalMousePos = Input.mousePosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        shotCooldown = 0.0f;
        walkCooldown = 0.0f;

        walkSounds = new List<AudioSource>(){walk1, walk2, walk3, walk4, walk5, walk6, walk7};
    }

    void Update()
    {
        bool isMoving = false;

        // player movement
        if (Input.GetKey(KeyCode.W)){
            rb.velocity = transform.forward * speed;
            isMoving = true;
        } else if (Input.GetKey(KeyCode.S)){
            rb.velocity = -transform.forward * speed;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.A)){
            if (!isMoving){
                rb.velocity = -transform.right * speed;
                isMoving = true;
            } else {
                rb.velocity -= transform.right * speed;
            }
        } else if (Input.GetKey(KeyCode.D)){
            if (!isMoving){
                rb.velocity = transform.right * speed;
                isMoving = true;
            } else {
                rb.velocity += transform.right * speed;
            }
        }

        // walk sounds
        randomWalk = walkSounds[Random.Range(0,walkSounds.Count)];

        if (isMoving && walkCooldown <= 0){
            randomWalk.Play();
            walkCooldown = 0.69f;
        }

        // prevent player from moving on the y-axis (up and down)
        if (isMoving){
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        } else {

            // stop player's movement if no movement key is being pressed
            rb.velocity = new Vector3(0,0,0);
        }


        // rotate the player (and in turn, the camera and the gun) based on the mouse's position
        Vector3 currentMousePos = Input.mousePosition;

        objTransform.Rotate(0, (currentMousePos.x - originalMousePos.x) * mouseSens, 0, Space.World);
        objTransform.Rotate(-(currentMousePos.y - originalMousePos.y) * mouseSens, 0, 0, Space.Self);

        originalMousePos = currentMousePos;

        // configure x rotation
        float xRot = objTransform.eulerAngles.x;

        if (xRot > 180){
            xRot -= 360;
        }

        // limit the x rotation between -45 and 45 degrees
        if (xRot > 45){
            objTransform.Rotate(-(xRot - 45), 0, 0, Space.Self);
        } else if (xRot < -45){
            objTransform.Rotate(-(xRot + 45), 0, 0, Space.Self);
        }


        if (shotCooldown > 0){
            shotCooldown -= Time.deltaTime;
        }

        if (walkCooldown > 0){
            walkCooldown -= Time.deltaTime;
        }

        // player input to shoot (left click)
        if (Input.GetMouseButton(0) && shotCooldown <= 0){
            gun.Shoot();

            shotCooldown = 0.25f;
        }
    }
}
