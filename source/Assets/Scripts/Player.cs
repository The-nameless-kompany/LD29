using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool pause = false;
	private GameManager gameManager;
	private Vector3 position;
	// Use this for initialization
	void Start () {
		gameManager = GameManager.obtener();

	}
	
	// Update is called once per frame
	void Update () {
		if(!pause)
		{
			if(Input.GetKey(KeyCode.UpArrow)&&gameManager.move(0))
			{
				position = transform.localPosition;
				position.y += 1.0f;
				transform.localPosition = position;
			}
			if(Input.GetKey(KeyCode.RightArrow)&& gameManager.move(1))
			{
				position = transform.localPosition;
				position.x += 1.0f;
				transform.localPosition = position;
			}
			if(Input.GetKey(KeyCode.DownArrow) && gameManager.move(2))
			{
				position = transform.localPosition;
				position.y -= 1.0f;
				transform.localPosition = position;
			}
			if(Input.GetKey(KeyCode.LeftArrow) && gameManager.move(3))
			{
				position = transform.localPosition;
				position.x -= 1.0f;
				transform.localPosition = position;
			}
		}

	}



}
