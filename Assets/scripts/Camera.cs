using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(256, 256, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
