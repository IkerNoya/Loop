using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float hp;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region SETTERS/GETTERS
    public void SetHP(float healthPoints)
    {
        hp = healthPoints;
    }
    public float GetHP()
    {
        return hp;
    }
    #endregion
}
