using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : Character
{
    private AIDestinationSetter aiPathDestination;
    protected AIPath aiPath;
    protected FSM fsm;

    [SerializeField] protected bool enableCallAlies = true;
    protected bool callAlies;
    [SerializeField]
    protected int layerEnvarioment = 9;

    [SerializeField] protected float distancePlayerInRange;
    private Vector3 currentDistanceWhitPlayer;
    private Vector3 auxCurrentDistanceWhitPlayer;
    protected Transform currentTarget;
    protected PlayerController[] targets;
    protected virtual void Awake()
    {
        aiPathDestination = GetComponent<AIDestinationSetter>();
        targets = FindObjectsOfType<PlayerController>();
        aiPath = GetComponent<AIPath>();

        aiPath.maxSpeed = speed;
        currentDistanceWhitPlayer = transform.position - targets[0].transform.position;
        aiPathDestination.target = targets[0].transform;
    }
    protected virtual void Start(){}
    protected virtual void Update(){ CheckCurrentTarget(); }
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
        //aiPath.enabled = false;
    }
    public void StartAIPathDestination()
    {
        aiPath.maxSpeed = speed;
        //aiPath.enabled = true;
    }

    protected virtual void Attack() { }
}
