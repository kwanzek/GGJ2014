using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class Setup : MonoBehaviour {

	//Values come from using Tiled

	public enum Walls
	{
		UpDown = 6,
		LeftRight = 7,
		TopLeft = 8,
		BottomRight = 9,
		BottomLeft = 10,
		TopRight = 11,
	}

	public enum Tiles {
		Empty = 0,
		Checkered = 12,
		Start = 13
	};

	public enum ColorTiles{
		Player1 = 1,
		Player2 = 2,
		Player3 = 3,
		UnusedPlayer=4,
		Player4 = 5,
	}

	//All the game object prefabs we are using

	//public GameObject blockObject;
	public GameObject player1Block;
	public GameObject player2Block;
	public GameObject player3Block;
	public GameObject unusedPlayer;
	public GameObject player4Block;
	public GameObject upDown;
	public GameObject leftRight;
	public GameObject topLeft;
	public GameObject bottomRight;
	public GameObject bottomLeft;
	public GameObject topRight;
	public GameObject checkered;
	public GameObject start;
	
	public GameObject player1Object;
	public GameObject player2Object;
	public GameObject player3Object;
	public GameObject player4Object;

	[HideInInspector]
	public GameObject player1;	
	[HideInInspector]
	public GameObject player2;
	[HideInInspector]
	public GameObject player3;
	[HideInInspector]
	public GameObject player4;

	public List<Vector2> startLocations;

	//Size of tilemap
	[HideInInspector]
	public int tilemapWidth = 32;
	[HideInInspector]
	public int tilemapHeight = 32;

	//Width / Height of tiles in pixels
	[HideInInspector]
	public int tileSize = 32;

	public List<GameObject> wallCollidableList = new List<GameObject>();
	public List<GameObject> colorTileCollidableList = new List<GameObject>();
	public List<GameObject> otherCollidableList = new List<GameObject>();
	public List<GameObject> playerList = new List<GameObject>();

	private float timer;
	
	public int[,] tilemap = new int[,]
	{
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,9,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,10,0,0},
		{0,0,9,8,3,3,3,0,0,0,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,5,5,5,11,10,0},
		{0,9,8,3,3,3,3,0,0,0,0,0,0,0,0,0,12,13,0,0,0,0,0,0,0,0,5,5,5,5,11,10},
		{0,6,3,3,3,0,0,0,0,0,0,0,0,0,0,0,12,0,13,0,0,0,0,0,0,0,0,0,5,5,5,6},
		{0,6,3,3,0,0,0,0,0,0,0,0,0,0,0,0,12,0,0,13,0,0,0,0,0,0,0,0,0,5,5,6},
		{0,6,3,3,0,0,0,0,0,0,0,0,0,0,0,0,12,0,0,0,13,0,0,0,0,0,0,0,0,5,5,6},
		{0,6,0,0,0,0,0,0,1,1,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,2,2,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,1,9,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,10,2,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,5,11,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,8,3,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,5,5,0,2,0,0,0,0,0,0,0,3,0,0,0,0,0,3,3,0,0,0,0,6},
		{0,6,2,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,1,1,6},
		{0,6,2,2,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,1,1,6},
		{0,6,2,2,2,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,6},
		{0,11,10,2,2,2,2,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,1,0,0,1,1,1,1,9,8},
		{0,0,11,10,2,2,2,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,1,0,0,1,1,1,9,8,0},
		{0,0,0,11,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,8,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	};


	// Use this for initialization
	void Start () {

		timer = 0.5f;
		int yPosition = tileSize/2; //offset for tiles since position is centered

		startLocations = new List<Vector2>();

		GameObject[] objectIndices = {null, player1Block, player2Block, player3Block, unusedPlayer,
			player4Block, upDown, leftRight, topLeft, bottomRight, bottomLeft, topRight, checkered, start};

		for (int i = tilemap.GetLength (0)-1; i >= 0; --i)
		{
			int xPosition = tileSize/2; //offset for tiles since position is centered
			for (int j = 0; j < tilemap.GetLength(1); ++j)
			{
				int tileType = tilemap[i,j];
				GameObject tempObj = null;
				if(tileType != (int)Tiles.Empty)
				{
					tempObj = (GameObject)Instantiate(objectIndices[tileType], 
					                                  new Vector3(xPosition, yPosition, -1), Quaternion.identity);

					if(System.Enum.IsDefined(typeof(Walls), tileType))
					{
						wallCollidableList.Add (tempObj);
					}
					else if(System.Enum.IsDefined(typeof(ColorTiles), tileType))
					{
						colorTileCollidableList.Add(tempObj);
					}
					else if(System.Enum.IsDefined(typeof(Tiles), tileType) && tileType != (int)Tiles.Start)
					{
						otherCollidableList.Add (tempObj);
					}
					else if(tileType == (int)Tiles.Start)
					{
						startLocations.Add(new Vector2(xPosition, yPosition));
					}
				}
				
				xPosition+=tileSize;
				
			}
			yPosition+=tileSize;
		}

		//Set up the players

		int randomInt = Random.Range(0, startLocations.Count);
		Vector2 tempVector = startLocations[randomInt];

		player1 = (GameObject)Instantiate(player1Object, new Vector3(tempVector.x, 
		                                                            tempVector.y, 0), Quaternion.identity);
		PlayerController playerController = player1.GetComponent("PlayerController") as PlayerController;
		playerController.playerNumber = 1;
		playerList.Add(player1);

		startLocations.Remove(tempVector);



		randomInt = Random.Range(0, startLocations.Count);
		tempVector = startLocations[randomInt];
		
		player2 = (GameObject)Instantiate(player2Object, new Vector3(tempVector.x, 
		                                                            tempVector.y, 0), Quaternion.identity);
		PlayerController playerController2 = player2.GetComponent("PlayerController") as PlayerController;
		playerController2.playerNumber = 2;
		playerList.Add(player2);
		
		startLocations.Remove(tempVector);




		randomInt = Random.Range(0, startLocations.Count);
		tempVector = startLocations[randomInt];
		
		player3 = (GameObject)Instantiate(player3Object, new Vector3(tempVector.x, 
		                                                            tempVector.y, 0), Quaternion.identity);
		PlayerController playerController3 = player3.GetComponent("PlayerController") as PlayerController;
		playerController3.playerNumber = 3;
		playerList.Add(player3);
		
		startLocations.Remove(tempVector);






		randomInt = Random.Range(0, startLocations.Count);
		tempVector = startLocations[randomInt];
		
		player4 = (GameObject)Instantiate(player4Object, new Vector3(tempVector.x, 
		                                                            tempVector.y, 0), Quaternion.identity);
		PlayerController playerController4 = player4.GetComponent("PlayerController") as PlayerController;
		playerController4.playerNumber = 4;
		playerList.Add(player4);

		startLocations.Remove(tempVector);

		CustomCamera cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CustomCamera") as CustomCamera;
		if(player1 != null)
			cameraController.player1 = player1.transform;
		if(player2 != null)
			cameraController.player2 = player2.transform;
		if(player3 != null)
			cameraController.player3 = player3.transform;
		if(player4 != null)
			cameraController.player4 = player4.transform;
		cameraController.updateArray();


	}
	
	// Update is called once per frame
	void Update () {

		if(timer > 0)
			timer -= Time.deltaTime;
		if(timer < 0)
		{
			PlayerController playerController = player1.GetComponent("PlayerController") as PlayerController;
			playerController.setCanInput(true);

			PlayerController playerController2 = player2.GetComponent("PlayerController") as PlayerController;
			playerController2.setCanInput(true);

			PlayerController playerController3 = player3.GetComponent("PlayerController") as PlayerController;
			playerController3.setCanInput(true);

			PlayerController playerController4 = player4.GetComponent("PlayerController") as PlayerController;
			playerController4.setCanInput(true);


			timer = 0;
		}
	
	}
}
