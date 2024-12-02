using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static State;
using UnityEngine.AI;

public class Chase : State
{

    public Chase(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.CHASE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        agent.speed = 4f;
        agent.isStopped = false;
        agent.SetDestination(player.position);
        base.Enter();
    }

    public override void Update()
    {
        if (CheckPlayerInLineOfSight(60f) && CheckPlayerIsNear(5f))
        {
            nextState = new Attack(npc, agent, anim, player);
            Debug.Log("Attaaack");
            base.Exit();
        } else
        {
            if (CheckPlayerInLineOfSight(60f) && CheckPlayerIsNear(15f))
            {
                agent.SetDestination(player.position);
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        agent.speed = 0f;
        agent.isStopped = true;
        base.Exit();
    }
}
