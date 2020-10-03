using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {
    [SerializeField] GameObject[] doors;
    Camera cam;

    public delegate void ClickedDoor();
    public static event ClickedDoor DoorClicked;

    void Start() {
        cam = Camera.main;
        for (int i = 0; i < doors.Length; i++)
            if (doors[i] != null)
                doors[i].SetActive(false);

        int doorToOpen = Random.Range(0, doors.Length);
        if (doors[doorToOpen] != null)
            doors[doorToOpen].SetActive(true);
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
                if (hit.transform.CompareTag("Door"))
                    NextLevel(hit.transform.gameObject);
        }
    }

    void NextLevel(GameObject door) {
        for (int i = 0; i < doors.Length; i++)
            if (doors[i] != null)
                doors[i].SetActive(false);


        int doorToOpen = Random.Range(0, doors.Length);
        while (doors[doorToOpen] == door.gameObject)
            doorToOpen = Random.Range(0, doors.Length);

        if (doors[doorToOpen] != null)
            doors[doorToOpen].SetActive(true);

        if (DoorClicked != null)
            DoorClicked();
    }

}
