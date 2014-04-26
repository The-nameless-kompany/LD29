using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public string level = "Game";

	void OnMouseDown(){
		Application.LoadLevel(level);
	}
}
