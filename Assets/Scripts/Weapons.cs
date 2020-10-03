using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float timeToShoot;
    float timer = 0;
    bool canShoot = true;
    public void ShootSubmachineGun()
    {
        if (canShoot)
        {
            if (bullet != null)
                Instantiate(bullet, transform.position, Quaternion.identity);

            canShoot = false;
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown() {
        canShoot = false;
        yield return new WaitForSeconds(timeToShoot);
        canShoot = true;
        StopCoroutine(Cooldown());
        yield return null;
    }
    public bool GetCanShoot() {
        return canShoot;
    }
}
