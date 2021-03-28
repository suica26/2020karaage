using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	float angle;
	public float speed;

	void Update()
	{

		angle = angle + speed;

		transform.eulerAngles = new Vector3(0, angle, 0);
	}
}
