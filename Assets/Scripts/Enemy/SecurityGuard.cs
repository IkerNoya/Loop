﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : Enemy
{
    public enum EstadosGuardia
    {
        Idle,
        Patrullar,
        Perseguir,
        Atacar,
        Morir,
        Count
    }
    //HAGO UN ENUM DE Eventos
    public enum EventosGuardia
    {
        EmpezarPatrullaje,
        EnRangoDePersecucion,
        EnRangoDeAtaque,
        FueraDeRangoDePersecucion,
        FueraDeRangoDeAtaque,
        SinVida,
        Count
    }

    protected override void Awake()
    {
        base.Awake();
        // Aca defino las relaciones de estado y le hago el new al objeto FSM
        fsm = new FSM((int)EstadosGuardia.Count, (int)EventosGuardia.Count, (int)EstadosGuardia.Idle);
        fsm.SetRelations((int)EstadosGuardia.Idle, (int)EstadosGuardia.Patrullar, (int)EventosGuardia.EmpezarPatrullaje);
        fsm.SetRelations((int)EstadosGuardia.Patrullar, (int)EstadosGuardia.Perseguir, (int)EventosGuardia.EnRangoDePersecucion);
        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Atacar, (int)EventosGuardia.EnRangoDeAtaque);
        fsm.SetRelations((int)EstadosGuardia.Atacar, (int)EstadosGuardia.Perseguir, (int)EventosGuardia.FueraDeRangoDeAtaque);
        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Patrullar, (int)EventosGuardia.FueraDeRangoDePersecucion);

        fsm.SetRelations((int)EstadosGuardia.Idle, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);
        fsm.SetRelations((int)EstadosGuardia.Patrullar, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);
        fsm.SetRelations((int)EstadosGuardia.Perseguir, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);
        fsm.SetRelations((int)EstadosGuardia.Atacar, (int)EstadosGuardia.Morir, (int)EventosGuardia.SinVida);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        switch (fsm.GetCurrentState())
        {
            case (int)EstadosGuardia.Idle:
                break;
            case (int)EstadosGuardia.Patrullar:
                break;
            case (int)EstadosGuardia.Perseguir:
                break;
            case (int)EstadosGuardia.Atacar:
                break;
            case (int)EstadosGuardia.Morir:
                break;
        }
    }
}
