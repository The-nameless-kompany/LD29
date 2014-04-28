using UnityEngine;
using System.Collections;

public class Next : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown(){
		GameManager.obtener().nextMap();
	}
}
