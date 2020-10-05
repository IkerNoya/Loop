using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LevelManager : MonoBehaviour {
    [SerializeField] GameObject[] levels;

    [SerializeField] GameObject[] doors;

    GameManager gm;
    int actualLevel = 1;
    public static event Action<LevelManager> ChangedLevel;

    [SerializeField] AstarPath paths;

    [SerializeField] Boss boss;

    private void OnEnable()
    {
        CleanLevel.OnClearLevel += CheckNextLevel;
        PlayerController.DoorEnter += ChangeLevel;
    }
    private void OnDisable()
    {
        CleanLevel.OnClearLevel -= CheckNextLevel;
        PlayerController.DoorEnter -= ChangeLevel;
    }

    private void Start() {
        gm = GameManager.instanceGM;
       //for (int i = 0; i < doors.Length; i++)
       //    if (doors[i] != null)
       //        doors[i].SetActive(false);
       //
       //int doorToOpen = Random.Range(0, doors.Length);
       //if (doors[doorToOpen] != null)
       //    doors[doorToOpen].SetActive(true);
       //

        for (int i = 0; i < levels.Length; i++)
            if (levels[i] != null)
                levels[i].SetActive(false);

        if (levels[actualLevel] != null) {
            levels[actualLevel].SetActive(true);
        }

        paths.Scan();


        doors = GameObject.FindGameObjectsWithTag("Door");
        
    }


    public void CheckNextLevel()
    {
        if (gm != null)
        {
            if (gm.GetCurrentCountEnemy() <= 0 && gm.GetEnableCheckNextLevel())
            {
                ChangeLevel();
                gm.SetEnableCheckNextLevel(false);
            }
        }
    }

    public void CheckNextLevel(CleanLevel cleanLevel)
    {
        if (cleanLevel != null)
        {
            if (gm != null)
            {
                if (gm.GetCurrentCountEnemy() <= 0 && gm.GetEnableCheckNextLevel())
                {
                    ChangeLevel();
                    gm.SetEnableCheckNextLevel(false);
                }
            }
        }
    }
    public void ChangeLevel() {
        //Debug.Log("ALFAJOR");
        actualLevel++;
        if (actualLevel > 3)
            actualLevel = 1;

        

       for (int i = 0; i < doors.Length; i++)
           if (doors[i] != null)
               doors[i].SetActive(false);

        if (doors.Length > 0)
            Array.Clear(doors, 0, doors.Length);

        StartCoroutine(Change());
       

        // int doorToOpen = Random.Range(0, doors.Length);
        // while (doors[doorToOpen] == door.gameObject)
        //     doorToOpen = Random.Range(0, doors.Length);
        //
        // if (doors[doorToOpen] != null)
        //     doors[doorToOpen].SetActive(true);

    }
    IEnumerator Change() {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();


        for (int i = 0; i < levels.Length; i++)
            if (levels[i] != null)
                levels[i].SetActive(false);

        if (levels[actualLevel] != null)
            levels[actualLevel].SetActive(true);

        doors = GameObject.FindGameObjectsWithTag("Door");

        paths.Scan();

        if (ChangedLevel != null)
            ChangedLevel(this);

        if(actualLevel == 3) {
            SpawnBoss();
        }

        StopCoroutine(Change());
    }

    void SpawnBoss() {
        Boss b = Instantiate(boss, new Vector3(0, 0, 0), Quaternion.identity, levels[actualLevel].transform);
    }

    public int GetCurrentLevel()
    {
        return actualLevel;
    }

    public GameObject[] GetLevels()
    {
        return levels;
    }
}
