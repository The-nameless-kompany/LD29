using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {
	private GameObject pauseMenu;
	
	void Start()
	{
		pauseMenu = GameObject.Find("Pause menu(Clone)");
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		Instantiate((GameObject)Resources.Load("Help menu"));
		(FindObjectOfType(typeof(Player)) as Player).pause = false;
		Destroy(pauseMenu);
	}
}
