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
    private bool specialShooting;
    private float rotateHorizontal;
    private float rotateVertical;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 initPosition;

    private Animator animator;

    //Audiostuff
    [SerializeField]
    private AudioClip steps;
    new AudioSource audio;

    public float PowerUpTimer { get; internal set; }

    // Use this for initialization
    void Start()
    {
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        initPosition = transform.position;
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
        if (!GameManager.Instance.Running)
        {
            rb.velocity = Vector3.zero;
            CurrentGun.Shooting = false;
            CurrentGun.SpecialShooting = false;
            rotateHorizontal = 0;
            rotateVertical = 0;
            moveHorizontal = 0;
            moveVertical = 0;
            return;
        }

        animator.SetBool("moving", rb.velocity.magnitude > 0.1f);

        PowerUpTimer -= Time.deltaTime;

        //Dropdetection
        if (transform.position.y < -3)
            while (GameManager.Instance.Lives > 0)
                GameManager.Instance.RemoveLife();

        CurrentGun.Shooting = Input.GetAxis("Fire") != 0;
        CurrentGun.SpecialShooting = Input.GetButtonDown("Fire2");
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

    internal void InitPosition()
    {
        this.transform.position = initPosition;
    }
}
