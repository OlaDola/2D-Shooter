using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
	public class Enemy : Actor
	{

		public GameObject[] weapons;
		public float[] chance;
		public LayerMask whatIsRaycastable;

		protected void EnemyStart()
		{
			EnemyController.enemies.Add(gameObject);
			maxMoveSpeed = 3;
			moveSpeed = 500;
			jumpForce = 500;
			maxJumps = 2;

			StartWeapon();
			weaponScript.EnemyWeapon();
		}

		private void Update()
		{
			if (Game.currentPlayer == null) return;

			Vector2 playerPos = Game.currentPlayer.transform.position;
			Vector2 lookPos = playerPos - (Vector2)transform.position;
			float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;

			//Rotate gun
			if (gun != null)
			{
				gun.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
			}

			RaycastHit2D hit = Physics2D.Raycast(transform.position, lookPos, 100, whatIsRaycastable);

			float playerX = playerPos.x;
			Behaviour(playerX, hit);
		}

		protected virtual void Behaviour(float playerX, RaycastHit2D hit2D)
		{

		}

		
		protected virtual void StartWeapon()
		{

			if (weapons.Length == 0)
				return;

			if (weapons.Length == 1)
			{
				GiveWeapon(weapons[0]);
				return;
			}

			float rand = Random.Range(0, 1f);
			float a = 0;

			for (int i = 0; i < chance.Length; i++)
			{
				a += chance[i];
				if (rand <= a)
				{
					GiveWeapon(weapons[i]);
					return;
				}
			}
			GiveWeapon(weapons[Random.Range(0, weapons.Length)]);
		}

	}
}
