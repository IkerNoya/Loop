using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    #region VARIABLES
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] CameraShake screenShake;
    [SerializeField] GameObject hitCollider;
    [HideInInspector] public Vector3 lastMousePosition;
    float shakeMagnitude = 0.05f;
    float shakeDuration = 0.2f;
    Vector3 mousePosition;
    Vector3 movement;

    public delegate void EnterDoor(GameObject door);
    public static event EnterDoor DoorEnter;
    #endregion

    #region BASE_FUNCTIONS
    void Start()
    {

    }
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        transform.position += movement * Time.deltaTime;
        Inputs();
        transform.LookAt(mousePosition, Vector3.back);
    }
    #endregion

    #region FUNCTIONS
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
                StartCoroutine(screenShake.Shake(shakeDuration, shakeMagnitude));
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(Input.GetMouseButton(1))
        {
            StartCoroutine(StartCollider(hitCollider));
        }
    }
    #endregion
    #region COROUTINES
    IEnumerator StartCollider(GameObject collider)
    {
        collider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        collider.SetActive(false);
    }
    #endregion
    #region COLLISION
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("XD");
        if (collision.gameObject.CompareTag("Door")) {
            if (DoorEnter != null)
                DoorEnter(collision.gameObject);
        }
    }
    #endregion
}
