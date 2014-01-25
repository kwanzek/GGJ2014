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
	private float acceleration = 100.0f;

	private bool canInput = false;

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
	
		if(canInput)
			{
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
		}



		//This is where the player wants to go
		float forwardX = Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad*rotation) * speed;
		float forwardY = Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad*rotation) * speed;


		//Get bounding box
		Bounds boundingBox = renderer.bounds;
		Vector3 extents = boundingBox.extents;
		Rect boundingRect = new Rect(boundingBox.center.x-extents.x, boundingBox.center.y-extents.y, 
		                             extents.x*2, extents.y*2);


		float tempDistanceX = 0;
		float tempDistanceY = 0;
		float closestDistanceY = Mathf.Infinity;
		float closestDistanceX = Mathf.Infinity;


		foreach(GameObject obj in blockList)
		{
			Bounds tileBoundingBox = obj.renderer.bounds;
			Vector3 tileExtents = tileBoundingBox.extents;
			
			Rect tileBoundingRect = new Rect(tileBoundingBox.center.x-tileExtents.x, 
			                                 tileBoundingBox.center.y-tileExtents.y, tileExtents.x*2, tileExtents.y*2);
			
			//Debug.Log (forwardX);
			float tempDistanceMin_Y = getDistance(0, forwardY, 0, tileBoundingRect.yMin);
			float tempDistanceMax_Y = getDistance(0, forwardY, 0, tileBoundingRect.yMax);
			tempDistanceY = Mathf.Min(tempDistanceMin_Y, tempDistanceMax_Y);

			float tempDistanceMin_X = getDistance(forwardX, 0, tileBoundingRect.xMin, 0);
			float tempDistanceMax_X = getDistance(forwardX, 0, tileBoundingRect.xMax, 0);
			tempDistanceX = Mathf.Min(tempDistanceMin_X, tempDistanceMax_X);

			if(tempDistanceY < closestDistanceY)
			{
				closestDistanceY = tempDistanceY;
				//Debug.Log ("Temp Distance X: " + tempDistanceX + "Temp Distance Y: " + tempDistanceY);
				//closestTile = obj;
				//NEED TO GET THE TILE HERE?
			}
			if(tempDistanceX < closestDistanceX)
			{
				closestDistanceX = tempDistanceX;
				//Debug.Log ("shorterX");
			}
		}


		//forwardX = Mathf.Min(closestDistanceX, forwardX);
		//forwardY = Mathf.Min(closestDistanceY, forwardY);

		float newX = transform.position.x + forwardX; //Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad*rotation) * speed;
		float newY = transform.position.y + forwardY; //Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad*rotation) * speed;

		transform.rotation = Quaternion.AngleAxis(rotation-90, Vector3.forward);

		//Update position
		//transform.position = (new Vector3(transform.position.x + xVelocity,transform.position.y + yVelocity,transform.position.z));
		transform.position = (new Vector3(newX, newY, transform.position.z));
	}

	public void setCanInput(bool val)
	{
		canInput = val;
	}

	float getDistance(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt(Mathf.Pow(x1-x2, 2) + Mathf.Pow(y1-y2, 2));
	}
}
