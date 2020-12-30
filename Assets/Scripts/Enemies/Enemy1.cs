using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Enemies
{
	public class Enemy1 : Enemy
	{
		private bool dead;

		private new void Start()
		{
			base.Start();
			EnemyStart();
		}

		protected override void Behaviour(float playerX, RaycastHit2D hit2D)
		{
			if (playerX > transform.position.x)
				Move(1);
			else Move(-1);

			if (Random.Range(0, 1f) < 0.0025)
				Jump();

			if (Random.Range(0, 1f) < 0.01f)
				weaponScript.Fire();
		}

		protected override void OnDeathAction()
		{
			if (dead) return;
			dead = true;
			EnemyController.instance.normalBois--;
		}
	}
}
