using UnityEngine;
using System.Collections;

public class GameStat : MonoBehaviour {

	public static bool gameStatDisplayUp = false;	
	private Rect statDisplay,  label, button, gameOverDisplay;
	public GUIStyle fontLabel,fontButton,gameStatStyle;
	// Use this for initialization
	void Start () {
		gameStatDisplayUp = true;
		statDisplay	= new Rect (
			GameMaster.nativeWidth* (1.5f/32), 
			GameMaster.nativeHeight* (1.5f/20), 
			GameMaster.nativeWidth * (16f/32), 
			GameMaster.nativeHeight * (14f/20)
		);
		gameOverDisplay	= new Rect (
			GameMaster.nativeWidth* (1.5f/32), 
			GameMaster.nativeHeight* (1.5f/20), 
			GameMaster.nativeWidth * (29f/32), 
			GameMaster.nativeHeight * (17f/20)
		);
		label = new  Rect(10,15,statDisplay.width,statDisplay.height);
		button = new Rect (statDisplay.width/2f-100f,statDisplay.height-150f,200f,100f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);
			if(GameMaster.FuseLife > 0){
				GUI.BeginGroup (statDisplay, gameStatStyle);
				GUI.Label(new  Rect(10,15,statDisplay.width,statDisplay.height),
				"Victory! \n" +
				"Next Level"+ " : " + GameMaster.level + "\n" +
				"FuseLife: " + GameMaster.FuseLife + "\n" +
				"Total Points : " + GameMaster.point + "\n",fontLabel);
				
				if(GUI.Button (new Rect (statDisplay.width/2f-100f,statDisplay.height-150f,200f,100f), "OK",fontButton)){
					gameStatDisplayUp = false;
					Object.Destroy(gameObject, 0f);
				}
			}else{
				GUI.BeginGroup (gameOverDisplay, gameStatStyle);
				GUI.Label(new  Rect(10,15,gameOverDisplay.width,gameOverDisplay.height),
				"Game Over \n" +
				"Highest Level"+ " : " + (GameMaster.level-1) + "\n" +
				"FuseLife: " + 0 + "\n" +
				"Total Points : " + GameMaster.point + "\n", fontLabel);
				
				if(GUI.Button (new Rect (gameOverDisplay.width/2f-100f,gameOverDisplay.height-150f,200f,100f), "OK",fontButton)){
					gameStatDisplayUp = false;
					Object.Destroy(gameObject, 0f);
					Application.LoadLevel(0);
				}			
			}
		GUI.EndGroup ();
	}
}
