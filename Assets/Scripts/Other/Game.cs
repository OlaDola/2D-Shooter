
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;


public class Game : MonoBehaviour
{
    public GameObject pistol;
    public GameObject currentWeapon;
    public GameObject normalBullet;

	public MapGenerator MapGenerator;
	public EnemyController EnemyController;

	//public Button height, speed, triple, shotgun, rifle, sniper, saber, explosive;
	//public TextMeshProUGUI displayHp;

	//Player prefab
	public GameObject player;

    //public GameObject playerUI;
    //public GameObject menuUI;
    //public GameObject gameOverUI;

    public static GameObject currentPlayer;
    public static Player currentPlayerScript;

    public static int health = 100;

    public static bool inGame;

    //public PostProcessingProfile ppProfile;

    private static Game instance;

    private float newMoveSpeed;
    private float newMaxSpeed;
    private float newJumpForce;
    private int newJumps;
    private GameObject bullet;

    private void Start()
    {
        instance = this;
        StartGame();
    }

    public void StartGame()
    {
        inGame = true;

        float x = Random.Range(-9f, 9f);
        float y = MapGenerator.floorPos.y;

        Vector2 SpawnPos = new Vector2(x, y);

        GameObject newPlayer = Instantiate(player, SpawnPos, transform.rotation);
        newPlayer.name = "Player";
        currentPlayer = newPlayer;
        currentPlayerScript = currentPlayer.GetComponent(typeof(Player)) as Player;

		ResetUpgrades();

		//Perform the upgrades

		UpgradePlayer();

        MapGenerator.instance.GenerateMap();
        EnemyController.instance.StartEnemies();
    }


	private void Update()
	{
		if (inGame && currentPlayer == null /*&& !gameOverUI.activeInHierarchy*/)
		{
			Invoke("GameOver", 1f);
			inGame = false;
		}
	}

	//public void EnterLobby()
	//{
	//	ppProfile.depthOfField.enabled = true;
	//	playerUI.SetActive(false);
	//	gameOverUI.SetActive(false);
	//	menuUI.SetActive(true);
	//	health = 100;
	//	inGame = false;
	//	ScoreController.instance.score = 0;
	//	ResetUpgrades();
	//	MapGenerator.instance.CleanMap();
	//	EnemyController.instance.ClearEnemies();
	//	Camera.main.orthographicSize = 2;
	//	AudioManager.Play("Lobby");
	//}

	private void UpgradePlayer()
	{
		currentPlayerScript.GiveWeapon(currentWeapon);
		//speed
		currentPlayerScript.maxMoveSpeed = newMaxSpeed;
		currentPlayerScript.moveSpeed = newMoveSpeed;
		//Height
		currentPlayerScript.jumpForce = newJumpForce;
		//Triple
		currentPlayerScript.maxJumps = newJumps;

		//bullet
		currentPlayerScript.weaponScript.bullet = bullet;
	}

    private void ResetUpgrades()
    {
        currentWeapon = pistol;
        newMoveSpeed = 3800;
        newMaxSpeed = 14;
        newJumpForce = 850;
        newJumps = 2;
        health = 20;

        //height.interactable = true;
        //triple.interactable = true;
        //speed.interactable = true;
        //shotgun.interactable = true;
        //rifle.interactable = true;
        //sniper.interactable = true;
        //saber.interactable = true;
        //explosive.interactable = true;

        //other
        bullet = normalBullet;

        //displayHp.text = "HP: " + health;
    }

    public void Buy(GameObject w)
	{
		//oof please dont judge my code
		if (w.name == "Shotgun")
		{
			if (health - 15 > 0)
			{
				currentWeapon = w;
				//TakeMoney(15);
				//DisableWeaponButtons();
			}
			else { //AudioManager.Play("Error"); 
			}
		}
		else if (w.name == "Rifle")
		{
			if (health - 25 > 0)
			{
				currentWeapon = w;
				TakeMoney(25);
				DisableWeaponButtons();
			}
			else { //AudioManager.Play("Error"); 
			}
		}
		else if (w.name == "Sniper")
		{
			if (health - 30 > 0)
			{
				currentWeapon = w;
				TakeMoney(30);
				DisableWeaponButtons();
			}
			else { //AudioManager.Play("Error"); 
					}
		}
		else if (w.name == "LightSaber")
		{
			if (health - 50 > 0)
			{
				currentWeapon = w;
				TakeMoney(50);
				DisableWeaponButtons();
			}
			else { //AudioManager.Play("Error"); 
					}
		}
		else if (w.name == "Rocket Launcher")
		{
			if (health - 60 > 0)
			{
				currentWeapon = w;
				TakeMoney(60);
				DisableWeaponButtons();
			}
			else { //AudioManager.Play("Error"); 
			}
		}
	}

	private void DisableWeaponButtons()
	{
		//sniper.interactable = false;
		//rifle.interactable = false;
		//shotgun.interactable = false;
		//saber.interactable = false;
	}

	public void BuySpeed(int cost)
	{
		if (health - cost <= 0)
		{
			//AudioManager.Play("Error");
			return;
		}

		newMoveSpeed = 4200;
		newMaxSpeed = 16;
		//speed.interactable = false;
		TakeMoney(cost);
	}

	public void BuyHeight(int cost)
	{
		if (health - cost <= 0)
		{
			//AudioManager.Play("Error");
			return;
		}

		newJumpForce = 1000;
		//height.interactable = false;
		TakeMoney(cost);
	}

	public void BuyTriple(int cost)
	{
		if (health - cost <= 0)
		{
			//AudioManager.Play("Error");
			return;
		}

		newJumps = 3;
		//triple.interactable = false;
		TakeMoney(cost);
	}

	public void BuyExplosives(int cost)
	{
		if (health - cost <= 0)
		{
			//AudioManager.Play("Error");
			return;
		}

		//bullet = explosiveBullet;
		//explosive.interactable = false;
		TakeMoney(cost);
		print(bullet);
	}

	public void TakeMoney(int f)
	{
		health -= f;
		//AudioManager.Play("Buy");
		//displayHp.text = "HP: " + health;
		//CameraShake.ShakeOnce(0.2f, 1.5f);
	}

	public void GameOver()
	{
		//ppProfile.depthOfField.enabled = true;
		//playerUI.SetActive(false);
		//GameOverUI.instance.UpdateScore();
		//gameOverUI.SetActive(true);

		//EnemyController.instance.spawn = false;

		//AudioManager.Stop("Song");
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Heal()
	{
		//AudioManager.Play("Heal");
	}
}
