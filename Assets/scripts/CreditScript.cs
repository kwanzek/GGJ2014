using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {

	public AudioClip creditMusic;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(creditMusic, Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			Application.LoadLevel (0);
		}

	}
}
