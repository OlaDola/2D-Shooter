using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
//using Audio;
using Players;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	private float damage = 1f;
	//public GameObject destroyFx;
	private bool gone;

	private void OnTriggerEnter2D(Collider2D other)
	{
		print("Something");
		if (gone)
		{
			Invoke("Reset", 0.02f);
			return;
		}
		gone = true;
		if (other.CompareTag("Actor"))
		{
			Actor targetHit = other.GetComponent(typeof(Actor)) as Actor;

            print(targetHit.tag);

			if (other.name == "Player")
			{
				//CameraShake.ShakeOnce(0.3f, 1.6f);
				//AudioManager.Play("PlayerDamage");
			}
			else //AudioManager.Play("EnemyDamage");


			//damage
			if (targetHit != null)
			{

				targetHit.Damage(damage);
				targetHit.GetRb().AddForce(transform.up * 500f);
				//Destroy(this.gameObject);
			}
			Destroy(this.gameObject);
		}

		//if (destroyFx != null)
		//	Instantiate(destroyFx, transform.position, transform.rotation);

		if (other.CompareTag("Bullet"))
		{
			print(other.tag);
			if (transform.localScale.x <= other.transform.localScale.x)
			{
				Destroy(this.gameObject);
				return;
			}
			return;
		}

		if (other.gameObject.CompareTag("Wall"))
		{
			print(other.tag);
			//AudioManager.Play("WallHit");
			Destroy(this.gameObject);
		}


	}

	public void SetDamage(float f)
	{
		damage = f;
	}

	private void Reset()
	{
		gone = false;
	}
}
