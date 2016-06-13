using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 10;

    [SerializeField]
    private float speed = 2;

    public void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;

        if (lifetime <= 0)
            Destroy(gameObject);

        this.transform.position += this.transform.forward * speed;
    }
}
