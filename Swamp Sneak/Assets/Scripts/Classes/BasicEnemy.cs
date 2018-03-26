using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy {
	
	protected override void Init() {
		base.Init();

        degreeOfSight = 50;
        sightDistance = 5;
        thisNavAgent.speed = 3;

        enemy_los.SetDegreeOfSight(degreeOfSight);
		enemy_los.SetSightDistance(sightDistance);
	}
}
