using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
	public SpriteRenderer sr;
	// Update is called once per frame
	void Update()
	{
		float z = transform.rotation.eulerAngles.z;
		if (z > 0 && z < 180 && !sr.flipX)
			sr.flipX = true;
		else if (z < 360 && z > 180 && sr.flipX)
			sr.flipX = false;
	}
}
