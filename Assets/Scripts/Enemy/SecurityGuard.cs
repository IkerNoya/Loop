using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SecurityGuard : Enemy
{
    public static event Action<Enemy, int> OnDetectedPlayer;
    [SerializeField] protected float speedAttack;


    public enum EstadosGuardia
    {
        Idle,
        Perseguir,
        Atacar,
        Morir,
        Count
    }
    //HAGO UN ENUM DE Eventos
    public enum EventosGuardia
    {
        Quieto,
        EnRangoDePersecucion,
        EnRangoDeAtaque,
        FueraDeRangoDePersecucion,
        FueraDeRangoDeAtaque,
        SinVida,
        Count
    }

    public EstadosGuardia test;

    private void OnEnable()
    {
        SecurityGuard.OnDetectedPlayer += LisentCallAlies;
    }
    private void OnDisable()
    {
        SecurityGuard.OnDetectedPlayer -= LisentCallAlies;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Awake()
    {
        base.Awake();
        // Aca defino las relaciones de estado y le hago el new al objeto FSM
        fsm = new FSM((int)EstadosGuardia.Count, (int)EventosGuardia.Count, (int)EstadosGuardia.Idle);
        fsm.SetRelations((int)EstadosGuardia.Idle, (int)EstadosGuardia.Perseguir, (int)EventosGuardia.EnRangoDePersecucion);
        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Atacar, (int)EventosGuardia.EnRangoDeAtaque);
        fsm.SetRelations((int)EstadosGuardia.Atacar, (int)EstadosGuardia.Perseguir, (int)EventosGuardia.FueraDeRangoDeAtaque);
        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Idle, (int)EventosGuardia.FueraDeRangoDePersecucion);

        fsm.SetRelations((int)EstadosGuardia.Idle, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);
        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);
        fsm.SetRelations((int)EstadosGuardia.Atacar, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);

        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Idle, (int)EventosGuardia.Quieto);
        fsm.SetRelations((int)EstadosGuardia.Atacar, (int)EstadosGuardia.Idle, (int)EventosGuardia.Quieto);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        switch (fsm.GetCurrentState())
        {
            case (int)EstadosGuardia.Idle:
                StopAIPathDestination();
                callAlies = true;
                break;
            case (int)EstadosGuardia.Perseguir:
                StartAIPathDestination();
                if (callAlies && enableCallAlies)
                {
                    if(OnDetectedPlayer != null)
                        OnDetectedPlayer(this, (int)EstadosGuardia.Perseguir);
                    callAlies = false;
                }
                break;
            case (int)EstadosGuardia.Atacar:
                callAlies = true;
                aiPath.maxSpeed = speedAttack;
                break;
            case (int)EstadosGuardia.Morir:
                callAlies = false;
                break;
        }
        CheckPlayerInRange();
        test = (EstadosGuardia)fsm.GetCurrentState();
    }
    public void CheckPlayerInRange()
    {
        Vector3 currentDistance = Vector3.zero;
        if (currentTarget != null)
        {
            //Debug.Log("A BUSCAR UWU");
            currentDistance = transform.position - currentTarget.position;
            if (currentDistance.magnitude <= distancePlayerInRange)
            {
                fsm.SendEvent((int)EventosGuardia.EnRangoDePersecucion);
            }
        }
    }
    public void LisentCallAlies(Enemy e, int state)
    {
        if (e == null || e == this) return;

        fsm.SendEvent(state);
    }
}
