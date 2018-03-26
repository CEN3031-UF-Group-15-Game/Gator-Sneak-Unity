using UnityEngine.AI;

public abstract class Person : Entity {

    //Variables shared across any type of person (Enemy & NPC)
    protected NavMeshAgent thisNavAgent;

    protected override void Init()
    {
        thisNavAgent = GetComponentInParent<NavMeshAgent>();
    }

    public float GetMaxMoveSpeed()
    {
        return thisNavAgent.speed;
    }

}
