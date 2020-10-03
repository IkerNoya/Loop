using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 mousePos;
    Vector3 movement;
    private void Start()
    {
        
    }

    private void Update()
    {
        mousePos = Input.mousePosition;
        Vector3 direction = mousePos - transform.position;
        movement = direction.normalized * speed;
        transform.position += movement * Time.deltaTime;
    }
}
