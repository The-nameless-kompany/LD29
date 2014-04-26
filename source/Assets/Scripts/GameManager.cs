using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject[,] board;
	private int[,] boardState;
	private float resolution = 800 / 800;

	/*
	 *Simbology 
	 * 0: player
	 * 10: ground
	 * 11: stone
	 * 
	 * 20: build
	 * 
	 * 30: gold
	 */
	// Use this for initialization
	void Start () {
		//Camera.main.aspect = resolution;
		boardState = new int[6,6]{{10,10,10,10,10,10},{10,0,10,10,10,10},{10,10,10,10,10,10},{10,10,10,10,10,10},{10,10,10,10,30,10},{10,10,10,10,10,10}};
		fillBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void fillBoard(){
		board = new GameObject[boardState.GetLength(0),boardState.GetLength(1)];
		for(int i=0;i<board.GetLength(0);++i)
		{
			for(int j=0;j<board.GetLength(1);++j)
			{
				switch(boardState[i,j])
				{
				case 0:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/player"));
					break;

				case 10:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
					break;
				case 11:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/stone"));
					break;

				case 20:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
					break;
				
				case 30:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/gold"));
					break;
				}
				board[i,j].transform.localPosition = new Vector3(i-board.GetLength(0)/2,board.GetLength(1)/2-j,0.0f);

			}
		}
	}
}
