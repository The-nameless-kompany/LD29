using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour {
	
	private GameObject view;
	private float force = 3.0f;
	private int magnitude = 1;
	private int shakes = 15;
	private int[] directions;
	private Vector3 poV;
	private bool isQuake= false;
	private int x=0,y=0,count =0;

	void Start(){
		view =GameObject.Find("Environment/Main Camera");
		directions = new int[shakes];
	}
	
	private void getDirections(){
		for(int x = 0; x<shakes;++x){
			directions[x]= Random.Range(0,3);
		}
	}

	public void quake()
	{
		this.getDirections();
		magnitude=Random.Range(3,shakes);
		isQuake = true;
		x=0;
		y=0;
	}

	public void Update(){
		if(isQuake)
		{
			if(count%5 == 0)
			{
				if(count%2==0)
				{
					++x;
					this.move(directions[x],true);
				}
				else
				{
					++y;
					this.move(directions[y],false);
				
				}
			}
			++count;
			if(y==magnitude)
			{
				Vector3 location = transform.position;
				location.x = 0.0f;
				location.y = 0.0f;
				transform.position = location;
				isQuake = false;
				count = 0;
			}
		}
	}
	
	/*
	3: A
	2: >
	1: V
	0: <
	*/
	private void move(int dir, bool inverted){
		poV = transform.position;
		if(inverted==false){
			switch (dir){
			case 3:
				poV.x += 1.0f/force;
				break;
			case 2:
				poV.y += 1.0f/force;
				break;
			case 1:
				poV.x -= 1.0f/force;
				break;
			case 0:
				poV.y -= 1.0f/force;
				break;
			}
		}else{
			switch (dir){
			case 3:
				poV.x -= 1.0f/force;
				break;
			case 2:
				poV.y -= 1.0f/force;
				break;
			case 1:
				poV.x += 1.0f/force;
				break;
			case 0:
				poV.y += 1.0f/force;
				break;
			}
		}
		transform.position = poV;
		//StartCoroutine(wait(100.0F));
	}	
	
	//retorna Earthquake
	public static Earthquake get() {
		return FindObjectOfType(typeof(Earthquake)) as Earthquake;
	}
}
