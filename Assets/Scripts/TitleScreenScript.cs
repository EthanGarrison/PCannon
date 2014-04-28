using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

	Rect startRect, exitRect;
	public GUIStyle startButton, exitButton;
	public static int nativeWidth = 1920, nativeHeight = 1200;

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
		AutoResize(nativeWidth,nativeHeight);

		if(GUI.Button(startRect, "Play Game", startButton)){
			Application.LoadLevel(1);
		}

		if(GUI.Button(exitRect, "", exitButton) || Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public static void AutoResize(int screenWidth, int screenHeight){ //Resizes Screen to a specified Dimention
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}
