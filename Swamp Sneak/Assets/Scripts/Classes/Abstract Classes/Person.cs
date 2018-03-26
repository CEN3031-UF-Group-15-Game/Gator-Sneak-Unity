using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class Person : Entity {

    //Variables shared across any type of person (Enemy & NPC)
    protected NavMeshAgent this_NavAgent;

    protected override void Init()
    {
        this_NavAgent = GetComponentInParent<NavMeshAgent>();
    }

    public float GetMaxMoveSpeed()
    {
        return this_NavAgent.speed;
    }

}
