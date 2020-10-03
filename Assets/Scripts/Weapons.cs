using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
   
    [SerializeField] private Bullet.User user;
    [SerializeField] private Bullet OriginBullet;
    private Bullet bullet;
    [SerializeField] float timeToShoot;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitudeShotgun;
    [SerializeField] float shakeMagnitudeSMG;
    float shotgunShellsAmmount = 7;
    public enum WeaponType
    {
        subMachineGun, Shotgun, Revolver, Count
    }
    public WeaponType type;
    float timer = 0;
    bool canShoot = true;
    public void ShootSubmachineGun()
    {
        if (canShoot)
        {
            if (OriginBullet != null)
            {
                bullet = Instantiate(OriginBullet, transform.position, Quaternion.identity);
                bullet.SetUser(user);
                Debug.Log(bullet.GetUser());
            }

            canShoot = false;
            StartCoroutine(Cooldown(timeToShoot));
        }
    }
    public void ShootShotgun()
    {
        if (canShoot)
        {
            if(OriginBullet != null)
            {
                for (int i = 0; i < shotgunShellsAmmount; i++)
                {
                    bullet = Instantiate(OriginBullet, transform.position, Quaternion.identity);
                    bullet.SetUser(user);
                }
            }
            canShoot = false;
            StartCoroutine(Cooldown(timeToShoot + 1f));
        }
    }
    public void ShootRevolver()
    {
        if (canShoot)
        {
            if (OriginBullet != null)
            {
                bullet = Instantiate(OriginBullet, transform.position, Quaternion.identity);
                bullet.SetUser(user);
            }

            canShoot = false;
            StartCoroutine(Cooldown(timeToShoot + 0.3f));
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
    public float GetShakeDuration()
    {
        return shakeDuration;
    }
    public int GetCountWeapons()
    {
        return (int)WeaponType.Count;
    }
    public float GetShakeMagnitude(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.subMachineGun:
                return shakeMagnitudeSMG;
                break;
            case WeaponType.Shotgun:
                return shakeMagnitudeShotgun;
                break;
            case WeaponType.Revolver:
                return shakeMagnitudeSMG;
                break;
        }
        return 0;
    }
}
