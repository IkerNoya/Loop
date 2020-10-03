using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [HideInInspector] public Vector3 lastMousePosition;
    Vector3 mousePosition;
    Vector3 movement;
    void Start()
    {
        
    }
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        transform.position += movement * Time.deltaTime;
        Inputs();

    }
    void Inputs() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                lastMousePosition = hit.point;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
