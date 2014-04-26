using UnityEngine;
using System.Collections;

public class BotonIniciar : MonoBehaviour {

	public string nivel = "Juego";

	void OnMouseDown(){
		Application.LoadLevel(nivel);
	}
}
