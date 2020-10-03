using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float speed;
    [SerializeField] protected bool usePatrullaje;
    [SerializeField] Transform waypointsPatrullaje;
    
    protected FSM fsm;

    private void Awake()
    {
        
    }
    protected virtual void Attack() { }
}
