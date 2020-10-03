using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected bool usePatrullaje;
    [SerializeField] Transform waypointsPatrullaje;
    protected FSM fsm;

    protected virtual void Attack() { }
}
