using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerController : MonoBehaviour {


	// How far to translate on X axis
	private float xVelocity = 0;
	// How far to translate on Y axis
	private float yVelocity = 0;

	private float xAcceleration = 1.0f;
	private float yAcceleration = 1.0f;

	private float rotation = 90.0f;
	private float angularVelocity = 200.0f;
	private float speed = 0.0f;
	private float maxSpeed = 100.0f;
	private float acceleration = 50.0f;


	// List of tiles / blocks
	List<GameObject> blockList;

	private int blockSize;

	//List of keys for input
	private KeyCode KEY_FORWARD = KeyCode.W;
	private KeyCode KEY_BACKWARD = KeyCode.S;
	private KeyCode KEY_LEFT = KeyCode.A;
	private KeyCode KEY_RIGHT = KeyCode.D;

	// Use this for initialization
	void Start () {
		//Get tilemap script from Master object
		GameObject masterController = GameObject.FindGameObjectWithTag("MasterObject");
		Setup setupScript = masterController.GetComponent("Setup") as Setup;

		//Grab tile list
		blockList = setupScript.blockList;

		//Grab tile size
		blockSize = setupScript.tileSize;

	}

	void FixedUpdate()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KEY_FORWARD))
		{
			speed += (acceleration * Time.deltaTime);
			if(speed > maxSpeed)
				speed = maxSpeed;

		}
		else if(Input.GetKey(KEY_BACKWARD))
		{
			speed -= (acceleration * Time.deltaTime);
			if(speed < -1*maxSpeed)
				speed = -1*maxSpeed;
		}

		if(Input.GetKey (KEY_LEFT))
		{
			rotation+=angularVelocity*Time.deltaTime;
			if(rotation > 0)
			{
				rotation-=360;
			}
		}
		else if (Input.GetKey (KEY_RIGHT))
		{
			rotation-=angularVelocity*Time.deltaTime;
			if(rotation < 0)
			{
				rotation+=360;
			}
		}

		float newX = transform.position.x + Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad*rotation) * speed;
		float newY = transform.position.y + Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad*rotation) * speed;

		transform.rotation = Quaternion.AngleAxis(rotation-90, Vector3.forward);

		//Update position
		//transform.position = (new Vector3(transform.position.x + xVelocity,transform.position.y + yVelocity,transform.position.z));
		transform.position = (new Vector3(newX, newY, transform.position.z));
	}
}
