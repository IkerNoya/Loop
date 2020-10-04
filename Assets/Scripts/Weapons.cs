using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
   
    [SerializeField] private Bullet.User user;
    [SerializeField] private Enemy enemyUser;
    [SerializeField] private Bullet OriginBullet;
    private Bullet bullet;
    [SerializeField] float timeToShoot;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitudeShotgun;
    [SerializeField] float shakeMagnitudeSMG;
    [SerializeField] float shakeMagnitudeRevolver;
    [SerializeField] float damageShootgun;
    [SerializeField] float damageRevolver;
    [SerializeField] float damageSMG;
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
                bullet.enemyUser = enemyUser;
                bullet.SetDamage(damageSMG);

               // Debug.Log(bullet.GetUser());
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
                    bullet.enemyUser = enemyUser;
                    bullet.SetDamage(damageShootgun);
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
                bullet.enemyUser = enemyUser;
                bullet.SetDamage(damageRevolver);
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
            case WeaponType.Shotgun:
                return shakeMagnitudeShotgun;
            case WeaponType.Revolver:
                return shakeMagnitudeRevolver;
        }
        return 0;
    }
}
