using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movebackground : MonoBehaviour
{
	
	public float speed = 4.0f;

	public float px;
	public float py;
	public float pz;

	public doorscore dHP;

	public float spdown;
	public shaker eq;

	// Update is called once per frame
	void Update()
	{
		
		transform.position += transform.right * speed * Time.deltaTime;

		if (dHP.nowanim >= 5 && speed >= 0)
        {

			speed -= spdown;
			eq.duration -= spdown;

        }
		
		
	}
	void OnTriggerEnter(Collider collision)
	{

		if (collision.gameObject.name == "backgroundhiter")
		{

			transform.position = new Vector3(px, py, pz);
		}
	}
}