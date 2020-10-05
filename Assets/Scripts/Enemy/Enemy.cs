using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
public class Enemy : Character
{

    public static event Action<Enemy> OnStartEnemy;
    public static event Action<Enemy> OnDieEnemy;

    [SerializeField]
    private bool dieOnPlayerDead = true;
    private AIDestinationSetter aiPathDestination;
    protected AIPath aiPath;
    protected FSM fsm;
    bool IDead;

    [SerializeField] protected bool enableCallAlies = true;
    protected bool callAlies;
    [SerializeField]
    protected int layerEnvarioment = 9;

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
    protected virtual void Start()
    {
        if (OnStartEnemy != null)
            OnStartEnemy(this);
    }
    private void OnEnable()
    {
        PlayerController.OnDiePlayer += DeadForFinishGame;
    }
    private void OnDisable()
    {
        PlayerController.OnDiePlayer -= DeadForFinishGame;
    }
    protected virtual void Update()
    {
        CheckCurrentTarget();
    }
    public void CheckCurrentTarget()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
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
    }
    public void DeadForFinishGame(PlayerController p)
    {
        Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            //Debug.Log("OUCH");
            Bullet b = collision.GetComponent<Bullet>();
            if (b != null)
            {
                if (b.enemyUser != this && b.GetUser() != Bullet.User.Enemy)
                {
                    //Debug.Log("Damage:" + b.GetDamage());
                    SetHP(GetHP() - b.GetDamage());
                    if (GetHP() <= 0 && !IDead) 
                    {
                        SetHP(1);
                        if (OnDieEnemy != null)
                            OnDieEnemy(this);

                        SetHP(0);
                        IDead = true;
                    }
                    Destroy(b.gameObject);
                }
            }
        }
    }
}
