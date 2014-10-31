using UnityEngine;
using System.Collections;

public class HelpButton2 : MonoBehaviour {

	// Update is called once per frame
	void OnMouseDown () {
		Instantiate((GameObject)Resources.Load("Help menu"));
	}
}
