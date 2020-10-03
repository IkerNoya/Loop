using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTester : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gameObjectsLevel;
    private int index = 0;
    public KeyCode nextLevelInput;
    public KeyCode prevLevelInput;

    // Update is called once per frame
    void Update()
    {
        CheckNextLevel();
    }
    public void CheckNextLevel()
    {
        if (Input.GetKeyDown(nextLevelInput))
        {
            index++;
            if (index >= gameObjectsLevel.Length)
            {
                index = 0;
            }
            for (int i = 0; i < gameObjectsLevel.Length; i++)
                gameObjectsLevel[i].SetActive(false);

            gameObjectsLevel[index].SetActive(true);
        }
        if (Input.GetKeyDown(prevLevelInput))
        {
            index--;
            if (index < 0)
            {
                index = gameObjectsLevel.Length -1;
            }
            for (int i = 0; i < gameObjectsLevel.Length; i++)
                gameObjectsLevel[i].SetActive(false);

            gameObjectsLevel[index].SetActive(true);
        }
    }
}
