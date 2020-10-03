﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : Character {
    #region VARIABLES
    [SerializeField] float dashSpeed;
    [SerializeField] CameraShake screenShake;
    [SerializeField] GameObject hitCollider;
    [SerializeField] float shakeMagnitude = 0.05f;
    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] string playerInputHorizontal;
    [SerializeField] string playerInputVertical;
    [HideInInspector] public Vector3 lastMousePosition;
    Vector3 mousePosition;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    Weapons weapons;

    bool ActivateDash = false;
    bool canActivateDash = true;

    public delegate void EnterDoor(GameObject door);
    public static event EnterDoor DoorEnter;
    #endregion

    #region BASE_FUNCTIONS
    void Start() {
        weapons = GetComponent<Weapons>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Update() {
        if (!ActivateDash) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movement = new Vector2(Input.GetAxis(playerInputHorizontal), Input.GetAxis(playerInputVertical)) * speed;
            transform.position += movement * Time.deltaTime;

            Vector2 dir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = dir;

            Inputs();
        }
        if(ActivateDash)
        {
            StartCoroutine(Dash());
            StartCoroutine(DashCooldown());
        }
    }

    #endregion

    #region FUNCTIONS
    void Inputs() {
        if (Input.GetMouseButton(0) && !ActivateDash) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null) {
                if (weapons != null)
                    if (weapons.GetCanShoot()) {
                        lastMousePosition = hit.point;
                        weapons.ShootSubmachineGun();
                        if (screenShake != null)
                            StartCoroutine(screenShake.Shake(shakeDuration, shakeMagnitude));
                    }
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            spriteRenderer.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            spriteRenderer.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            spriteRenderer.color = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            spriteRenderer.color = Color.yellow;
        }
        if (Input.GetMouseButton(1)) {
            if (hitCollider != null)
                StartCoroutine(StartCollider(hitCollider));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canActivateDash) {
            ActivateDash = true;
            canActivateDash = false;
        }
    }
    #endregion

    #region COROUTINES
    IEnumerator StartCollider(GameObject collider) {
        collider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        collider.SetActive(false);
    }

    IEnumerator DashCooldown() {
        yield return new WaitForSeconds(2f);
        canActivateDash = true;
    }
    IEnumerator Dash()
    {
        transform.position += movement * dashSpeed * Time.deltaTime;
        yield return new WaitForSeconds(0.05f);
        ActivateDash = false;
    }
    #endregion

    #region COLLISION
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Door")) {
            if (DoorEnter != null)
                DoorEnter(collision.gameObject);
        }
    }
    #endregion
}
