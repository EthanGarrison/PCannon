using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public static bool gameStatDisplayUp = false;

	// Use this for initialization
	void Start () {
		gameStatDisplayUp = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);
		Rect StatDisplay = new Rect (
			GameMaster.nativeWidth/14, 
			GameMaster.nativeHeight/14, 
			GameMaster.nativeWidth * (12f/14), 
			GameMaster.nativeHeight * (12f/14)
		);

		GUI.BeginGroup (StatDisplay);
			GUI.Box (new Rect (0,0,StatDisplay.width,StatDisplay.height), " GameOver");
			GUI.Label(new Rect(10,15,StatDisplay.width,StatDisplay.height),
			"Highest Level"+ " : " + (GameMaster.level-1) + "\n" +
			"Remaining FuseLife: " + 0 + "\n" +
			"Total Points : " + GameMaster.point + "\n");

			if(GUI.Button (new Rect (StatDisplay.width/2-40,StatDisplay.height-35,80,30), "OK")){
					gameStatDisplayUp = false;
					Object.Destroy(gameObject, 0f);
					//Go to title screen
			}

		GUI.EndGroup ();
	}
}
