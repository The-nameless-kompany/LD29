using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public string level = "Game";

	void OnMouseDown(){
		Application.LoadLevel(level);
	}
}
