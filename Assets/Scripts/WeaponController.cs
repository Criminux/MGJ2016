using UnityEngine;
using System.Collections;
using System;

public class WeaponController : MonoBehaviour
{
    //AudioStuff
    [SerializeField]
    private AudioClip shot;
    new AudioSource audio;

    [SerializeField]
    private GameObject weapon;

    private bool specialShooting;
    private bool shooting;

    public bool SpecialShooting
    {
        get { return specialShooting; }
        set { specialShooting = value; }
    }

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
    private float specialShootTime;
    private float specialShootTimer;

    [SerializeField]
    private Transform gunOutputPoint;

    [SerializeField]
    private float delay;
    private float delaycounter;

    private Transform player;

    void Start()
    {
        this.currentAmmunition = clipsize;
        audio = GetComponent<AudioSource>();
        specialShootTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        specialShootTimer -= Time.deltaTime;
        delaycounter -= Time.deltaTime;
        if (delaycounter <= 0)
            delaycounter = delay;

        if (currentAmmunition > 0)
        {
            if (delaycounter == delay)
                if (shooting)
                {
                    Shoot();
                }
        }
        else
            Reload();

        if (specialShootTimer <= 0)
        {
            if (specialShooting)
            {
                ShootSpecial();
                specialShootTimer = specialShootTime;
            }
        }


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

    private void ShootSpecial()
    {
        Instantiate(weapon, gunOutputPoint.position, player.rotation);
    }
}
