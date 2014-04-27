using UnityEngine;
using System.Collections;

public class Structure : MonoBehaviour {


	public int life = 100;
	private int x =-1;
	private int y =-1;
	public Texture badState = null;

	// Use this for initialization
	void Start () {

	}

	/*
	 * Simbology of damage
	 * 1: low damage
	 * 2: medium damage
	 * 3: high damage
	 * 4: critical damage
	 */ 
	public bool damage(int damage){
		bool result = true;
		life -= Random.Range(8*damage,15*damage);
		if(life < 1)
		{
			result = false;
		}
		if(life < 50 && badState != null)
		{
			this.renderer.material.mainTexture = badState;
		}
		return result;
	}

	public void setCoordinates(int x,int y){
		this.x = x;
		this.y = y;
	}

	public int getX(){
		return x;
	}

	public int getY(){
		return y;
	}
}
