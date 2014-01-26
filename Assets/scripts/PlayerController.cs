﻿using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerController : MonoBehaviour {


	public int playerNumber = 0;

	private float rotation = 180.0f;
	private float angularVelocity = 200.0f;
	private float speed = 0.0f;
	private float maxSpeed = 250.0f;
	private float acceleration = 100.0f;

	float playerScalar = 100.0f;
	float forceScalar = 2000.0f;

	float colorSpeedFactor = 1.5f;
	float colorSlowFactor = 0.5f;

	float maxSpeedDragFactor = 1000f;

	private bool canInput = false;

	// List of tiles / blocks
	List<GameObject> wallColliderList;
	List<GameObject> colorTileCollidableList;
	List<GameObject> otherCollidableList;

	private int blockSize;

	// Use this for initialization
	void Start () {
		//Get tilemap script from Master object
		GameObject masterController = GameObject.FindGameObjectWithTag("MasterObject");
		Setup setupScript = masterController.GetComponent("Setup") as Setup;

		//Grab tile list
		wallColliderList = setupScript.wallCollidableList;

		colorTileCollidableList = setupScript.colorTileCollidableList;

		otherCollidableList = setupScript.otherCollidableList;

		//Grab tile size
		blockSize = setupScript.tileSize;

	}

	void FixedUpdate()
	{

	}
	
	// Update is called once per frame
	void Update () {

		//Get bounding box
		Bounds boundingBox = renderer.bounds;
		Vector3 extents = boundingBox.extents;
		Rect boundingRect = new Rect(boundingBox.center.x-extents.x, boundingBox.center.y-extents.y, 
		                             extents.x*2, extents.y*2);


		float altered_Acceleration = acceleration;
		float maxSpeed_extraFactor = 1.0f;

		foreach(GameObject obj in colorTileCollidableList)
		{
			Bounds tileBoundingBox = obj.renderer.bounds;
			Vector3 tileExtents = tileBoundingBox.extents;
			
			Rect tileBoundingRect = new Rect(tileBoundingBox.center.x-tileExtents.x, 
			                                 tileBoundingBox.center.y-tileExtents.y, tileExtents.x*2, tileExtents.y*2);
			
			bool isIntersecting = doesIntersect(boundingRect, tileBoundingRect);

			if(isIntersecting)
			{
				BlockScript blockScript = obj.GetComponent("BlockScript") as BlockScript;
				if(blockScript.colorNumber == playerNumber)
				{
					maxSpeed_extraFactor = colorSpeedFactor;
				}
				else
				{
					maxSpeed_extraFactor = colorSlowFactor;
				}
				//Debug.Log (maxSpeed_extraFactor);
			}

		}
		if(canInput)
		{
			float xAxis = Input.GetAxis ("L_XAxis_"+playerNumber);
			float yAxis = Input.GetAxis ("L_YAxis_"+playerNumber);


			float tempFactor = 1.0f;
			//if(maxSpeed_extraFactor == colorSpeedFactor)

			if(maxSpeed_extraFactor == colorSpeedFactor)
			{
				tempFactor = 2.0f;
			}
			speed += ((acceleration * Time.deltaTime) * yAxis * -1*tempFactor);

			rotation += angularVelocity * Time.deltaTime * xAxis * -1;
			if(rotation < 0)
				rotation +=360;
			else if(rotation > 360)
				rotation -= 360;


			/*
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
			}*/
		}

		if(Mathf.Abs(speed) > maxSpeed * maxSpeed_extraFactor)
		{
			speed -= maxSpeedDragFactor * Time.deltaTime;
		}

		//This is where the player wants to go
		float forwardX = Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad*rotation) * speed;
		float forwardY = Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad*rotation) * speed;

		//COLLISION DETECTION!

		Vector2 forceVector = new Vector2(0,0);

		foreach(GameObject obj in wallColliderList)
		{
			Bounds tileBoundingBox = obj.renderer.bounds;
			Vector3 tileExtents = tileBoundingBox.extents;
			
			Rect tileBoundingRect = new Rect(tileBoundingBox.center.x-tileExtents.x, 
			                                 tileBoundingBox.center.y-tileExtents.y, tileExtents.x*2, tileExtents.y*2);

			bool isIntersecting = doesIntersect(boundingRect, tileBoundingRect);
			if(isIntersecting)
			{
				forceVector = applyForce(tileBoundingRect.center.x,tileBoundingRect.center.y
				           ,boundingRect.center.x,boundingRect.center.y, forceVector);
			}
		}


		//forwardX = Mathf.Min(closestDistanceX, forwardX);
		//forwardY = Mathf.Min(closestDistanceY, forwardY);


		//END COLLISION DETECTION

		float newX = transform.position.x + forceVector.x+forwardX;
		float newY = transform.position.y + forceVector.y+forwardY;

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

	Vector2 applyForce(float x1, float y1, float playerX, float playerY, Vector2 currentForceVector)
	{
		float deltaX = playerX-x1;
		float deltaY = playerY-y1;

		float distance = getDistance(x1,y1, playerX, playerY);

		float repulsion = forceScalar/Mathf.Pow(distance, 2);
		Vector2 unitVector = new Vector2(deltaX, deltaY).normalized;

		unitVector.Scale(new Vector2(repulsion, repulsion));
		Vector2 forceVector = unitVector;
		currentForceVector = new Vector2(currentForceVector.x+forceVector.x, currentForceVector.y + forceVector.y);

		return currentForceVector;
	}

	bool doesIntersect(Rect player, Rect obj)
	{
		Vector2 playerTopLeft = new Vector2(player.center.x-player.width/2, player.center.y+player.height/2);
		Vector2 playerBottomRight = new Vector2(player.center.x+player.width/2, player.center.y-player.height/2);

		Vector2 objectTopLeft = new Vector2(obj.center.x-obj.width/2, obj.center.y+obj.height/2);
		Vector2 objectBottomRight = new Vector2(obj.center.x+obj.width/2, obj.center.y-obj.height/2);



		return (playerTopLeft.x < objectBottomRight.x && playerTopLeft.y > objectBottomRight.y) && 
			(playerBottomRight.x > objectTopLeft.x && playerBottomRight.y < objectTopLeft.y);
	}

}
