using UnityEngine;
using System.Collections;

public class LapCounter : MonoBehaviour {

	public GameObject attachedPlayer;
	public GUIText lapText;

	int laps = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerController playerController = attachedPlayer.GetComponent("PlayerController") as PlayerController;
		laps = playerController.lapsCompleted+1;


/*
		lapText.transform.position = new Vector3(0.5f,0.5f,0f);//transform.position;
		lapText.text = "ALKSjALSJAKLSHLJHSA";//laps.ToString();

		
		
		Vector3 v3ScreenPos = Camera.main.WorldToScreenPoint(lapText.gameObject.transform.position);
		lapText.gameObject.transform.position = v3ScreenPos;

		Debug.Log(lapText.transform.position);*/
	}

	void OnGUI()
	{
		Vector3 pos = transform.position;
		//Debug.Log(pos);
		pos = Camera.main.WorldToViewportPoint(pos);
		//Debug.Log ("New: " + pos);
		//pos.y *= (Camera.main.orthographicSize*2 / 1280);
		//Vector3 relativePosition = Camera.main.transform.InverseTransformDirection(transform.position - Camera.main.transform.position);


		//Rect rect = new Rect((relativePosition.x / (2 * Camera.main.orthographicSize) * 1280.0f)
		 //                    , (relativePosition.y / (2 * Camera.main.orthographicSize) * 720.0f)
		  //                   , 60
		   //                  , 60);


		Rect rect = new Rect(pos.x*1280+1280/2, pos.y*720+720/2, 60,60);
		GUI.Label(rect, "ASKLJASLKJAS");


		/*
		Vector3 pos = transform.position;
		pos.y += 2; 
		pos = Camera.main.WorldToScreenPoint(pos);
		
		
		Rect rect = new Rect(pos.x - 10
		                     , Screen.height - pos.y - 15
		                     , 100
		                     , 22);
		
		GUI.Label(rect, nameScript.getName ());*/



		//GUI.Label (rect, laps.ToString());
	}
}
