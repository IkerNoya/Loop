using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GeneratorEnemysManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int minAddedRandomGenerate = 1;
    public int maxAddedRandomGenerate = 2;
    public int minRandomGenerateEnemys = 1;
    public int maxRandomGenerateEnemys = 2;
    public GeneradorEnemys[] generadorEnemys;
    public OnEnableLevel enableLevelObject;

    private void Start()
    {
        for (int i = 0; i < generadorEnemys.Length; i++)
        {
            generadorEnemys[i].numberCurrentLevel = enableLevelObject.numberLevel;
        }
    }
    private void OnEnable()
    {
        OnEnableLevel.onEnableLevel += AddMaxRandomGenerate;
        OnEnableLevel.onEnableLevel += Enable;
    }
    private void OnDisable()
    {
        OnEnableLevel.onEnableLevel -= AddMaxRandomGenerate;
        OnEnableLevel.onEnableLevel += Enable;


    }
    public void AddMaxRandomGenerate(OnEnableLevel onEnableLevel, int currentLevel)
    {
        int resultRandom = UnityEngine.Random.Range(minAddedRandomGenerate, maxAddedRandomGenerate);
        maxRandomGenerateEnemys = maxRandomGenerateEnemys + resultRandom;
        minRandomGenerateEnemys = maxRandomGenerateEnemys - resultRandom;

        for (int i = 0; i < generadorEnemys.Length; i++)
        {
            generadorEnemys[i].maxEnemysGenerates = UnityEngine.Random.Range(minRandomGenerateEnemys, maxRandomGenerateEnemys);
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
    }
}
