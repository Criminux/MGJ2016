using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private WeaponController CurrentGun;
    private bool shooting;
    private float rotateHorizontal;
    private float rotateVertical;
    private float moveHorizontal;
    private float moveVertical;


    //Audiostuff
    [SerializeField]
    private AudioClip steps;
    new AudioSource audio;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CurrentGun.SetAssociatedPlayer(transform.GetChild(0));

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetRelativeRotationtoCamera();
        Move();
        Rotate();
    }

    public void Update()
    {
        CurrentGun.Shooting = Input.GetAxis("Fire") != 0;
        rotateHorizontal = Input.GetAxis("HorizontalControllerRotation");
        rotateVertical = Input.GetAxis("VerticalControllerRotation");
        moveHorizontal = Input.GetAxis("HorizontalControllerMovement");
        moveVertical = Input.GetAxis("VerticalControllerMovement");
    }

    private void Rotate()
    {
        Vector3 rotationTarget = this.transform.position + new Vector3(rotateHorizontal, 0, rotateVertical);

        Transform child = transform.GetChild(0);

        child.LookAt(rotationTarget);
    }

    private void GetRelativeRotationtoCamera()
    {

    }

    void Move()
    {
        

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (movement == new Vector3(0, 0, 0))
        {
            audio.Pause();
        }
        else { audio.UnPause(); }
    }
}
