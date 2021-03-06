﻿using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	private Player player;
	public string pauseKey = "p";


	public static Pause get() {
		return FindObjectOfType(typeof(Pause)) as Pause;
	}

	public void setPlayer(Player player)
	{
		this.player = player;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(pauseKey))
		{
			player.pause = false;
			gameObject.SetActive(false);
		}
	}
}
