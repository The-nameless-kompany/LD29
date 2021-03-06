﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private GameObject[,] board;
	private GameObject player;
	private int[,] boardState;
	private int currentMap=0;
	private int[] resources = new int[2]{1290,960};
	private int[,,] maps = new int[2,6,6]{
		{   {10,10,10,10,10,10},
			{10,20,10,10,10,10},
			{10,11,10,11,10,10},
			{10,11,10,11,10,10},
			{10,10,10,11,11,11},
			{10,11,10,10,10,30}
		},
		{
		{20,10,20,10,20,10},
		{10,20,10,20,11,10},
		{20,10,20,11,11,10},
		{10,20,11,11,10,10},
		{20,11,11,10,10,11},
		{10,10,10,11,11,30}}

	};

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
	 * 
	 * 20: build
	 * 21: ladder
	 * 
	 * 30: gold
	 * 
	 * 40: dinamite
	 * 
	 * 50: invencible
	 */
	// Use this for initialization
	void Start () {
		//Camera.main.aspect = resolution;
		xPlayer = 1;
		yPlayer = 1;
		boardState = new int[6,6]{{10,10,10,10,10,10},{10,20,20,10,50,10},{10,10,10,11,10,10},{10,10,10,11,10,10},{10,10,10,10,30,10},{10,10,10,10,10,10}};

		fillBoard();
		player = (GameObject)Instantiate((GameObject)Resources.Load("player"));
		player.transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void fillBoard(){
		board = new GameObject[boardState.GetLength(0),boardState.GetLength(1)];
		for(int i=0;i<board.GetLength(0);++i){
			for(int j=0;j<board.GetLength(1);++j){
				switch(boardState[i,j]){
				case 10:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
					break;
				case 11:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/stone"));
					break;

				case 20:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
					break;
				case 21:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
					break;

				case 30:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/gold"));
					break;
					
				case 40:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/dinamite"));
					break;
				case 50:
					board[i,j] = (GameObject)Instantiate((GameObject)Resources.Load("Board/invencible"));
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
		switch(place){
		case 0:
			if(canMove(xPlayer,yPlayer-1)){
				--yPlayer;
				result = true;
			}
			break;
		case 1:
			if(canMove(xPlayer+1,yPlayer)){
				++xPlayer;
				result = true;
			}
			break;
		case 2:
			if(canMove(xPlayer,yPlayer+1)){
				++yPlayer;
				result = true;
			}
			break;
		case 3:
			if(canMove(xPlayer-1,yPlayer)){
				--xPlayer;
				result = true;
			}
			break;
		}
		if(win())
		{
			((Player)player.GetComponent(typeof(Player))).pause = true;
			GameObject tmp = GameObject.Find("Win menu(Clone)");
			if(tmp == null){
				Instantiate((GameObject)Resources.Load("Win menu"));
			}
		}
		return result;
	}

	public bool dig(int direction){
		bool result = false;
		switch(direction){
		case 0:
			if(reachable(xPlayer,yPlayer-1)){
				ladder(xPlayer,yPlayer-1);
				boardState[xPlayer,yPlayer-1] = 21;
				Destroy(board[xPlayer,yPlayer-1]);
				board[xPlayer,yPlayer-1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
				board[xPlayer,yPlayer-1].transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-(yPlayer-1),0.0f);
				result = true;
			}
			break;
		case 1:
			if(reachable(xPlayer+1,yPlayer)){
				Destroy(board[xPlayer+1,yPlayer]);
				if(!ladder(xPlayer+1,yPlayer)){
					boardState[xPlayer+1,yPlayer] = 20;
					board[xPlayer+1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
				}
				else{
					boardState[xPlayer+1,yPlayer] = 21;
					board[xPlayer+1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
				}
				board[xPlayer+1,yPlayer].transform.localPosition = new Vector3(xPlayer+1-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
				result = true;
			}
			break;
		case 2:
			if(reachable(xPlayer,yPlayer+1)){
				ladder(xPlayer,yPlayer+1);
				boardState[xPlayer,yPlayer+1] = 21;
				Destroy(board[xPlayer,yPlayer+1]);
				board[xPlayer,yPlayer+1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
				board[xPlayer,yPlayer+1].transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-(yPlayer+1),0.0f);
				result = true;
			}
			break;
		case 3:
			if(reachable(xPlayer-1,yPlayer)){
				Destroy(board[xPlayer-1,yPlayer]);
				if(!ladder(xPlayer-1,yPlayer)){
					boardState[xPlayer-1,yPlayer] = 20;
					board[xPlayer-1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/build"));
				}
				else{
					boardState[xPlayer-1,yPlayer] = 21;
					board[xPlayer-1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
				}
				board[xPlayer-1,yPlayer].transform.localPosition = new Vector3(xPlayer-1-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
				result = true;
			}
			break;
		}
		return result;
	}

	public bool dinamite(int direction){
		bool result = false;
		switch(direction){
		case 0:
			if(canDinamite(xPlayer,yPlayer-1)){
				boardState[xPlayer,yPlayer-1] = 40;
				Destroy(board[xPlayer,yPlayer-1]);
				board[xPlayer,yPlayer-1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/dinamite"));
				board[xPlayer,yPlayer-1].transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-(yPlayer-1),0.0f);
				result = true;
				((Dinamite)board[xPlayer,yPlayer-1].GetComponent(typeof(Dinamite))).setCoordenates(xPlayer,yPlayer-1);
			}
			break;
		case 1:
			if(canDinamite(xPlayer+1,yPlayer)){
				boardState[xPlayer+1,yPlayer] = 40;
				Destroy(board[xPlayer+1,yPlayer]);
				board[xPlayer+1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/dinamite"));
				board[xPlayer+1,yPlayer].transform.localPosition = new Vector3(xPlayer+1-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
				((Dinamite)board[xPlayer+1,yPlayer].GetComponent(typeof(Dinamite))).setCoordenates(xPlayer+1,yPlayer);
				result = true;
			}
			break;
		case 2:
			if(canDinamite(xPlayer,yPlayer+1)){
				boardState[xPlayer,yPlayer+1] = 40;
				Destroy(board[xPlayer,yPlayer+1]);
				board[xPlayer,yPlayer+1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/dinamite"));
				board[xPlayer,yPlayer+1].transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-(yPlayer+1),0.0f);
				((Dinamite)board[xPlayer,yPlayer+1].GetComponent(typeof(Dinamite))).setCoordenates(xPlayer,yPlayer+1);
				result = true;
			}			
			break;
		case 3:
			if(canDinamite(xPlayer-1,yPlayer)){
				boardState[xPlayer-1,yPlayer] = 40;
				Destroy(board[xPlayer-1,yPlayer]);
				board[xPlayer-1,yPlayer] = (GameObject)Instantiate((GameObject)Resources.Load("Board/dinamite"));
				board[xPlayer-1,yPlayer].transform.localPosition = new Vector3(xPlayer-1-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
				((Dinamite)board[xPlayer-1,yPlayer].GetComponent(typeof(Dinamite))).setCoordenates(xPlayer-1,yPlayer);
				result = true;
			}
			break;
		}
		return result;

	}


	public bool ladder(int x, int y){
		bool result = false;
		if(0< y-1 && boardState[x,y-1]<30 && 20<=boardState[x,y-1]){
			if(boardState[x,y-1] != 21){
				Destroy(board[x,y-1]);
				boardState[x,y-1] = 21;
				board[x,y-1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
				board[x,y-1].transform.localPosition = new Vector3(x-board.GetLength(0)/2,board.GetLength(1)/2-(y-1),0.0f);
			}
			result = true;
		}
		if(y+1<boardState.GetLength(1) && boardState[x,y+1]<30 && 20<=boardState[x,y+1]){
			if(boardState[x,y+1] != 21){
				Destroy(board[x,y+1]);
				boardState[x,y+1] = 21;
				board[x,y+1] = (GameObject)Instantiate((GameObject)Resources.Load("Board/ladder"));
				board[x,y+1].transform.localPosition = new Vector3(x-board.GetLength(0)/2,board.GetLength(1)/2-(y+1),0.0f);
			}
			result = true;
		}
		return result;
	}

	public void quake(){
		int damage = (Earthquake.get().quake(this)+1)/4;
		for(int i=0;i<board.GetLength(0);++i){
			for(int j=0;j<board.GetLength(1);++j){
				if(boardState[i,j]<30 && 20<=boardState[i,j]){
					if(((Structure)board[i,j].GetComponent(typeof(Structure))).damage(damage)){
						boardState[i,j] = 10;
						board[i,j]= (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
						board[i,j].transform.localPosition = new Vector3(i-board.GetLength(0)/2,board.GetLength(1)/2-j,0.0f);
						if(i== xPlayer && j ==yPlayer)
						{
							gameOver();
						}
					}
				}
			}
		}
	}

	public void explosion(int x, int y)
	{
		Destroy(board[x,y]);
		boardState[x,y] = 10;
		board[x,y]= (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
		board[x,y].transform.localPosition = new Vector3(x-board.GetLength(0)/2,board.GetLength(1)/2-y,0.0f);
		if(canMove(x,y+1)&&boardState[x,y+1] != 30)
		{
			if(((Structure)board[x,y+1].GetComponent(typeof(Structure))).damage(2)){
				boardState[x,y+1] = 10;
				board[x,y+1]= (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
				board[x,y+1].transform.localPosition = new Vector3(x-board.GetLength(0)/2,board.GetLength(1)/2-(y-1),0.0f);
				if(x== xPlayer && y+1 ==yPlayer)
				{
					gameOver();
				}
			}
		}
		if(canMove(x,y-1)&&boardState[x,y-1] != 30)
		{
			if(((Structure)board[x,y-1].GetComponent(typeof(Structure))).damage(2)){
				boardState[x,y-1] = 10;
				board[x,y-1]= (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
				board[x,y-1].transform.localPosition = new Vector3(x-board.GetLength(0)/2,board.GetLength(1)/2-(y+1),0.0f);
				if(x== xPlayer && y-1 ==yPlayer)
				{
					gameOver();
				}
			}
		}
		if(canMove(x+1,y) && boardState[x+1,y] != 30)
		{
			if(((Structure)board[x+1,y].GetComponent(typeof(Structure))).damage(2)){
				boardState[x+1,y] = 10;
				board[x+1,y]= (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
				board[x+1,y].transform.localPosition = new Vector3(x+1-board.GetLength(0)/2,board.GetLength(1)/2-y,0.0f);
				if(x+1== xPlayer && y ==yPlayer)
				{
					gameOver();
				}
			}
		}
		if(canMove(x-1,y)&&boardState[x-1,y] != 30)
		{
			if(((Structure)board[x-1,y].GetComponent(typeof(Structure))).damage(2)){
				boardState[x-1,y] = 10;
				board[x-1,y]= (GameObject)Instantiate((GameObject)Resources.Load("Board/ground"));
				board[x-1,y].transform.localPosition = new Vector3(x-1-board.GetLength(0)/2,board.GetLength(1)/2-y,0.0f);
				if(x-1== xPlayer && y ==yPlayer)
				{
					gameOver();
				}
			}
		}
	}

	public bool repair(){
		return ((Structure)board[xPlayer,yPlayer].GetComponent(typeof(Structure))).repair();
	}
	
	public bool canMove(int x, int y){
		return (x<boardState.GetLength(0) && 0<=x && y<boardState.GetLength(1) && 0<=y && ((boardState[x,y]<30 && 20<=boardState[x,y])||boardState[x,y] ==30 ));
	}

	public bool reachable(int x, int y){
		return (x<boardState.GetLength(0) && 0<=x && y<boardState.GetLength(1) && 0<=y && boardState[x,y]==10);
	}

	public bool canDinamite(int x, int y){
		return (x<boardState.GetLength(0) && 0<=x && y<boardState.GetLength(1) && 0<=y && boardState[x,y]==11);
	}

	public int getXPlayer(){
		return xPlayer;
	}

	public int getYPlayer(){
		return yPlayer;
	}

	public void continu(){
		if(player!= null)
		((Player)player.GetComponent(typeof(Player))).pause = false;
	}

	public void gameOver(){
		Destroy(player);
		Instantiate((GameObject)Resources.Load("GameOver"));

	}

	public bool win(){
		return (boardState[xPlayer,yPlayer]==30);
	}

	public void destroyCurrent(){
		for(int i=0;i<board.GetLength(0);++i){
			for(int j=0;j<board.GetLength(1);++j){
				Destroy(board[i,j]);
			}
		}
	}

	public void nextMap(){
		destroyCurrent();
		currentMap = (currentMap+1)%maps.GetLength(0);
		boardState = new int[maps.GetLength(1),maps.GetLength(2)];

		for(int i=0;i<maps.GetLength(1);++i)
		{
			for(int j=0;j<maps.GetLength(2);++j)
			{
				boardState[i,j] = maps[currentMap,i,j];
			}
		}
		fillBoard();
		Destroy(player);
		player = (GameObject)Instantiate((GameObject)Resources.Load("player"));
		xPlayer =0;
		((Player)player.GetComponent(typeof(Player))).setResources(resources[currentMap]);
		yPlayer =0;
		player.transform.localPosition = new Vector3(xPlayer-board.GetLength(0)/2,board.GetLength(1)/2-yPlayer,0.0f);
	}

}
