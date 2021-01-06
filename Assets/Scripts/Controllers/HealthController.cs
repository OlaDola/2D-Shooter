using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthController : MonoBehaviour
{

	public RectTransform greenHealthBar;

	private float maxScale;

	private Vector2 vel = Vector2.zero;
	private float smoothTime = 0.3f;

	// Use this for initialization
	void Start()
	{
		maxScale = greenHealthBar.localScale.x;
		greenHealthBar.localScale = new Vector2(0, greenHealthBar.localScale.y);
	}

	void Update()
	{
		//if (!Game.inGame) return;
		float newScale;

		if (Game.currentPlayer == null) newScale = 0;
		else newScale = Game.currentPlayerScript.Health / Game.currentPlayerScript.startHealth;
		if (newScale < 0) newScale = 0;
		Vector2 desiredScale = new Vector2(newScale, 1);
		greenHealthBar.localScale = Vector2.SmoothDamp(greenHealthBar.localScale, desiredScale, ref vel, smoothTime);
	}
}
