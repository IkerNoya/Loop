using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected bool usePatrol;
    [SerializeField] Transform waypointsList;
    protected FSM fsm;

    protected virtual void Attack() { }
}
