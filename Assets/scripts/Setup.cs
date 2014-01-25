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
		Player1 = 1,
		Player2 = 2,
		Player3 = 3,
		UnusedPlayer=4,
		Player4 = 5,
		Checkered = 12,
		Start = 13
	};

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
	
	public GameObject playerObject; 

	[HideInInspector]
	public GameObject player1;

	//Size of tilemap
	[HideInInspector]
	public int tilemapWidth = 32;
	[HideInInspector]
	public int tilemapHeight = 32;

	//Width / Height of tiles in pixels
	[HideInInspector]
	public int tileSize = 32;

	public List<GameObject> wallCollidableList = new List<GameObject>();
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
		{0,6,0,0,0,0,0,0,1,1,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,4,4,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,1,9,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,10,4,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,5,11,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,8,3,0,0,0,0,6},
		{0,6,0,0,0,0,0,0,5,5,0,4,0,0,0,0,0,0,0,3,0,0,0,0,0,3,3,0,0,0,0,6},
		{0,6,4,4,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,1,1,6},
		{0,6,4,4,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,1,1,6},
		{0,6,4,4,4,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,6},
		{0,11,10,4,4,4,4,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,1,0,0,1,1,1,1,9,8},
		{0,0,11,10,4,4,4,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,1,0,0,1,1,1,9,8,0},
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

		GameObject[] objectIndices = {null, player1Block, player2Block, player3Block, unusedPlayer,
			player4Block, upDown, leftRight, topLeft, bottomRight, bottomLeft, topRight, checkered, start};

		//GameObject[] startLocations = new GameObject[4];

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
					else if(System.Enum.IsDefined(typeof(Tiles), tileType))
					{
						otherCollidableList.Add(tempObj);
					}
				}
				
				xPosition+=tileSize;
				
			}
			yPosition+=tileSize;
		}

		//Set up the players

		player1 = (GameObject)Instantiate(playerObject, new Vector3(300, 350, 0), Quaternion.identity);
		PlayerController playerController = player1.GetComponent("PlayerController") as PlayerController;
		playerController.playerNumber = 1;
		playerList.Add(player1);

	}
	
	// Update is called once per frame
	void Update () {

		if(timer > 0)
			timer -= Time.deltaTime;
		if(timer < 0)
		{
			PlayerController playerController = player1.GetComponent("PlayerController") as PlayerController;
			playerController.setCanInput(true);
			timer = 0;
		}
	
	}
}
