using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
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
        direction = playerController.lastMousePosition - transform.position;
        weaponType = player.GetComponent<Weapons>();
        if(weaponType.type == Weapons.WeaponType.Shotgun)
        {
           randomDir = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0) + direction;
        }
        movement = direction.normalized * speed;
        switch (weaponType.type)
        {
            case Weapons.WeaponType.subMachineGun:
                movement = direction.normalized * speed;
                Destroy(gameObject, lifeTime);
                break;
            case Weapons.WeaponType.Shotgun:
                movement = randomDir.normalized * (speed + 2f);
                Destroy(gameObject, lifeTime - 0.5f);
                break;
            case Weapons.WeaponType.Revolver:
                movement = direction.normalized * speed;
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
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject); 
        }
    }
}
