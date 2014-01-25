using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class Setup : MonoBehaviour {

	//Values come from using Tiled
	public enum Tiles {
		Impassable = 0,
		Player1 = 1,
		Player2 = 2,
		Player3 = 3,
		Player4 = 4,
		Powerup = 5,
		Neutral = 7
	};

	//All the game object prefabs we are using

	/*
	public GameObject Impassable;
	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public GameObject Player4;
	public GameObject Powerup;
	public GameObject Neutral;*/

	public GameObject blockObject;
	public GameObject player1Block;
	public GameObject player2Block;
	public GameObject player3Block;
	public GameObject player4Block;
	public GameObject playerObject; 

	public GameObject player1;

	//Size of tilemap
	public int tilemapWidth = 16;
	public int tilemapHeight = 16;

	//Width / Height of tiles in pixels
	public int tileSize = 32;

	public List<GameObject> blockList = new List<GameObject>();

	private float timer;

	//Tile map as integers
	public int[,] tilemap = new int[,] 
	{
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,7,7,7,7,7,7,7,7,7,7,0,0,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,0,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,1,2,4,0},
		{0,1,2,3,4,0,0,0,0,0,0,0,1,2,4,0},
		{0,1,2,3,4,0,0,0,0,0,0,0,1,2,4,0},
		{0,1,2,3,4,0,0,0,0,0,0,0,1,2,4,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,0,7,7,7,7,7,7,7,7,7,7,7,7,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	};

	// Use this for initialization
	void Start () {
		timer = 3.0f;
		int yPosition = tileSize/2; //offset for tiles since position is centered

		for (int i = 0; i < tilemap.GetLength(0); ++i)
		{
			int xPosition = tileSize/2; //offset for tiles since position is centered
			for (int j = 0; j < tilemap.GetLength(1); ++j)
			{
				int tileType = tilemap[i,j];
				GameObject tempObj = null;
				if(tileType != (int)Tiles.Neutral)
				{
					//Debug.Log (tileType);
					if(tileType == (int)Tiles.Impassable)
					{
						tempObj = (GameObject)Instantiate(blockObject, new Vector3(xPosition, yPosition, -1), Quaternion.identity);
						tempObj.renderer.material.color = GetColorFromTile(tileType);
					}
					else if (tileType == (int)Tiles.Player1)
					{
						tempObj = (GameObject)Instantiate(player1Block, new Vector3(xPosition, yPosition, -1), Quaternion.identity);
					}
					else if (tileType == (int)Tiles.Player2)
					{
						tempObj = (GameObject)Instantiate(player2Block, new Vector3(xPosition, yPosition, -1), Quaternion.identity);
					}
					else if (tileType == (int)Tiles.Player3)
					{
						tempObj = (GameObject)Instantiate(player3Block, new Vector3(xPosition, yPosition, -1), Quaternion.identity);
					}
					else if (tileType == (int)Tiles.Player4)
					{
						tempObj = (GameObject)Instantiate(player4Block, new Vector3(xPosition, yPosition, -1), Quaternion.identity);
					}
					
					blockList.Add (tempObj);
				}
				
				xPosition+=tileSize;
				
			}
			yPosition+=tileSize;
		}

		player1 = (GameObject)Instantiate(playerObject, new Vector3(128, 128, 0), Quaternion.identity);
		player1.renderer.material.color = Color.red;
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

	//Returns the GameObject corresponding to the given tileType int
	Color GetColorFromTile(int tileType)
	{
		Color blockColor = Color.clear;
		switch(tileType)
		{
		case (int)Tiles.Impassable:
		{
			blockColor = Color.white;
			break;
		}
		case (int)Tiles.Player1:
		{
			blockColor = Color.red;
			break;
			
		}
		case (int)Tiles.Player2:
		{
			blockColor = Color.blue;
			break;
		}
		case (int)Tiles.Player3:
		{
			blockColor = Color.green;
			break;
		}
		case (int)Tiles.Player4:
		{
			blockColor = Color.yellow;
			break;	
		}
		case (int)Tiles.Powerup:
		{
			blockColor = Color.magenta;
			break;
		}
		case (int)Tiles.Neutral:
		{
			blockColor = Color.grey;
			break;
		}
		default:
			blockColor = Color.white;
			break;
		}
		return blockColor;
	}
}
