using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static State;
using UnityEngine.AI;

public class LookLastPoint : State
{
    float animationDuration;
    float currentTime;
    public LookLastPoint(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.LOOK;
    }

    public override void Enter()
    {
        Debug.Log("LookLastPoint");
        anim.SetTrigger("isRunning");
        agent.speed = 4f;
        agent.isStopped = false;
        agent.SetDestination(player.position);
        base.Enter();
    }

    public override void Update()
    {

        if (CheckPlayerInLineOfSight(60f) && CheckPlayerIsNear(15f))
        {
            nextState = new Chase(npc, agent, anim, player);
            base.Exit();
        }
        if (agent.remainingDistance <= 2f)
        {
            nextState = new Patrol(npc, agent, anim, player);
            base.Exit();
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isShooting");
        agent.speed = 0f;
        agent.isStopped = true;
        base.Exit();
    }
}
