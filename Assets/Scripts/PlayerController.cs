using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    Rigidbody2D rb;

    bool ActivateDash = false;
    bool canActivateDash = true;

    public delegate void EnterDoor(GameObject door);
    public static event EnterDoor DoorEnter;
    #endregion

    #region BASE_FUNCTIONS
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!ActivateDash)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
            transform.position += movement * Time.deltaTime;
            transform.LookAt(mousePosition, Vector3.forward);
            Inputs();
        }
        else
        {
            transform.position += transform.up * 50 * Time.deltaTime;
            ActivateDash = false;
            StartCoroutine(DashCooldown());
        }
        Debug.Log("can A...." + canActivateDash);
        Debug.Log("can A...." + ActivateDash);
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
                if(bullet!=null)
                    Instantiate(bullet, transform.position, Quaternion.identity);
                if(screenShake!=null)
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
            if(hitCollider!=null)
                StartCoroutine(StartCollider(hitCollider));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canActivateDash)
        {
            ActivateDash = true;
            canActivateDash = false;
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

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(2f);
        canActivateDash = true;
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
