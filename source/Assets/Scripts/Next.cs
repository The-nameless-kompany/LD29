using UnityEngine;
using System.Collections;

public class Next : MonoBehaviour {

	GameObject winMenu;
	void Start()
	{

	}

	void OnMouseDown(){
		winMenu = GameObject.Find("Win menu(Clone)");
		GameManager.obtener().nextMap();
		Destroy(winMenu);
	}
}
