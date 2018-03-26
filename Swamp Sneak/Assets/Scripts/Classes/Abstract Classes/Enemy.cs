public abstract class Enemy : Person {

    //Variable declarations that shared across all Enemy SubClasses
        //These variables were made public and given default values so they could be seen in the editor
    public int degreeOfSight; 
    public int sightDistance;
    protected EnemyLineOfSight enemy_los;

    protected override void Init()
    {
        base.Init();

        enemy_los = gameObject.GetComponentInChildren<EnemyLineOfSight>();
    }
}
