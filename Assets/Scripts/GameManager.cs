using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int currentCountEnemys;
    bool gameStarted;
    bool enableCheckNextLevel = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckNextLevel()
    {
        if (currentCountEnemys <= 0)
        {
            gameStarted = false;
        }
        else
        {
            gameStarted = true;
        }
    }
    public void SetEnableCheckNextLevel(bool _enableCheckNextLevel)
    {
        enableCheckNextLevel = _enableCheckNextLevel;
    }
    public bool GetEnableCheckNextLevel()
    {
        return enableCheckNextLevel;
    }
}
