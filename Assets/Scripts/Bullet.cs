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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

        Debug.Log(weaponType.type);
        if (weaponType.type == Weapons.WeaponType.Shotgun)
        {
            Debug.Log("SOY SHOOTGUN");
            randomDir = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0) + direction;
        }
        else
        {
            if(weaponType.type == Weapons.WeaponType.subMachineGun)
            {
                randomDir = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0) + direction;
            }
            else if(weaponType.type == Weapons.WeaponType.Revolver)
            {
                randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) + direction;
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
                Destroy(gameObject, lifeTime-0.5f);
                break;
            case Weapons.WeaponType.Revolver:
                movement = (randomDir.normalized) * speed;
                Destroy(gameObject, lifeTime + 0.5f);
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
