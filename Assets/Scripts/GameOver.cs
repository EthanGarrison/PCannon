using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public static bool gameStatDisplayUp = false;
	private Rect statDisplay, box, label, button;

	// Use this for initialization
	void Start () {
		gameStatDisplayUp = true;
		statDisplay	= new Rect (
			GameMaster.nativeWidth* (1.5f/32), 
			GameMaster.nativeHeight* (1.5f/20), 
			GameMaster.nativeWidth * (29f/32), 
			GameMaster.nativeHeight * (17f/20)
		);
		box = new Rect (0,0,statDisplay.width,statDisplay.height);
		label = new  Rect(10,15,statDisplay.width,statDisplay.height);
		button = new Rect (statDisplay.width/2f-40f,statDisplay.height-35f,80f,30f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);

		GUI.BeginGroup (statDisplay);
			GUI.Box (box, " GameOver");
			GUI.Label(label,
			"Highest Level"+ " : " + (GameMaster.level-1) + "\n" +
			"Remaining FuseLife: " + 0 + "\n" +
			"Total Points : " + GameMaster.point + "\n");

			if(GUI.Button (button, "OK")){
					gameStatDisplayUp = false;
					Object.Destroy(gameObject, 0f);
					//Go to title screen
			}

		GUI.EndGroup ();
	}
}
