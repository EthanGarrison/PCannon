using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

	Rect startRect, exitRect;
	public GUIStyle startButton, exitButton;
	public GameMaster GM;

	// Use this for initialization
	void Start () {
	
		startRect = new Rect(
						GameMaster.nativeWidth*(5.5f/32),
						GameMaster.nativeHeight*(7f/32),
						GameMaster.nativeWidth*(22f/32),
						GameMaster.nativeHeight*(6f/32)
						);

		exitRect = new Rect(
						GameMaster.nativeWidth*(28f/32),
						GameMaster.nativeWidth*(1f/32),
						GameMaster.nativeWidth*(3f/32),
						GameMaster.nativeWidth*(3f/32)
						);

	}

	void OnGUI() {
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);

		if(GUI.Button(startRect, "Play Game", startButton)){
			Application.LoadLevel(1);
		}

		if(GUI.Button(exitRect, "", exitButton) || Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
