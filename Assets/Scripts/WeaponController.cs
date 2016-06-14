using UnityEngine;
using System.Collections;
using System;

public class WeaponController : MonoBehaviour
{
    //AudioStuff
    [SerializeField]
    private AudioClip shot;
    new AudioSource audio;




    private bool shooting;

    public bool Shooting
    {
        get { return shooting; }
        set { shooting = value; }
    }

    [SerializeField]
    private int clipsize;

    public int ClipSize
    {
        get { return clipsize; }
    }

    [SerializeField]
    private int currentAmmunition;

    [SerializeField]
    private GameObject Projectile;

    internal void SetAssociatedPlayer(Transform player)
    {
        this.player = player;
    }

    [SerializeField]
    private float reloadingTime;

    private float reloadingTimer;

    [SerializeField]
    private Transform gunOutputPoint;

    private Transform player;

    void Start()
    {
        this.currentAmmunition = clipsize;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmunition > 0)
        {
            if (shooting)
            {
                Shoot();
            }
        }
        else
            Reload();


    }

    private void Reload()
    {
        if (reloadingTimer <= 0)
        {
            this.currentAmmunition = clipsize;
            reloadingTimer = reloadingTime;
        }
        reloadingTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        currentAmmunition--;
        Instantiate(Projectile, gunOutputPoint.position, player.rotation);
        audio.PlayOneShot(shot, 0.3f);
    }
}
