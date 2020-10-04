using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemys : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeEnemysGenerates
    {
        GenerateCientifico,
        GenerateSecurityGuard,
        GenerateAll,
    }
    [SerializeField] float delayGenerate;
    [SerializeField] float auxDelayGenerate;
    public GameObject securityGuard_GO;
    public GameObject cientifico_GO;
    public TypeEnemysGenerates typeEnemysGenerates;

    private int countEnemysGenerates;
    private int maxEnemysGenerates;
    GameManager instanceGM;

    private void Awake()
    {
        instanceGM = GameManager.instanceGM;
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateEnemys()
    {
        switch (typeEnemysGenerates)
        {
            case TypeEnemysGenerates.GenerateAll:
                break;
            case TypeEnemysGenerates.GenerateCientifico:
                break;
            case TypeEnemysGenerates.GenerateSecurityGuard:
                break;
        }
    }
}
