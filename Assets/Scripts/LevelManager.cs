using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] GameObject[] levels;

    [SerializeField] GameObject[] doors;

    int actualLevel = 1;

    private void Start() {
        for (int i = 0; i < doors.Length; i++)
            if (doors[i] != null)
                doors[i].SetActive(false);

        int doorToOpen = Random.Range(0, doors.Length);
        if (doors[doorToOpen] != null)
            doors[doorToOpen].SetActive(true);

        PlayerController.DoorEnter += ChangeLevel;

        for (int i = 0; i < levels.Length; i++)
            if (levels[i] != null)
                levels[i].SetActive(false);

        if (levels[actualLevel] != null) {
            levels[actualLevel].SetActive(true);
        }
    }
    private void OnDisable() {
        PlayerController.DoorEnter -= ChangeLevel;
    }

    void ChangeLevel(GameObject door) {
        actualLevel++;
        if (actualLevel > 3)
            actualLevel = 1;

        for (int i = 0; i < doors.Length; i++)
            if (doors[i] != null)
                doors[i].SetActive(false);

        for (int i = 0; i < levels.Length; i++)
            if (levels[i] != null)
                levels[i].SetActive(false);

        if (levels[actualLevel] != null)
            levels[actualLevel].SetActive(true);


        int doorToOpen = Random.Range(0, doors.Length);
        while (doors[doorToOpen] == door.gameObject)
            doorToOpen = Random.Range(0, doors.Length);

        if (doors[doorToOpen] != null)
            doors[doorToOpen].SetActive(true);

    }

    public int GetCurrentLevel()
    {
        return actualLevel;
    }
}
