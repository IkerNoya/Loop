using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserSphere : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] Vector3 objective;
    private void Start() {
        StartCoroutine(LateStart());
    }

    public void SetObjective(Vector3 o) {
        objective = o;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(damage);
            StopCoroutine(MoveToObjective());
            Destroy(this.gameObject);
        }
    }

    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();
        StopCoroutine(LateStart());
        StartCoroutine(MoveToObjective());
    }

    IEnumerator MoveToObjective() {
        while(transform.position != objective) {
            transform.position = Vector3.MoveTowards(transform.position, objective, speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(MoveToObjective());
        Destroy(this.gameObject);
        yield return null;
    }

}
