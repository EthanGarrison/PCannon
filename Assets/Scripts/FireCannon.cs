using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour{

	//creates an Animator
	Animator animCannonFire;
	//public Texture button;
	public GUIStyle buttonStyle;
	public Rigidbody2D ball;


	// Use this for initialization
	void Start () {
		//gets the Animator Component
		animCannonFire = GetComponent<Animator>();

	}
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {	
		GUI.contentColor = Color.white;
				if(GUI.Button(new Rect(393,379,67,22),"",buttonStyle)){
				//if (GUI.Button(new Rect(393,379,67,22),button)){
					animCannonFire.SetTrigger("cannonFireTrigger");
					ball.AddForce (new Vector2 (80,10));
		}
		}
		
}
