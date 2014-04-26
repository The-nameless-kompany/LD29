using UnityEngine;
using System.Collections;

public class ManejadorJuego : MonoBehaviour {
	private GameObject[,] tablero;
	private float originalAspecto = 800 / 600;

	// Use this for initialization
	void Start () {
		Camera.main.aspect = originalAspecto;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
