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
	public GameObject playerObject; 

	//Size of tilemap
	public int tilemapWidth = 16;
	public int tilemapHeight = 16;

	//Width / Height of tiles in pixels
	public int tileSize = 32;

	public List<GameObject> blockList = new List<GameObject>();

	//Tile map as integers
	//Generated from Tiled
	public int[,] tilemap = new int[,] 
	{
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,7,7,7,7,7,7,7,7,7,7,0,0,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,0,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,1,2,3,0},
		{0,1,2,3,0,0,0,0,0,0,0,0,1,2,3,0},
		{0,1,2,3,0,0,0,0,0,0,0,0,1,2,3,0},
		{0,1,2,3,0,0,0,0,0,0,0,0,1,2,3,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,7,7,7,7,7,7,7,7,7,7,7,7,7,7,0},
		{0,0,7,7,7,7,7,7,7,7,7,7,7,7,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	};

	// Use this for initialization
	void Start () {

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
					tempObj = (GameObject)Instantiate(blockObject, new Vector3(xPosition, yPosition, -1), Quaternion.identity);
					blockList.Add (tempObj);
					tempObj.renderer.material.color = GetColorFromTile(tileType);
				}
				
				xPosition+=tileSize;
				
			}
			yPosition+=tileSize;
		}

		GameObject player1 = (GameObject)Instantiate(playerObject, new Vector3(64, 64, 0), Quaternion.identity);
		player1.renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
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
