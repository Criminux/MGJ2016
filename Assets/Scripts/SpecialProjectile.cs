using UnityEngine;
using System.Collections;

public class SpecialProjectile : MonoBehaviour {

    [SerializeField]
    private float lifetime = 3;

    [SerializeField]
    private float speed = 2;

    public void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;

        if (lifetime <= 0)
        {
            //TODO Damage an alle umliegenden Radierer
            Destroy(gameObject);
        }
            

        this.transform.position += transform.forward * speed;
    }
}
