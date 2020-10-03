using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float timeToShoot;
    float shotgunShellsAmmount = 7;
    public enum WeaponType
    {
        subMachineGun, Shotgun, Pistol
    }
    public WeaponType type;
    float timer = 0;
    bool canShoot = true;
    public void ShootSubmachineGun()
    {
        if (canShoot)
        {
            if (bullet != null)
                Instantiate(bullet, transform.position, Quaternion.identity);

            canShoot = false;
            StartCoroutine(Cooldown(timeToShoot));
        }
    }
    public void ShootShotgun()
    {
        if (canShoot)
        {
            if(bullet!=null)
            {
                for(int i=0;i<shotgunShellsAmmount;i++)
                    Instantiate(bullet, transform.position, Quaternion.identity);
            }
            canShoot = false;
            StartCoroutine(Cooldown(timeToShoot + 1f));
        }
    }
    IEnumerator Cooldown(float rateOfFire) {
        canShoot = false;
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
        StopCoroutine(Cooldown(rateOfFire));
        yield return null;
    }
    public bool GetCanShoot() {
        return canShoot;
    }
}
