using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GeneratorEnemysManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int countRoundsAddRandomGenerate = 1;
    [SerializeField] int rounds;
    public int minAddedRandomGenerate = 1;
    public int maxAddedRandomGenerate = 2;
    public int minRandomGenerateEnemys = 1;
    public int maxRandomGenerateEnemys = 2;
    public GeneradorEnemys[] generadorEnemys;
    public OnEnableLevel enableLevelObject;

    private void Start()
    {
        rounds = 0;
        for (int i = 0; i < generadorEnemys.Length; i++)
        {
            generadorEnemys[i].numberCurrentLevel = enableLevelObject.numberLevel;
        }
    }
    private void OnEnable()
    {
        OnEnableLevel.onEnableLevel += Enable;
    }
    private void OnDisable()
    {
        OnEnableLevel.onEnableLevel -= Enable;
    }
    public void AddMaxRandomGenerate()
    {
        if (rounds >= countRoundsAddRandomGenerate)
        {
            int resultRandom = UnityEngine.Random.Range(minAddedRandomGenerate, maxAddedRandomGenerate);
            maxRandomGenerateEnemys = maxRandomGenerateEnemys + resultRandom;
            minRandomGenerateEnemys = maxRandomGenerateEnemys - resultRandom;

            for (int i = 0; i < generadorEnemys.Length; i++)
            {
                generadorEnemys[i].maxEnemysGenerates = UnityEngine.Random.Range(minRandomGenerateEnemys, maxRandomGenerateEnemys);
            }
            rounds = 0;
        }
        else
        {
            rounds++;
        }
    }
    public void Enable(OnEnableLevel onEnableLevel, int currentLevel)
    {
        if (onEnableLevel != null)
        {
            for (int i = 0; i < generadorEnemys.Length; i++)
            {
                generadorEnemys[i].gameObject.SetActive(true);
            }
        }
        AddMaxRandomGenerate();
    }
}
