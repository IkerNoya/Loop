﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int currentCountEnemys;
    bool gameStarted;
    bool enableCheckNextLevel = false;
    public static GameManager instanceGM;
    [SerializeField] Music m;
    [SerializeField] GameObject camvasGameOver;
    private void Awake()
    {
        if (instanceGM == null)
        {
            instanceGM = this;
        }
        else
        {
            Destroy(instanceGM.gameObject);
        }
    }
    private void Start() {
        m.StartMusicGameplay();
    }
    private void OnEnable()
    {
        Enemy.OnStartEnemy += AddedEnemy;
        Enemy.OnDieEnemy += SubstractEnemy;
        PlayerController.OnDiePlayer += EnableCamvasGameOver;
    }
    private void OnDisable()
    {
        Enemy.OnStartEnemy -= AddedEnemy;
        Enemy.OnDieEnemy -= SubstractEnemy;
        PlayerController.OnDiePlayer -= EnableCamvasGameOver;
    }

    public int GetCurrentCountEnemy()
    {
        return currentCountEnemys;
    }
    void AddedEnemy(Enemy e)
    {
        currentCountEnemys++;
        enableCheckNextLevel = true;
    }
    void SubstractEnemy(Enemy e)
    {
        currentCountEnemys--;
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
    public void EnableCamvasGameOver(PlayerController p)
    {
        camvasGameOver.SetActive(true);
    }
}
