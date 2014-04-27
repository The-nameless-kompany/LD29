using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool pause = false;
	private bool moving = false;
	public int resources = 1000;
	public int speed = 15;
	private int direction = 0;
	private int yDir = 0;
	private int xDir = 0;
	private int count = 0;
	private GameManager gameManager;
	private Vector3 position;

	/*
	 * Simbology
	 * 
	 * 1: dig
	 */ 
	private int action = 1;
	// Use this for initialization
	void Start () {
		gameManager = GameManager.obtener();

	}
	
	// Update is called once per frame
	void Update () {
		if(!pause)
		{
			if(Input.GetKey(KeyCode.UpArrow))
			{
				if(gameManager.move(0))
				{
					pause = true;
					moving = true;
					yDir = 1;
					count = speed;
				}
				direction = 0;
			}
			if(Input.GetKey(KeyCode.RightArrow))
			{
				if(gameManager.move(1))
				{
					pause = true;
					moving = true;
					xDir = 1;
					count = speed;
				}
				direction = 1;
			}
			if(Input.GetKey(KeyCode.DownArrow))
			{
				if(gameManager.move(2))
				{
					pause = true;
					moving = true;
					yDir = -1;
					count = speed;
				}
				direction = 2;
			}
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				if(gameManager.move(3))
				{
					pause = true;
					moving = true;
					xDir = -1;
					count = speed;
				}
				direction = 3;
			}
		}

		if(moving)
		{
			position = transform.localPosition;
			if(yDir!=0)
			{
				if(yDir==1)
				{
					position.y += 1.0f/speed;
					
				}
				else
				{
					position.y -= 1.0f/speed;
				}
			}
			if(xDir!=0)
			{
				if(xDir==1)
				{
					position.x += 1.0f/speed;
					
				}
				else
				{
					position.x -= 1.0f/speed;
				}
			}
			--count;
			transform.localPosition = position;
			if(count ==0 )
			{
				xDir = 0;
				yDir = 0;
				position.x = gameManager.getXPlayer();
				position.y = gameManager.getYPlayer();
				pause = false;
				moving = false;
			}

		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			switch (action)
			{
			case 1:
				gameManager.dig(direction);
				break;
			}
		}
	}



}
