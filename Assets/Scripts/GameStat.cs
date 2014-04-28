using UnityEngine;
using System.Collections;

public class GameStat : MonoBehaviour {

	public static bool gameStatDisplayUp = false;	
	private Rect statDisplay,  label, button, gameOverDisplay;
	public GUIStyle victoryLabel, fontLabel, fontButton, gameStatStyle;
	// Use this for initialization
	void Start () {
		gameStatDisplayUp = true;
		statDisplay	= new Rect (
			GameMaster.nativeWidth* (2f/32), 
			GameMaster.nativeHeight* (2f/20), 
			GameMaster.nativeWidth * (14f/32), 
			GameMaster.nativeHeight * (16f/20)
		);
		gameOverDisplay	= new Rect (
			GameMaster.nativeWidth* (1.5f/32), 
			GameMaster.nativeHeight* (1.5f/20), 
			GameMaster.nativeWidth * (29f/32), 
			GameMaster.nativeHeight * (17f/20)
		);
	}
	
	void Update () {

	}

	void OnGUI(){
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);
			if(GameMaster.fuseLife > 0){
				GUI.BeginGroup (statDisplay, gameStatStyle);
				GUI.Label(new  Rect(statDisplay.width* (2f/15),statDisplay.height* (2f/17), statDisplay.width * (10f/15),statDisplay.height * (3f/17)),
				"Victory!",victoryLabel);

				GUI.Label(new  Rect(statDisplay.width* (2f/15),statDisplay.height* (6f/17), statDisplay.width * (10f/15),statDisplay.height * (6f/17)),
				"Level: " + GameMaster.level + "\n" +
				"Score: " +  GameMaster.point + "\n" +
				"Lives " + GameMaster.fuseLife + "\n",fontLabel);
				
				if(GUI.Button (new Rect (statDisplay.width* (8f/15),statDisplay.height* (13f/17),statDisplay.width * (5f/15),statDisplay.height * (2f/17)), "OK",fontButton)){
					gameStatDisplayUp = false;
					Object.Destroy(gameObject, 0f);
				}
			}else{
				GUI.BeginGroup (gameOverDisplay, gameStatStyle);
				GUI.Label(new  Rect(10,15,gameOverDisplay.width,gameOverDisplay.height),
				"Game Over \n" +
				"Highest Level"+ " : " + (GameMaster.level-1) + "\n" +
				"Fuse Life: " + 0 + "\n" +
				"Total Points : " + GameMaster.point + "\n", fontLabel);
				
				if(GUI.Button (new Rect (gameOverDisplay.width/2f-100f,gameOverDisplay.height-150f,200f,100f), "Play Again?",fontButton)){
					gameStatDisplayUp = false;
					GameMaster.Reset();
					Object.Destroy(gameObject, 0f);
				}			
			}
		GUI.EndGroup ();
	}
}
