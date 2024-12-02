using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    private float randomTime;
    private float timePassed;
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        randomTime = Random.Range(1f, 3f);
        timePassed = 0;
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > randomTime)
        {
            //Pasamos a patrol
            nextState = new Patrol(npc,agent,anim,player);
            base.Exit();
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
