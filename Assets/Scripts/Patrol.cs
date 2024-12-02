using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    private int currentPoint;
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        anim.SetTrigger("isWalking");
        agent.speed = 3f;
        agent.isStopped = false;
        currentPoint = 0;
        agent.SetDestination(GameEnvironment.Singleton.Checkpoints[0].transform.position);
        base.Enter();
    }

    public override void Update()
    {
        if(agent.remainingDistance <= 2f)
        {
            currentPoint++;
            if(currentPoint >= GameEnvironment.Singleton.Checkpoints.Count)
            {
                currentPoint = 0;
            }
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentPoint].transform.position);
        }
        if(CheckPlayerInLineOfSight(60f) && CheckPlayerIsNear(15f))
        {
            nextState = new Chase(npc, agent, anim, player);
            base.Exit();
        }
    }

    public override void Exit()
    {
        agent.speed = 0f;
        agent.isStopped = true;
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
