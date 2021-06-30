using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movebackground : MonoBehaviour
{
	
	public float speed = 4.0f;

	public float px;
	public float py;
	public float pz;

	// Update is called once per frame
	void Update()
	{
		
		transform.position += transform.right * speed * Time.deltaTime;
		
		
	}
	void OnTriggerEnter(Collider collision)
	{

		if (collision.gameObject.name == "backgroundhiter")
		{

			transform.position = new Vector3(px, py, pz);
		}
	}
}