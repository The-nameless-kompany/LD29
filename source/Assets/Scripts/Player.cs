using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Texture right = null;
	public Texture left = null;
	public AudioClip digSound = null;
	public AudioClip walkSound = null;
	public int digCost = 120;
	public int repairCost = 60;
	public int dinamiteCost = 130;
	public bool pause = false;
	public int resources = 1000;
	public int speed = 15;
	public int actionsByWalk = 1;
	public int actionsByDigRightOrLeft = 2;
	public int actionsByDigUpOrDown = 3;
	public int actionsByDinamite = 2;

	private int actionsPerQuake = 20;
	private string pauseKey = "p";
	private int direction = 1;
	private int yDir = 0;
	private int xDir = 0;
	private int count = 0;
	private GameManager gameManager;
	private Vector3 position;
	private GameObject silverText;
	private int actions = 0;
	private bool moving = false;


	/*
	 * Simbology
	 * 
	 * 1: dig
	 */ 
	private int action = 1;

	public static Player get() {
		return FindObjectOfType(typeof(Player)) as Player;
	}


	// Use this for initialization
	void Start () {
		gameManager = GameManager.obtener();
		silverText = GameObject.Find("Game/SilverText");
		silverText.guiText.text = "Silver: "+resources;
	}
	
	// Update is called once per frame
	void Update () {
		if(!pause){
			if(Input.GetKey(KeyCode.UpArrow)){
				if(gameManager.move(0)){
					pause = true;
					moving = true;
					yDir = 1;
					count = speed;
				}
				direction = 0;
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				if(gameManager.move(1)){
					pause = true;
					moving = true;
					xDir = 1;
					count = speed;
				}
				this.renderer.material.mainTexture = right;
				direction = 1;
			}
			if(Input.GetKey(KeyCode.DownArrow)){
				if(gameManager.move(2)){
					pause = true;
					moving = true;
					yDir = -1;
					count = speed;
				}
				direction = 2;
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				if(gameManager.move(3)){
					pause = true;
					moving = true;
					xDir = -1;
					count = speed;
				}
				this.renderer.material.mainTexture = left;
				direction = 3;
			}
			if(Input.GetKeyDown(pauseKey)){
				pause = true;
				Instantiate((GameObject)Resources.Load("Pause menu"));
				(FindObjectOfType(typeof(Pause)) as Pause).setPlayer(this);
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				switch (action){
				case 1:
					if(0<=(resources-digCost)){
						if(gameManager.dig(direction)){
							resources-=digCost;
							audio.PlayOneShot(digSound);
							silverText.guiText.text = "Silver: "+resources;
							if(direction == 0||direction ==2){
								actions+=actionsByDigUpOrDown;
							}
							else{
								actions+=actionsByDigRightOrLeft;
							}
						}
					}
					if(0<=(resources-dinamiteCost)){
						if(gameManager.dinamite(direction)){
							resources-=digCost;
							silverText.guiText.text = "Silver: "+resources;
							actions+=actionsByDinamite;
						}
					}
					break;
				}
			}
			if(Input.GetKeyDown(KeyCode.LeftControl)){
				if(gameManager.repair())
				{
					if(0<=(resources-repairCost)){
						resources -= repairCost;
						audio.PlayOneShot(digSound);
						silverText.guiText.text = "Silver: "+resources;
					}
				}
			}
		}

		if(moving){
			position = transform.localPosition;
			if(yDir!=0){
				if(yDir==1){
					position.y += 1.0f/speed;
					
				}
				else{
					position.y -= 1.0f/speed;
				}
			}
			if(xDir!=0){
				if(xDir==1){
					position.x += 1.0f/speed;
					
				}
				else{
					position.x -= 1.0f/speed;
				}
			}
			--count;
			transform.localPosition = position;
			if(count ==0){
				xDir = 0;
				yDir = 0;
				pause = false;
				moving = false;
				actions += actionsByWalk;
			}

		}
		if(actionsPerQuake<actions){
			pause = true;
			gameManager.quake();
			actions = 0;
		}
	}

	public void setResources(int resources){
		this.resources = resources;
	}
}
