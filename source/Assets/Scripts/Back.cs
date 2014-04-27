using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {

	private GameObject pauseMenu;

	void Start()
	{
		pauseMenu = GameObject.Find("Game/Pause menu");
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		(FindObjectOfType(typeof(Player)) as Player).pause = false;
		active = false;
		pauseMenu.SetActive(false);
	}
}
