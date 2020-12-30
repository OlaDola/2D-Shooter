﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Players
{
    public class Player : Actor 
	{
		private new void Start()
		{
			base.Start();
		}

		private void Update()
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 dirRelativeToPlayer = (mousePos - (Vector2)transform.position).normalized;
			//Input

			float mx =CrossPlatformInputManager.GetAxis("Horizontal");

			if (mx<0)
				Move(-1);

			if (mx>0)
				Move(1);

			bool jump = CrossPlatformInputManager.GetButtonDown("Jump");

			if (jump)
			{
				//Debug.Log("Jump");
				Jump();
			}

			mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
			Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
			lookPos = lookPos - transform.position;
			float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
			gun.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

			if (CrossPlatformInputManager.GetButtonDown("Shoot"))
			{
				Fire();
			}

			//Friction if not moving
			if (mx==0)
			{
				StopMoving();
				if (winding)
					Move(0);
			}
		}
	}
}
