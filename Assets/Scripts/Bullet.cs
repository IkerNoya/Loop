using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum User
    {
        Enemy,
        Player,
    }
    [SerializeField] User user;
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
        weaponType = player.GetComponent<Weapons>();

        if (user == User.Player)
            direction = playerController.lastMousePosition - transform.position;
        else
            direction = Camera.main.WorldToScreenPoint(player.transform.position);

        if (weaponType.type == Weapons.WeaponType.Shotgun)
        {
            randomDir = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0) + direction;
        }
        else
        {
            randomDir = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0) + direction;
        }
        movement = direction.normalized * speed;
        switch (weaponType.type)
        {
            case Weapons.WeaponType.subMachineGun:
                movement = (direction.normalized + randomDir.normalized) * speed;
                Destroy(gameObject, lifeTime);
                break;
            case Weapons.WeaponType.Shotgun:
                movement = randomDir.normalized * (speed + 2f);
                Destroy(gameObject, lifeTime - 0.5f);
                break;
            case Weapons.WeaponType.Revolver:
                movement = (direction.normalized + randomDir.normalized) * speed;
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
    public void SetUser(User _user)
    {
        user = _user;
    }
    public Bullet.User GetUser()
    {
        return user;
    }
}
