using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject[,] board;
	private GameObject player;
	private int[,] boardState;
	//private float resolution = 800 / 800;
	private int xPlayer =0;
	private int yPlayer =0;


	public static GameManager obtener() {
		return FindObjectOfType(typeof(GameManager)) as GameManager;
	}

	/*
	 *Simbology 
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
		xPlayer = 1;
		yPlayer = 1;
		boardState = new int[6,6]{{10,10,10,10,10,10},{10,20,20,10,10,10},{10,20,10,10,10,10},{10,10,10,10,10,10},{10,10,10,10,30,10},{10,10,10,10,10,10}};
		fillBoard();
		player = (GameObject)Instantiate((GameObject)Resources.Load("player"));
		player.transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void fillBoard(){
		board = new GameObject[boardState.GetLength(0),boardState.GetLength(1)];
		for(int i=0;i<board.GetLength(0);++i)
		{
			for(int j=0;j<board.GetLength(1);++j)
			{
				switch(boardState[i,j])
				{
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

	/*
	 * Simbology
	 * 0: up
	 * 1: right
	 * 2: down
	 * 3: left
	 */
	public bool move(int place){
		bool result = false;
		switch(place)
		{
		case 0:
			if(canMove(xPlayer,yPlayer-1))
			{
				--yPlayer;
				result = true;
			}
			break;
		case 1:
			if(canMove(xPlayer+1,yPlayer))
			{
				++xPlayer;
				result = true;
			}
			break;
		case 2:
			if(canMove(xPlayer,yPlayer+1))
			{
				++yPlayer;
				result = true;
			}
			break;
		case 3:
			if(canMove(xPlayer-1,yPlayer))
			{
				--xPlayer;
				result = true;
			}
			break;
		}
		return result;
	}

	public bool dig(int direction)
	{
		bool result = false;
		switch(direction)
		{
		case 0:
			if(reachable(xPlayer,yPlayer-1))
			{
				boardState[xPlayer,yPlayer-1] = 20;
				Destroy(board[xPlayer,yPlayer-1]);
				board[xPlayer,yPlayer-1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
				board[xPlayer,yPlayer-1].transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-(yPlayer-1),0.0f);
				result = true;
			}
			break;
		case 1:
			if(reachable(xPlayer+1,yPlayer))
			{
				boardState[xPlayer+1,yPlayer] = 20;
				Destroy(board[xPlayer+1,yPlayer]);
				board[xPlayer+1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
				board[xPlayer+1,yPlayer].transform.localPosition = new Vector3(xPlayer+1-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
				result = true;
			}
			break;
		case 2:
			if(reachable(xPlayer,yPlayer+1))
			{
				boardState[xPlayer,yPlayer+1] = 20;
				Destroy(board[xPlayer,yPlayer+1]);
				board[xPlayer,yPlayer+1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
				board[xPlayer,yPlayer+1].transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-(yPlayer+1),0.0f);
				result = true;
			}
			break;
		case 3:
			if(reachable(xPlayer-1,yPlayer))
			{
				boardState[xPlayer-1,yPlayer] = 20;
				Destroy(board[xPlayer-1,yPlayer]);
				board[xPlayer-1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
				board[xPlayer-1,yPlayer].transform.localPosition = new Vector3(xPlayer-1-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
				result = true;
			}
			break;
		}
		return result;
	}

	public bool repair()
	{
		return ((Structure)board[xPlayer,yPlayer].GetComponent(typeof(Structure))).repair();
	}


	public bool canMove(int x, int y){
		return (x<boardState.GetLength(0) && 0<=x && y<boardState.GetLength(1) && 0<=y && boardState[x,y]<30 && 20<=boardState[x,y]);
	}

	public bool reachable(int x, int y){
		return (x<boardState.GetLength(0) && 0<=x && y<boardState.GetLength(1) && 0<=y && boardState[x,y]<20 && 10<=boardState[x,y]);
	}

	public int getXPlayer(){
		return xPlayer;
	}

	public int getYPlayer(){
		return yPlayer;
	}
}
