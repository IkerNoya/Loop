using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character {
    #region VARIABLES
    [SerializeField] float dashSpeed;
    [SerializeField] CameraShake screenShake;
    [SerializeField] GameObject hitCollider;
    [SerializeField] GameObject gun;
    [SerializeField] float shakeMagnitude = 0.05f;
    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] string playerInputHorizontal;
    [SerializeField] string playerInputVertical;
    [HideInInspector] public Vector3 lastMousePosition;
    Vector3 mousePosition;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    SpriteRenderer gunSpriteRenderer;
    Weapons weapons;
    Rigidbody2D rb;

    [SerializeField] GameObject[] cannonPos;

    bool aimingRight = false;
    bool aimingLeft = false;

    bool ActivateDash = false;
    bool canActivateDash = true;

    [SerializeField] PlayerAnims playerAnims;

    public delegate void EnterDoor(GameObject door);
    public static event EnterDoor DoorEnter;
    #endregion

    #region BASE_FUNCTIONS
    void Start() {
        weapons = GetComponent<Weapons>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        screenShake = FindObjectOfType<CameraShake>();
        rb = GetComponent<Rigidbody2D>();
        gunSpriteRenderer = gun.GetComponent<SpriteRenderer>();
    }
    void Update() {
        if (!ActivateDash) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movement = new Vector2(Input.GetAxis(playerInputHorizontal), Input.GetAxis(playerInputVertical)) * speed;
            Vector2 dir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            if (mousePosition.x > transform.position.x) {
                gunSpriteRenderer.flipX = false;

                aimingRight = true;
                aimingLeft = false;
                if (cannonPos[0] != null)
                    if (!cannonPos[0].activeSelf)
                        cannonPos[0].SetActive(true);
                if (cannonPos[1] != null)
                    if (cannonPos[1].activeSelf)
                        cannonPos[1].SetActive(false);

                gun.transform.right = dir;
            }
            else if (mousePosition.x < transform.position.x) {
                gunSpriteRenderer.flipX = true;

                aimingRight = false;
                aimingLeft = true;
                if (cannonPos[0] != null)
                    if (cannonPos[0].activeSelf)
                        cannonPos[0].SetActive(false);
                if (cannonPos[1] != null)
                    if (!cannonPos[1].activeSelf)
                        cannonPos[1].SetActive(true);

                gun.transform.right = -dir;
            }
            if (gun.transform.position.y > transform.position.y) gunSpriteRenderer.sortingOrder = 1;
            else gunSpriteRenderer.sortingOrder = 3;
            
            Inputs();
        }

    }
    private void FixedUpdate() {
        rb.velocity = new Vector2(movement.x, movement.y);
        if (ActivateDash) {
            StartCoroutine(Dash());
            StartCoroutine(DashCooldown());
        }
    }

    #endregion

    #region FUNCTIONS
    void Inputs() {
        if (weapons != null) {
            switch (weapons.type) {
                case Weapons.WeaponType.subMachineGun:
                    if (Input.GetMouseButton(0) && !ActivateDash) {

                        if (weapons.GetCanShoot()) {
                            lastMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f) + new Vector3((float)Random.Range(-1.75f, 1.75f), (float)Random.Range(-1.75f, 1.75f), 0f);

                            if (aimingRight) {
                                if (cannonPos[0] != null && cannonPos[0].activeSelf)
                                    weapons.ShootSubmachineGun(cannonPos[0].transform.position);
                            }
                            else if (aimingLeft) {
                                if (cannonPos[1] != null && cannonPos[1].activeSelf)
                                    weapons.ShootSubmachineGun(cannonPos[1].transform.position);
                            }

                            if (screenShake != null)
                                StartCoroutine(screenShake.Shake(weapons.GetShakeDuration(), weapons.GetShakeMagnitude(Weapons.WeaponType.subMachineGun)));
                        }

                    }
                    break;
                case Weapons.WeaponType.Shotgun:
                    if (Input.GetMouseButtonDown(0) && !ActivateDash) {

                        if (weapons.GetCanShoot()) {
                            lastMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);

                            if (aimingRight) {
                                if (cannonPos[0] != null && cannonPos[0].activeSelf)
                                    weapons.ShootShotgun(cannonPos[0].transform.position);
                            }
                            else if (aimingLeft) {
                                if (cannonPos[1] != null && cannonPos[1].activeSelf)
                                    weapons.ShootShotgun(cannonPos[1].transform.position);
                            }

                            if (screenShake != null)
                                StartCoroutine(screenShake.Shake(weapons.GetShakeDuration(), weapons.GetShakeMagnitude(Weapons.WeaponType.Shotgun)));
                        }

                    }
                    break;
                case Weapons.WeaponType.Revolver:
                    if (Input.GetMouseButton(0) && !ActivateDash) {

                        if (weapons.GetCanShoot()) {
                            lastMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f) + new Vector3((float)Random.Range(-0.5f, 0.5f), (float)Random.Range(-0.5f, 0.5f), 0f);

                            if (aimingRight) {
                                if (cannonPos[0] != null && cannonPos[0].activeSelf)
                                    weapons.ShootRevolver(cannonPos[0].transform.position);
                            }
                            else if (aimingLeft) {
                                if (cannonPos[1] != null && cannonPos[1].activeSelf)
                                    weapons.ShootRevolver(cannonPos[1].transform.position);
                            }

                            if (screenShake != null)
                                StartCoroutine(screenShake.Shake(weapons.GetShakeDuration(), weapons.GetShakeMagnitude(Weapons.WeaponType.Shotgun)));
                        }

                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            playerAnims.StartIdleAnim();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            playerAnims.StartIdleAnim();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            spriteRenderer.flipX = true;
        }

        if(Input.GetKey(KeyCode.A))
            playerAnims.StartAnimMoveSide();

        if (Input.GetKeyDown(KeyCode.D)) {
            spriteRenderer.flipX = false;
        }

        if(Input.GetKey(KeyCode.D))
            playerAnims.StartAnimMoveSide();

        if (Input.GetMouseButton(1)) {
            if (hitCollider != null)
                StartCoroutine(StartCollider(hitCollider));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            weapons.type = Weapons.WeaponType.subMachineGun;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            weapons.type = Weapons.WeaponType.Shotgun;
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            weapons.type = Weapons.WeaponType.Revolver;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canActivateDash) {
            ActivateDash = true;
            canActivateDash = false;
        }
    }

    public void ReceiveDamage(float d) {
        hp -= d;
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
    IEnumerator Dash() {
        rb.velocity = movement * dashSpeed;
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
        if (collision.gameObject.CompareTag("Bullet")) {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null) {
                if (bullet.GetUser() != Bullet.User.Player) {
                    SetHP(GetHP() - bullet.GetDamage());
                    Destroy(bullet.gameObject);
                }
            }
        }
    }
    #endregion
}
