using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class RaceMaster : MonoBehaviour {

	int total_laps = 2;

	Vector2 referencePoint = new Vector2(420, 495);

	float[] playerReferenceAngles = {0f,0f,0f,0f};
	float[] playerAccumulatedAngles = {0f,0f,0f,0f};
	
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;


	public AudioClip redWins;
	public AudioClip blueWins;
	public AudioClip greenWins;
	public AudioClip yellowWins;

	private bool isWinner = false;

	public List<GameObject> players;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		for(int i = 0; i < 4; ++i)
		{
			GameObject curPlayer = players[i];
			if(curPlayer != null)
			{
				float differenceY = curPlayer.transform.position.y - referencePoint.y;
				float differenceX = curPlayer.transform.position.x - referencePoint.x;
				float currentAngle = Mathf.Atan2(differenceY, differenceX)*Mathf.Rad2Deg;

				PlayerController playerController = curPlayer.GetComponent("PlayerController") as PlayerController;

				float deltaAngle = currentAngle - playerReferenceAngles[i];


				playerReferenceAngles[i] = currentAngle;


				if(deltaAngle > 180)
					deltaAngle= -1*deltaAngle + 360;
				else if(deltaAngle  <= -180)
				{
					deltaAngle+=360;
				}
				playerAccumulatedAngles[i] += deltaAngle;
				if(playerAccumulatedAngles[i] >= 340)
				{
					playerController.canFinishLap = true;
					playerAccumulatedAngles[i] = 0;
				}
				if(playerController.lapsCompleted == total_laps && !isWinner)
				{
					isWinner = true;
					if(playerController.playerNumber == 1)
					{
						AudioSource.PlayClipAtPoint(blueWins, Camera.main.transform.position);
						isWinner = true;
					}
					else if(playerController.playerNumber == 2)
					{
						AudioSource.PlayClipAtPoint(greenWins, Camera.main.transform.position);
						isWinner = true;
					}
					else if(playerController.playerNumber == 3)
					{
						AudioSource.PlayClipAtPoint(yellowWins, Camera.main.transform.position);
						isWinner = true;
					}
					else if(playerController.playerNumber == 4)
					{
						AudioSource.PlayClipAtPoint(redWins, Camera.main.transform.position);
						isWinner = true;
					}

				}
			}
		}
	}

	public void setInitialAngles()
	{
		players = new List<GameObject>();
		players.Add(player1);
		players.Add(player2);
		players.Add(player3);
		players.Add(player4);
		for(int i = 0; i < 4; ++i)
		{
			GameObject curPlayer = players[i];
			if(curPlayer!= null)
			{
				float differenceY = curPlayer.transform.position.y - referencePoint.y;
				float differenceX = curPlayer.transform.position.x - referencePoint.x;
				float angle = Mathf.Atan2(differenceY, differenceX)*Mathf.Rad2Deg;

				playerReferenceAngles[i] = angle;

			}
		}
	}



}
