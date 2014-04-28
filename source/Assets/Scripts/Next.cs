using UnityEngine;
using System.Collections;

public class Next : MonoBehaviour {

	GameObject winMenu;
	void Start()
	{
		winMenu = GameObject.Find("Win menu(Clone)");
	}

	void OnMouseDown(){
		GameManager.obtener().nextMap();
		Destroy(winMenu);
	}
}
