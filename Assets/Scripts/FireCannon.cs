using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour{

	//creates an Animator
	Animator animCannonFire;
	//public Texture button;
	public GUIStyle buttonStyle;
	public Rigidbody2D ball;
	public float ballPositionX;
	public float ballPositionY;
	public float ballVelocity;
	public float ballAngle;


	// Use this for initialization
//	void Start () {
		//gets the Animator Component
//		animCannonFire = GetComponent<Animator>();

//	}
	// Update is called once per frame
/*	void Update () {
	}

	void OnGUI () {	
		GUI.contentColor = Color.white;
		if(GUI.Button(new Rect(393,379,67,22),"",buttonStyle)){
			resetBall();
			fireBall();
		}
	}

	void resetBall(){
		ball.velocity = new Vector3 (0,0,0);
		ball.transform.position = new Vector3(ballPositionX, ballPositionY, 0.0f);	
	}

	void fireBall(){
		animCannonFire.SetTrigger("cannonFireTrigger");
		ball.AddForce (new Vector2 (ballVelocity,ballAngle));
	}*/
}
