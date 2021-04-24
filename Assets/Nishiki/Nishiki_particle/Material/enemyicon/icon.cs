using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class icon : MonoBehaviour
{
	void Update()
	{
		Vector3 p = Camera.main.transform.position;
		p.y = transform.position.y;
		transform.LookAt(p);

		transform.Rotate(new Vector3(0, 0, 180));
	}
}