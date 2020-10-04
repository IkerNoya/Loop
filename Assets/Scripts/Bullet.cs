using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Bullet : MonoBehaviour
{
    public enum User
    {
        Enemy,
        Player,
    }
    [SerializeField] User user;
    [HideInInspector]
    public Enemy enemyUser;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject bullet;
    [SerializeField] ParticleSystem particles;
    float damage;
    GameObject player;
    Weapons weaponType;
    PlayerController playerController;
    Vector3 movement;
    Vector3 direction;
    Vector3 randomDir;

    /*[SerializeField]*/ float EnemyShotgunRecoil = 0.3f;
    /*[SerializeField]*/ float EnemyRevolverRecoil = 0.25f;
    /*[SerializeField]*/ float EnemySubMachineGunRecoil = 0.28f;

    /*[SerializeField]*/ float PlayerShotgunRecoil = 2.5f;
    /*[SerializeField]*/ float PlayerRevolverRecoil = 1.0f;
    /*[SerializeField]*/ float PlayerSubMachineGunRecoil = 1.5f;
    float shotgunLifetimeOffset = 0.3f;
    float revolverLifetimeOffset = 0.5f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        playerController = player.GetComponent<PlayerController>();

        if (user == User.Player)
        {
            direction = playerController.lastMousePosition - transform.position;
            weaponType = player.GetComponent<Weapons>();
        }
        else if(user == User.Enemy)
        {
            direction = enemyUser.transform.up;
            weaponType = enemyUser.GetComponent<Weapons>();
        }
        if (weaponType.type == Weapons.WeaponType.Shotgun)
        {
            if (user == User.Player)
                randomDir = new Vector3(Random.Range(-PlayerShotgunRecoil, PlayerShotgunRecoil), Random.Range(-PlayerShotgunRecoil, PlayerShotgunRecoil), 0) + direction;
            else if (user == User.Enemy)
                randomDir = new Vector3(Random.Range(-EnemyShotgunRecoil, EnemyShotgunRecoil), Random.Range(-EnemyShotgunRecoil, EnemyShotgunRecoil), 0) + direction;
        }
        else
        {
            if(weaponType.type == Weapons.WeaponType.subMachineGun)
            {
                if(user == User.Player)
                    randomDir = new Vector3(Random.Range(-PlayerSubMachineGunRecoil, PlayerSubMachineGunRecoil), Random.Range(-PlayerSubMachineGunRecoil, PlayerSubMachineGunRecoil), 0) + direction;
                else if(user == User.Enemy)
                    randomDir = new Vector3(Random.Range(-EnemySubMachineGunRecoil, EnemySubMachineGunRecoil), Random.Range(-EnemySubMachineGunRecoil, EnemySubMachineGunRecoil), 0) + direction;

            }
            else if(weaponType.type == Weapons.WeaponType.Revolver)
            {
                if(user == User.Player)
                    randomDir = new Vector3(Random.Range(-PlayerRevolverRecoil, PlayerRevolverRecoil), Random.Range(-PlayerRevolverRecoil, PlayerRevolverRecoil), 0) + direction;
                else if(user == User.Enemy)
                    randomDir = new Vector3(Random.Range(-EnemyRevolverRecoil, EnemyRevolverRecoil), Random.Range(-EnemyRevolverRecoil, EnemyRevolverRecoil), 0) + direction;
            }
        }
        movement = direction.normalized * speed;
        switch (weaponType.type)
        {
            case Weapons.WeaponType.subMachineGun:
                movement = (randomDir.normalized) * speed;
                Destroy(gameObject, lifeTime);
                break;
            case Weapons.WeaponType.Shotgun:
                movement = randomDir.normalized * speed;
                Destroy(gameObject, lifeTime - shotgunLifetimeOffset);
                break;
            case Weapons.WeaponType.Revolver:
                movement = (randomDir.normalized) * speed;
                Destroy(gameObject, lifeTime + revolverLifetimeOffset);
                break;
        }
    }

    private void Update()
    {
        transform.position += movement * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Boss>().ReceiveDamage(1f);
            return;
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            movement *= -1 / 2;
            Destroy(bullet);
            particles.Play();
            Destroy(gameObject, 1.0f);
        }
    }
    public void SetUser(User _user)
    {
        user = _user;
    }
    public Bullet.User GetUser()
    {
        return user;
    }
    public void SetDamage(float value)
    {
        damage = value;
    }
    public float GetDamage()
    {
        return damage;
    }
    public void SetTypeWeapon(Weapons.WeaponType _weaponType)
    {
        if(weaponType != null)
            weaponType.type = _weaponType;
    }
}
