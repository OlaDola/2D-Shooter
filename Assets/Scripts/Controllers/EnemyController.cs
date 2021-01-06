using System.Collections;
using System.Collections.Generic;
using Enemies;
using Players;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public LayerMask raycast;

	private bool active = true;

	public GameObject boi;
	public GameObject bigBoi;
	public GameObject flyingBoi;
	public GameObject suicideBoi;
	public GameObject sniperBoi;
	public GameObject barrel;

	private int maxNormalBois = 1;
	private int maxFlyingBois = 3;
	private int maxBigBois = 3;
	private int maxSuicideBois = 10;
	private int maxSniperBois = 2;

	public int normalBois, flyingBois, bigBois, suicideBois, sniperBois;

	public static List<GameObject> enemies;

	public static EnemyController instance;

	private float totalTimer;

	private float nTimer;
	private float newTime = 4.5f;
	private float minTime = 3f;

	private float bTimer;
	private float bNewTime = 8.5f;
	private float bMinTime = 5.5f;

	private float fTimer;
	private float fNewTime = 10.5f;
	private float fMinTime = 6.5f;

	private float suTimer;
	private float suNewTime = 18.5f;
	private float suMinTime = 14.5f;

	private float snTimer;
	private float snNewTime = 14.5f;
	private float snMinTime = 9.5f;

	private float barrelMax = 30f;
	private float barrelMin = 7f;

	public bool spawn;

	private void RestartTimers()
	{
		totalTimer = 0f;
		newTime = 3f;
		//bNewTime = 6f;
		//fNewTime = 7f;
		//suNewTime = 8;
		//snNewTime = 10f;

		nTimer = newTime;
		//bTimer = 0.1f;
		//fTimer = 0.1f;
		//suTimer = 0.1f;
		//snTimer = 0.1f;
	}

	private void Start()
	{
		enemies = new List<GameObject>();
		instance = this;
	}

	public void StartEnemies()
	{
		ClearEnemies();
		RestartTimers();
		enemies = new List<GameObject>();
		spawn = true;
		Invoke("SpawnBarrel", Random.Range(10, 20));
	}

	void Update()
	{
		if (!spawn) return;
		totalTimer += Time.deltaTime;

		//normal bois
		nTimer -= Time.deltaTime;
		if (nTimer <= 0)
			SpawnBoi();

		//big bois
		if (totalTimer < 20) return;
		bTimer -= Time.deltaTime;
		if (bTimer <= 0)
			SpawnBigBoi();

		//Flying bois
		if (totalTimer < 40) return;
		fTimer -= Time.deltaTime;
		if (fTimer <= 0)
			SpawnFlyingBoi();

		//Suicide bois
		if (totalTimer < 60) return;
		suTimer -= Time.deltaTime;
		if (suTimer <= 0)
			SpawnSuicideBoi();

		//Flying bois
		if (totalTimer < 80) return;
		snTimer -= Time.deltaTime;
		if (snTimer <= 0)
			SpawnSniperBoi();
	}

	private void SpawnBoi()
	{
		if (newTime > minTime)
		{
			newTime -= 0.05f;
		}

		nTimer = newTime;
		if (normalBois >= maxNormalBois) return;

		normalBois++;
		SpawnOneBoi(boi);
	}

	private void SpawnBigBoi()
	{
		if (bNewTime > bMinTime)
		{
			bNewTime -= 0.05f;
		}

		bTimer = bNewTime;
		if (bigBois >= maxBigBois) return;

		bigBois++;
		SpawnOneBoi(bigBoi);
	}

	private void SpawnOneBoi(GameObject boiToSpawn)
	{
		float x = Random.Range(-16f, 16f);
		float y = MapGenerator.floorPos.y + 6;

		Vector2 spawnPos = new Vector2(x, y);
		//if birdboi
		//if (boiToSpawn.GetComponent(typeof(Actor)) is FlyingEnemy)
		//{
		//	x = Random.Range(-16f, 16f);
		//	y = +Random.Range(0, 7f);
		//	spawnPos = new Vector2(x, y);
		//}
		//else
		//{
			RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 50, raycast);
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
				{
					SpawnOneBoi(boiToSpawn);
					return;
				}
			}
		//}

		Instantiate(boiToSpawn, spawnPos, transform.rotation);
	}

	private void SpawnFlyingBoi()
	{
		if (fNewTime > fMinTime)
		{
			fNewTime -= 0.05f;
		}

		fTimer = fNewTime;
		if (flyingBois >= maxFlyingBois) return;

		flyingBois++;
		SpawnOneBoi(flyingBoi);
	}

	private void SpawnSuicideBoi()
	{
		if (suNewTime > fMinTime)
		{
			suNewTime -= 0.05f;
		}

		suTimer = suNewTime;
		if (suicideBois >= maxSuicideBois) return;

		suicideBois++;
		SpawnOneBoi(suicideBoi);
	}

	private void SpawnSniperBoi()
	{
		if (snNewTime > fMinTime)
		{
			snNewTime -= 0.05f;
		}

		snTimer = snNewTime;
		if (sniperBois >= maxSniperBois) return;

		sniperBois++;
		SpawnOneBoi(sniperBoi);
	}

	public void ClearEnemies()
	{
		for (int i = 0; i < enemies.Count; i++)
		{
			print("destroying: " + enemies[i]);
			Destroy(enemies[i]);

		}

		normalBois = 0;
		bigBois = 0;
		flyingBois = 0;
		sniperBois = 0;
		suicideBois = 0;
		CancelInvoke();
	}

	private void SpawnBarrel()
	{
		if (barrelMax > barrelMin + 1)
			barrelMax -= 1f;

		float x = Random.Range(-16f, 16f);
		float y = 15;

		Vector3 euler = new Vector3(0, 0, Random.Range(0, 360));

		GameObject newBarrel = Instantiate(barrel, new Vector2(x, y), Quaternion.Euler(euler));

		Invoke("SpawnBarrel", Random.Range(barrelMin, barrelMax));
	}
}
