using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public static Vector2 floorPos;
	public static Vector2 middlePos;
	public static Vector2 rightPos, leftPos;

	public GameObject[] floors;
	public GameObject[] middlePlatforms;
	public GameObject[] sidePlatforms;

	private float xMax = 17f;
	private float xMin = -17f;
	private float yMin = -7;
	private float yMax = 4.5f;

	public static MapGenerator instance;

	private List<GameObject> parts;

	// Use this for initialization
	void Start()
	{
		instance = this;
		parts = new List<GameObject>();
		GenerateMap();
	}

	public void GenerateMap()
	{
		//Clean map before generating new
		parts = new List<GameObject>();
		rightPos = new Vector2(5f, 2f);
		leftPos = new Vector2(Screen.height/2, Screen.width/2);
		floorPos = new Vector2(0f, -4.5f);
		middlePos = new Vector2(0f, 0f);

		parts.Add(Instantiate(floors[Random.Range(0, floors.Length)], floorPos, transform.rotation));
		parts.Add(Instantiate(middlePlatforms[Random.Range(0, middlePlatforms.Length)], middlePos, transform.rotation));
		if (Random.Range(0, 1f) > 0.5f)
			parts.Add(Instantiate(sidePlatforms[Random.Range(0, sidePlatforms.Length)], rightPos, transform.rotation));
		if (Random.Range(0, 1f) > 0.5f)
			parts.Add(Instantiate(sidePlatforms[Random.Range(0, sidePlatforms.Length)], leftPos, transform.rotation));
	}

	public void CleanMap()
	{
		for (int i = 0; i < parts.Count; i++)
			Destroy(parts[i]);
	}
}
