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
    Vector3 mousePos;
    Vector3 movement;
    Vector3 direction;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        direction = playerController.lastMousePosition - transform.position;
        weaponType = player.GetComponent<Weapons>();
    }

    private void Update()
    {
        
        movement = direction.normalized * speed;
        transform.position += movement * Time.deltaTime;
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject); 
        }
    }
}
