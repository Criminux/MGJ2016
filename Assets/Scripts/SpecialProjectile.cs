using UnityEngine;
using System.Collections;

public class SpecialProjectile : MonoBehaviour
{

    [SerializeField]
    private float lifetime = 3;

    [SerializeField]
    private float speed = 2;

    [SerializeField]
    GameObject explosion;

    public void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;

        if (lifetime <= 0)
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
            Destroy(gameObject);
        }


        this.transform.position += transform.forward * speed;
    }


}
