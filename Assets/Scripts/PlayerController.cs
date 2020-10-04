using System.Collections;
using System.Collections.Generic;
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
    Rigidbody2D rb;

    bool ActivateDash = false;
    bool canActivateDash = true;

    public delegate void EnterDoor(GameObject door);
    public static event EnterDoor DoorEnter;
    #endregion

    #region BASE_FUNCTIONS
    void Start()
    {
        weapons = GetComponent<Weapons>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        screenShake = FindObjectOfType<CameraShake>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!ActivateDash)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movement = new Vector2(Input.GetAxis(playerInputHorizontal), Input.GetAxis(playerInputVertical)) * speed;
           

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
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    #endregion

    #region FUNCTIONS
    void Inputs() {
        if (weapons != null)
        {
            switch (weapons.type)
            {
                case Weapons.WeaponType.subMachineGun:
                    if (Input.GetMouseButton(0) && !ActivateDash)
                    {

                        if (weapons.GetCanShoot())
                        {
                            lastMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f) + new Vector3((float)Random.Range(-1.75f, 1.75f), (float)Random.Range(-1.75f, 1.75f), 0f);
                            weapons.ShootSubmachineGun();
                            if (screenShake != null)
                                StartCoroutine(screenShake.Shake(weapons.GetShakeDuration(), weapons.GetShakeMagnitude(Weapons.WeaponType.subMachineGun)));
                        }

                    }
                    break;
                case Weapons.WeaponType.Shotgun:
                    if (Input.GetMouseButtonDown(0) && !ActivateDash)
                    {

                        if (weapons.GetCanShoot())
                        {
                            lastMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
                            weapons.ShootShotgun();
                            if (screenShake != null)
                                StartCoroutine(screenShake.Shake(weapons.GetShakeDuration(), weapons.GetShakeMagnitude(Weapons.WeaponType.Shotgun)));
                        }

                    }
                    break;
                case Weapons.WeaponType.Revolver:
                    if (Input.GetMouseButton(0) && !ActivateDash)
                    {

                        if (weapons.GetCanShoot())
                        {
                            lastMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f) + new Vector3((float)Random.Range(-0.5f, 0.5f), (float)Random.Range(-0.5f, 0.5f), 0f);
                            weapons.ShootRevolver();
                            if (screenShake != null)
                                StartCoroutine(screenShake.Shake(weapons.GetShakeDuration(), weapons.GetShakeMagnitude(Weapons.WeaponType.Shotgun)));
                        }

                    }
                    break;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons.type = Weapons.WeaponType.subMachineGun;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons.type = Weapons.WeaponType.Shotgun;
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
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
