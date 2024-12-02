using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static State;
using UnityEngine.AI;

public class Attack : State
{
    float animationDuration;
    float currentTime;
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        anim.SetTrigger("isShooting");
        animationDuration = anim.GetCurrentAnimatorStateInfo(0).length;
        currentTime = 0f;
        agent.speed = 0f;
        agent.isStopped = true;

        base.Enter();
    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        npc.transform.LookAt(new Vector3(player.position.x,0, player.position.z));
        if(currentTime > animationDuration)
        {
            nextState = new Chase(npc, agent, anim, player);
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
