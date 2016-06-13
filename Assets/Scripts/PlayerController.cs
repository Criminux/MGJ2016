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

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CurrentGun.SetAssociatedPlayer(transform.GetChild(0));
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
    }

    private void Rotate()
    {
        float moveHorizontal = Input.GetAxis("HorizontalControllerRotation");
        float moveVertical = Input.GetAxis("VerticalControllerRotation");

        Vector3 rotationTarget = this.transform.position + new Vector3(moveHorizontal, 0, moveVertical);

        Transform child = transform.GetChild(0);

        child.LookAt(rotationTarget);
    }

    private void GetRelativeRotationtoCamera()
    {

    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("HorizontalControllerMovement");
        float moveVertical = Input.GetAxis("VerticalControllerMovement");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}
