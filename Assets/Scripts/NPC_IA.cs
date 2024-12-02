using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_IA : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField]
    Transform player;
    State currentState;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentState = new Idle(gameObject, agent, anim, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
