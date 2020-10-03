using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : Character
{
    [SerializeField] protected float speed;
    [SerializeField] protected bool usePatrullaje;
    [SerializeField] Transform waypointsPatrullaje;
    private AIDestinationSetter aiPathDestination;
    protected FSM fsm;
    private Transform currentTarget;
    private PlayerController[] targets; 
    private void Awake()
    {
        aiPathDestination = GetComponent<AIDestinationSetter>();
        targets = FindObjectsOfType<PlayerController>();
    }

    public void CheckCurrentTarget()
    {
        for (int i = 0; i < targets.Length; i++)
        {

        }
    }
    protected virtual void Attack() { }
}
