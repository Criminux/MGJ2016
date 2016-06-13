using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform GunOutput;
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject obj;
    private bool shooting;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        shooting = Input.GetAxis("Fire") != 0;
        Shoot();
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

    void Shoot()
    {
        if (shooting)
        {
            Instantiate(obj, GunOutput.position, transform.GetChild(0).rotation);
        }
    }

}
