using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

	Rect startRect, exitRect;

	// Use this for initialization
	void Start () {
	
		startRect = new Rect(0,0,100,100);

		exitRect = new Rect(100,100,100,100);

	}

	void OnGUI() {

		if(GUI.Button(startRect, "Start Game")){
			Application.LoadLevel(1);
		}

		if(GUI.Button(exitRect, "Exit Game")){
			Application.Quit();
		}
	}
}
