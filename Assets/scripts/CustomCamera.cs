using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class CustomCamera : MonoBehaviour {

	[HideInInspector]
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	[HideInInspector]
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	[HideInInspector]
	public float minCameraSize = 128;
	[HideInInspector]
	public float maxCameraSize = 512;

	[HideInInspector]
	public float smooth_value = 0.5f;

	public Transform player1;
	public Transform player2;
	public Transform player3;
	public Transform player4;

	private int size_offset = 32;

	public List<Transform> playerLocations;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(256, 256, transform.position.z);

	}

	void FixedUpdate()
	{
	}

	// Update is called once per frame
	void Update () {
		if(playerLocations != null)
		{
			if(playerLocations.Count > 0)
			{
				/*
				float minX = Mathf.Infinity;
				float minY = Mathf.Infinity;
				float maxX = -1;
				float maxY = -1;
				
				foreach(Transform player in playerLocations)
				{
					if(player.position.x < minX)
						minX = player.position.x;
					else if(player.position.x > maxX)
						maxX = player.position.x;
					if(player.position.y < minY)
						minY = player.position.y;
					else if (player.position.y > maxY)
						maxY = player.position.y;
				}

				maxX += size_offset;
				maxY += size_offset;*/

				Vector2 centroid = new Vector2(0,0);

				foreach(Transform player in playerLocations)
				{
					centroid += new Vector2(player.position.x, player.position.y);
				}

				centroid.Set((centroid.x+size_offset)/4, (centroid.y+size_offset)/4);

				float maxDistance = 0;
				foreach(Transform player in playerLocations)
				{
					float tempDistance = getDistance(player.position.x, player.position.y,
					                                 centroid.x, centroid.y);
					if(tempDistance > maxDistance)
						maxDistance = tempDistance;
				}

				float distance_percent = (maxDistance / 720.0f) * 1280.0f;

				if(distance_percent < minCameraSize)
					distance_percent = minCameraSize;
				if(distance_percent > maxCameraSize)
					distance_percent = maxCameraSize;

				float current_size = Camera.main.orthographicSize;

				distance_percent = Mathf.Lerp(current_size, distance_percent, smooth_value);

				Camera.main.orthographicSize = distance_percent;//Mathf.Clamp(distance_percent, minCameraSize, maxCameraSize);

				//Debug.Log (distance_percent);

				float targetX = Mathf.Lerp(transform.position.x, centroid.x, smooth_value * Time.deltaTime);
				float targetY = Mathf.Lerp(transform.position.y, centroid.y, smooth_value * Time.deltaTime);

				targetX = Mathf.Clamp(centroid.x, minXAndY.x, maxXAndY.x);
				targetY = Mathf.Clamp(centroid.y, minXAndY.y, maxXAndY.y);


				
				
				// Set the camera's position to the target position with the same z component.
				transform.position = new Vector3(targetX, targetY, transform.position.z);

				//want to be in middle of players and have size be +factor on either side



			}
		}
	}

	public void updateArray()
	{
		playerLocations = new List<Transform>();
		if(player1 != null)
			playerLocations.Add(player1);
		if(player2 != null)
			playerLocations.Add(player2);
		if(player3 != null)
			playerLocations.Add(player3);
		if(player4 != null)
			playerLocations.Add(player4);
	}

	float getDistance(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt(Mathf.Pow(x1-x2, 2) + Mathf.Pow(y1-y2, 2));
	}
}
