using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hide : State
{
    private int randomPoint;
    public Hide(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.HIDE;
    }

    public override void Enter()
    {
        Debug.Log("Hide");
        anim.SetTrigger("isRunning");
        agent.speed = 5f;
        agent.isStopped = false;
        randomPoint = Random.Range(0,GameEnvironment.Singleton.HidePoints.Count);
        agent.SetDestination(GameEnvironment.Singleton.Checkpoints[randomPoint].transform.position);
        base.Enter();
    }

    public override void Update()
    {
        if(agent.remainingDistance <= 2f)
        {
            nextState = new Idle(npc, agent, anim, player);
            base.Exit();
        }
    }

    public override void Exit()
    {
        agent.speed = 0f;
        agent.isStopped = true;
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
