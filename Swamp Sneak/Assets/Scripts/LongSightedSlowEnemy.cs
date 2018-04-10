using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSightedSlowEnemy : Enemy {

    protected override void Init() {
        base.Init();

        degreeOfSight = 90;
        sightDistance = 17;
        this_NavAgent.speed = 1.3F;

        enemy_los.SetDegreeOfSight(degreeOfSight);
        enemy_los.SetSightDistance(sightDistance);
    }
}
