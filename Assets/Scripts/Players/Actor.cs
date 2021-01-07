using System;
using Audio;
using Microsoft.Win32;
using TMPro;
using UnityEngine;

namespace Players
{
	public class Actor : MonoBehaviour
	{
		//public Canvas canvas;
		
		public HealthBar healthBar;

		private bool scored;
		//how many points enemy is worth
		public int points;

		public Transform footPos;
		public Transform GunPos;
		public GameObject jumpFx;
		public GameObject playerExplodeFx;
		public GameObject scoreFx;

		private bool facingRight;
		private bool jumpReady = true;
		private bool moving;
		protected bool winding;

		//Grounded stuff
		private bool grounded;
		public LayerMask whatIsGround;

		//variables
		public float moveSpeed = 2700f;
		public float jumpForce = 850f;
		private float windupSpeed = 3f;

		private int currentJumps;
		public int maxJumps = 2;

		//Health and stuff
		public float Health { get; set; }
		public float startHealth = 100;

		private float framesFlashing = 7f;

		//limit
		public float maxMoveSpeed = 14;

		//Rb
		private Rigidbody2D rb;

		//
		public GameObject gun;
		public Gun weaponScript;

		//Sprites and stuff
		private SpriteRenderer sprite;
		protected Color actorColor;

		//standards
		private Vector2 standardScale;

		protected void Start()
		{
			currentJumps = maxJumps;
			facingRight = true;
			rb = GetComponent<Rigidbody2D>();
			sprite = GetComponent<SpriteRenderer>();
			actorColor = sprite.color;
			weaponScript = gun.GetComponent(typeof(Gun)) as Gun;
			sprite.flipX = true;
			Health = startHealth;
			standardScale = transform.localScale;
			//healthBar=canvas.GetComponent(typeof(HealthBar)) as HealthBar;
			healthBar.SetMaxHealth(startHealth);
		}

		private void FixedUpdate()
		{
			SlowDown();
		}

		private void LateUpdate()
		{
			grounded = Physics2D.OverlapCircle(footPos.position, 0.7f, whatIsGround);
			if (currentJumps < maxJumps && grounded)
				currentJumps = maxJumps;

			if (transform.localScale.y < standardScale.y)
				transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + 0.03f);
			if (transform.localScale.x < standardScale.x)
				transform.localScale = new Vector2(transform.localScale.x + 0.03f, transform.localScale.y);
		}

		/// <summary>
		/// Move the player in given direction
		/// </summary>
		/// <param name="dir">1 for right, -1 for left. 0 for... nothing?</param>
		protected void Move(int dir)
		{
			//flip player
			FlipActor(dir);

			//Actually move player
			moving = true;

			float xVel = rb.velocity.x;

			if (xVel < maxMoveSpeed && dir > 0)
				rb.AddForce(moveSpeed * Time.deltaTime * Vector2.right * dir);
			else if (xVel > -maxMoveSpeed && dir < 0)
				rb.AddForce(moveSpeed * Time.deltaTime * Vector2.right * dir);

			if (dir == 0)
			{

			}


			//If player is turning around, help turn faster
			if (xVel > 0.2f && dir < 0)
				rb.AddForce(moveSpeed * 3.2f * Time.deltaTime * -Vector2.right);
			if (xVel < 0.2f && dir > 0)
			{
				rb.AddForce(moveSpeed * 3.2f * Time.deltaTime * Vector2.right);
			}
		}

        public void Damage(float damage)
        {
            Health -= damage;
			healthBar.SetHealth(Health);
			if (Health <= 0)
            {
                Kill();
                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (scored) return;
                    scored = true;
                    ScoreController.AddScore(points);
                    GameObject scorePop = Instantiate(scoreFx, transform.position, Quaternion.identity);
                    scorePop.GetComponentInChildren<TextMeshProUGUI>().text = "" + points;
                }
            }
            DamageFlash();
        }

        private void DamageFlash()
		{
			sprite.color = new Color(1, 1, 1, 1f);
			Invoke("ResetColor", Time.deltaTime * framesFlashing);
		}

		private void ResetColor()
		{
			sprite.color = actorColor;
		}

		private void SlowDown()
		{
			if (moving) return;

			//If no key pressed but still moving, slow down player
			if (rb.velocity.x > 0.2f)
				rb.AddForce(moveSpeed * Time.deltaTime * -Vector2.right);
			else if (rb.velocity.x < -0.2f)
				rb.AddForce(moveSpeed * Time.deltaTime * Vector2.right);
		}

		protected void StopMoving()
		{
			moving = false;
		}

		private void FlipActor(int dir)
		{
			if ((facingRight && dir < 0) || !facingRight && dir > 0)
			{
				facingRight = !facingRight;
				sprite.flipX = !sprite.flipX;
			}
		}

		protected void Fire()
		{
			if (!weaponScript.ready) return;
			weaponScript.Fire();
			rb.AddForce(-gun.transform.up * weaponScript.recoil);
		}

		protected void Jump()
		{
			if (currentJumps <= 1) return;

			float angleMod = transform.rotation.eulerAngles.z % 180;

			if (angleMod < 45 || angleMod > 135)
				transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y / 4);
			else
				transform.localScale = new Vector2(transform.localScale.x / 4, transform.localScale.y);

			//Spawn jump fx
			//Instantiate(jumpFx, transform.position, transform.rotation);

			//Play jump sound
			AudioManager.Play("Jump");

			rb.velocity = new Vector2(rb.velocity.x, 0);

			rb.AddForce(Vector2.up * jumpForce);
			currentJumps--;
			winding = false;
		}

		private void JumpReset()
		{
			currentJumps = maxJumps;
		}

		public void GiveWeapon(GameObject weapon)
		{
			//Destroy(gun);
			gun = Instantiate(weapon, GunPos.transform.position, GunPos.transform.rotation);
			weaponScript = gun.GetComponent(typeof(Gun)) as Gun;
			gun.transform.parent = this.transform;
		}

		public void Kill()
		{
			if (gameObject.name == "Player")
			{
				AudioManager.Play("PlayerDeath");
				//CameraShake.ShakeOnce(0.6f, 1.4f);
			}
			else
			{
				AudioManager.Play("EnemyDeath");
			}
			Health = -1;
			//GameObject playerExplosion = Instantiate(playerExplodeFx, transform.position, transform.rotation);
			//ParticleSystem ps = playerExplosion.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
			//ps.startColor = actorColor;
			OnDeathAction();
			Destroy(gameObject);
		}

		protected virtual void OnDeathAction()
		{

		}

		public Rigidbody2D GetRb()
		{
			return rb;
		}
	}
}
