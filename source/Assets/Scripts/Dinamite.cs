using UnityEngine;
using System.Collections;

public class Dinamite : MonoBehaviour {

	public AudioClip explosionSound = null;
	public float timer = 1.0f;
	private float count = 0.0f;
	private GameManager gameManager;
	private int x=0;
	private int y=0;
	private bool play= false;

	void Start () {
		gameManager = GameManager.obtener();
	}
	
	// Update is called once per frame
	void Update () {
	
		count+= Time.deltaTime;
		if(timer < count && !play)
		{
			audio.PlayOneShot(explosionSound);
			play = true;
			count = 0.0f;
		}
		if(play && 0.72f<count)
		{
			gameManager.explosion(x,y);

		}
	}

	public void setCoordenates(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
