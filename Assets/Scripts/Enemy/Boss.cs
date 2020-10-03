using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [SerializeField] float hp;
    [SerializeField] float damage;
    [SerializeField] float speed;
    bool attacking = false;
    [SerializeField] PlayerController player;
    [SerializeField] float timeStopped;
    [SerializeField] Vector3 posToAttack;
    void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable() {
        StartCoroutine(PrepareAttack());
    }
    private void OnDisable() {
        StopCoroutine(PrepareAttack());
        StopCoroutine(Attack());
    }

    private void Update() {
        if(player == null) {
            transform.Rotate(Vector3.forward * 180f * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!attacking)
            return;

        if (collision.gameObject.CompareTag("Player")) {
            player.ReceiveDamage(damage);
            attacking = false;
            if (player == null) {
                StopCoroutine(PrepareAttack());
                StopCoroutine(Attack());
            }
        }
    }
    IEnumerator PrepareAttack() {
        attacking = false;
        yield return new WaitForSeconds(timeStopped);
        StopCoroutine(PrepareAttack());
        if (player != null) {
            posToAttack = player.transform.position;
            StartCoroutine(Attack());
        }
        yield return null;
    }
    IEnumerator Attack() {
        attacking = true;
        Vector2 dir = new Vector2(posToAttack.x - transform.position.x, posToAttack.y - transform.position.y);
        transform.up = dir;
        while (transform.position != posToAttack) {
            transform.position = Vector2.MoveTowards(transform.position, posToAttack, speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(Attack());
        if (player != null) 
            StartCoroutine(PrepareAttack());
        
        yield return null;
    }
    public void ReceiveDamage(float d) {
        hp -= d;
        if(hp <= 0) {
            Destroy(this.gameObject);
        }
    }
}
