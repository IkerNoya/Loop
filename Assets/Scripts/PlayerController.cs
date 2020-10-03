using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    Vector3 mousePosition;
    Vector3 movement;
    void Start()
    {
        
    }
    void Update()
    {
        mousePosition = Input.mousePosition;
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed;
        transform.position += movement * Time.deltaTime;
        Inputs();
        Debug.Log(mousePosition);
    }
    void Inputs() 
    { 
        Ray castPoint = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
}
