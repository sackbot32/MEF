using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Para poder usar el NavMesh

public class State
{
    // Diferentes estados en los que puede estar el NPC.
    public enum STATE
    {
        IDLE, PATROL, CHASE, ATTACK, SLEEP
    };

    // Fase en la que se encuentra el estado.
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name; // Nombre del estado actual.
    protected EVENT stage; // Fase en la que nos encontramos.
    protected State nextState; // Estado al que vamos a cambiar

    protected GameObject npc; // GameObject del NPC.
    protected Animator anim; // Animator del NPC.
    protected NavMeshAgent agent; // NavMeshAgent del NPC.
    protected Transform player; // Transform del PLAYER, para poder controlar dónde se encuentra en cada momento

    //Declaración de variables
    //que se vayan a utilizar para 
    //control del contexto

    // Constructor for State
    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        stage = EVENT.ENTER; //Indicamos que lance el método Enter();
        
        npc = _npc;         //Asignación del NPC y sus componentes
        agent = _agent;
        anim = _anim;
        
        player = _player;   //Asignación del jugador
    }

    // Fases por las que atraviesa el ESTADO.
    public virtual void Enter() { stage = EVENT.UPDATE; } // Se ejecuta primero y establece los parámetros del ESTADO que se va a ejecutar
    public virtual void Update() { stage = EVENT.UPDATE; } // Una vez aquí, se sigue ejecutando hasta que el contexto indique cambiar de estado
    public virtual void Exit() { stage = EVENT.EXIT; } // Limpia el ESTADO y restablece los valores.

    // Método que se ejecutará externamente, y llamará a cada una de las fases del estado.
    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState; // Observa que este método devuelve un "estado".
        }
        return this; // Si no devolvemos ningún estado, seguimos en el que estamos.
    }
    
    protected bool CheckPlayerIsNear(float maxDistance)
    {
        float dist = Vector3.Distance(npc.transform.position,player.position);
        return dist < maxDistance;
    }

    protected bool CheckPlayerInLineOfSight(float maxAngle)
    {
        bool isInAngle = false;
        Vector3 dir = player.position - npc.transform.position;
        float angle = Vector3.Angle(npc.transform.forward, dir);
        if(angle < maxAngle)
        {
            isInAngle = true;
        }
        return isInAngle;
    }
}

