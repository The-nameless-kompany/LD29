using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {

	private GameObject pauseMenu;

	void Start()
	{
		pauseMenu = GameObject.Find("Pause menu(Clone)");
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		(FindObjectOfType(typeof(Player)) as Player).pause = false;
		Destroy(pauseMenu);
	}
}
