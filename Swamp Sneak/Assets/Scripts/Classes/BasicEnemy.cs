using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy {
	public int degreeOfSight = 50;
	public int sightDistance = 5;

	private EnemyLineOfSight elos;
	
	protected override void Init() {
		base.Init();

		elos = gameObject.GetComponentInChildren<EnemyLineOfSight>();
		elos.SetDegreeOfSight(degreeOfSight);
		elos.SetSightDistance(sightDistance);
	}
}
