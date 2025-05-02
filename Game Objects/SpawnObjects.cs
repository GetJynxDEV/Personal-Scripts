using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instatiate : MonoBehaviour
{
    public GameObject projectile;
    public Transform bulletPos;

    public float bulletTimer;

    private void Update()
    { 
        bulletTimer = Time.deltaTime;

        if (bulletTimer > phaseTime)
        {
            bulletTimer = 0;

            shootProjectile();
        }
    }

    private void shootProjectile()
    {
        Instantiate(projectile, bulletPos.position, Quaternion.identity);
    }
}
