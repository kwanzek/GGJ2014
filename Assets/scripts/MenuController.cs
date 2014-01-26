using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {


	float splashTimer = 5.0f;

	public GameObject splashscreen;
	private SplashScript splash;

	//Sprite[] spriteArray;

	// Use this for initialization
	void Start () {
		splash = splashscreen.GetComponent("SplashScript") as SplashScript;
	}
	
	// Update is called once per frame
	void Update () {
		if(splash.alpha <= 0)
		{
			Debug.Log ("Do something");
			//splashscreen.GetComponent<SpriteRenderer>().sprite = spriteArray[1];
		}
	}
}
