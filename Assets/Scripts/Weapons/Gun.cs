//using Audio;
using UnityEngine;

public class Gun : MonoBehaviour
{

	public GameObject bullet;
	public GameObject enemyBullet;
	public float bulletSpeed;
	public float fireRate;
	public float recoil;
	public float damage;
	public int amount = 1;
	public float spread;

	public bool ready = true;

	public void Fire()
	{
		if (!ready) return;

		//print("amount: " + amount);
		for (int i = 0; i < amount; i++)
		{
			SpawnBullet();
		}

		ready = false;
		Invoke("GetReady", fireRate);
	}

	private void GetReady()
	{
		ready = true;
	}

	private void SpawnBullet()
	{
		if (gameObject.CompareTag("Bullet"))
		{
			Sword();
			return;
		}
		GameObject newBullet;
		Vector3 spreadVector = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread));

		newBullet = Instantiate(bullet, transform.position, transform.rotation);
		

		//Change size depending on damage
		newBullet.transform.localScale *= (1 + (damage / 15));

		newBullet.layer = transform.parent.gameObject.layer + 1;
		newBullet.GetComponent<Rigidbody2D>().velocity = (transform.up * bulletSpeed) + spreadVector;

		//play sound
		if (transform.parent.name == "Player")
		{
			//AudioManager.Play("PlayerPistol");
			//CameraShake.ShakeOnce(0.2f, 0.1f);
		}
		else
		{
			//AudioManager.Play("Pistol");
		}

		//Set the damage for the bullet
		((Bullet)(newBullet.GetComponent(typeof(Bullet)))).SetDamage(damage);
	}

	public void EnemyWeapon()
	{
		bulletSpeed /= 2f;
	}

	private void Sword()
	{
		//CameraShake.ShakeOnce(0.2f, 0.1f);
		GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
		newBullet.layer = transform.parent.gameObject.layer + 1;
		((Bullet)(newBullet.GetComponent(typeof(Bullet)))).SetDamage(damage);
		newBullet.GetComponent<Rigidbody2D>().velocity = (transform.up * bulletSpeed);
		//AudioManager.Play("Sword");
	}
}
