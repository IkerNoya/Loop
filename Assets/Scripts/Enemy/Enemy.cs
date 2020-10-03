using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : Character
{
    [SerializeField] protected bool usePatrullaje;
    [SerializeField] Transform waypointsPatrullaje;
    private AIDestinationSetter aiPathDestination;
    private AIPath aiPath;
    protected FSM fsm;
    private Vector3 currentDistanceWhitPlayer;
    private Vector3 auxCurrentDistanceWhitPlayer;
    private Transform currentTarget;
    private PlayerController[] targets; 
    protected virtual void Awake()
    {
        aiPathDestination = GetComponent<AIDestinationSetter>();
        targets = FindObjectsOfType<PlayerController>();
        aiPath = GetComponent<AIPath>();

        aiPath.maxSpeed = speed;
        currentDistanceWhitPlayer = transform.position - targets[0].transform.position;
        aiPathDestination.target = targets[0].transform;
    }
    protected virtual void Update()
    {
        CheckCurrentTarget();
    }
    public void CheckCurrentTarget()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            auxCurrentDistanceWhitPlayer = transform.position - targets[i].transform.position;
            if (currentDistanceWhitPlayer.magnitude > auxCurrentDistanceWhitPlayer.magnitude)
            {
                currentDistanceWhitPlayer = auxCurrentDistanceWhitPlayer;
                currentTarget = targets[i].transform;
                aiPathDestination.target = currentTarget;
            }
        }
    }
    public void StopAIPathDestination()
    {
        aiPath.maxSpeed = 0;
    }
    public void StartAIPathDestination()
    {
        aiPath.maxSpeed = speed;
    }
    protected virtual void Attack() { }
}
