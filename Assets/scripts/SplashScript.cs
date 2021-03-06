﻿using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class SplashScript : MonoBehaviour {

	float splashTimer = 2.0f;

	public float alpha = 1.0f;

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;

	public AudioClip splashMusic;

	List<Sprite> sprites;

	int index = 0;
	float curFrames = 0;
	int framesPerSecond = 12;
	float timeDisplay;

	bool decrementSplashTimer = false;

	// Use this for initialization
	void Start () {
		timeDisplay = 1.0f/framesPerSecond;

		sprites = new List<Sprite>();
		sprites.Add (sprite1);
		sprites.Add (sprite2);
		sprites.Add (sprite3);
		sprites.Add (sprite4);
		sprites.Add (sprite5);
		sprites.Add (sprite6);
		sprites.Add (sprite7);

		AudioSource.PlayClipAtPoint(splashMusic, Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		curFrames += Time.deltaTime;
		if(curFrames >= timeDisplay)
		{
			curFrames = 0;
			index = (index+1)% 7;
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

			spriteRenderer.sprite = sprites[index];
			
		}

		if(Input.GetKey(KeyCode.C))
		{
			Application.LoadLevel(2);
		}

		if(Input.anyKey)
			decrementSplashTimer = true;
		if(decrementSplashTimer)
		{
			splashTimer-=Time.deltaTime;
			alpha -= (Time.deltaTime/2.0f);
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
			Color color = spriteRenderer.color;
			Color newColor = new Color(color.r, color.g, color.b, alpha);
			spriteRenderer.color = newColor;
		}
		if(splashTimer <= 0.0f)
		{
			alpha = 0.0f;
			Application.LoadLevel(1);
		}

	}
}
