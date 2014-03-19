using UnityEngine;
using System.Collections;
using System;

public class GameMaster : MonoBehaviour {

	public GameObject NumPad;

	private bool toggleNumActive = false;
	public GameObject Calc;
	public bool needHelpActive = false;

	public BallFlightPath CannonBall;

	int[,] BelowLvl10LegsAB = new int[10, 2] { 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }, 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }
	};
	private double LegA, LegB, LegC;	
	
	private bool GameStatDisplayUp = false;	
	private int Level = 1;
	private int FuseLife = 10;
	private int Points = 0;
	public GUIStyle PointStyle;
	public GUIStyle GUITheme;
	public String UserAnswer= "";

	public int NativeWidth, NativeHeight;

	void Start () {
		LegGenerator();
	}
	void Update(){
		if(NumPad.activeSelf){
			UserAnswer = NumPad.GetComponent<Numpad>().displayText;
		}
	}

	private void OnGUI(){
		AutoResize(NativeWidth, NativeHeight);	//Allows dynamic GUI Scaling.
		GUI.Label(new Rect(NativeWidth-100,NativeHeight-30, 100, 30),Points.ToString(),PointStyle); //Points DisplayBox

		if(!GameStatDisplayUp){
			if(GUI.Button (new Rect(0, NativeHeight-15,75,15),UserAnswer)){//Toggles the ActiveState of the Calculator GameObject	
				DisplayNumPad();
			}
			if(GUILayout.Button ("Fire!")){ExecuteHitResult();} //For Debug Purpose			
			}else{
				if(FuseLife<=0){
					DisplayGameStat("No more life", "Level");
					//Load new level once multiple scene has been made
					//Application.LoadLevel (0);
				}
				else{
					DisplayGameStat("Correct!", "Next Level");
				}
			}
		}

	private void DisplayGameStat(String Title, String LevelTitle){ //GUI OF THE STAT GAME		
		Rect StatDisplay = new Rect (0, 0, NativeWidth, NativeHeight);
		GUI.BeginGroup (StatDisplay);
		// We'll make a box so you can see where the group is on-screen.
		GUI.Box (new Rect (0,0,StatDisplay.width,StatDisplay.height), Title);
		GUI.Label(new Rect(10,15,StatDisplay.width,StatDisplay.height),
			LevelTitle + " : " + Level + "\n" +
			"Remaining FuseLife: " + FuseLife + "\n" +
			"Total Points : " + Points + "\n");

		if(GUI.Button (new Rect (StatDisplay.width/2-40,StatDisplay.height-35,80,30), "OK")){
			GameStatDisplayUp = false;	
		}
		GUI.EndGroup ();
	}
	
	public void ExecuteHitResult(){//Executes the animation of the castle being hit range
		if(!GameStatDisplayUp){
			double UserAnswerDouble = Double.Parse(UserAnswer);
			//TODO: Round the Double	
			if(UserAnswerDouble < LegC){
				CannonBall.PlayShort();
				FuseLife--;
			}
			else if(UserAnswerDouble > LegC){
				CannonBall.PlayLong();
				Points+=5;
				FuseLife-=2;
			}
			else{
				CannonBall.PlayExact();
				Points+=10;
				EndLevel(false);
			}

			ClearInput();
			if(FuseLife <= 0){
				EndLevel(true);		
			}
		}
	}

	private void EndLevel(bool GameOver){
		GameStatDisplayUp = true; 
		toggleNumActive = false;
		NumPad.SetActive(false);
		if(!GameOver){ //increments level if false
			Level++;
		}
		LegGenerator();
	}

	private void DisplayNumPad(){
		if(needHelpActive){
			Calc.SetActive(!toggleNumActive);			
		}else{
			NumPad.SetActive(!toggleNumActive);
		}
		toggleNumActive = !toggleNumActive;
	}

	
	//TODO Round LegC
	private void LegGenerator(){ //Determines which Legs to use
		if(Level <=10){ //use the array
			
			LegA = BelowLvl10LegsAB[Level-1,0];
			LegB = BelowLvl10LegsAB[Level-1,1];
			LegC = CalcLegC(LegA,LegB);
		}
		else{ //increment ever other leg by 2
			LegA = Math.Round(LegA);
			LegB = Math.Round(LegB);
			if(Level%2==0){
				LegA+=2;				
				LegC = CalcLegC(LegA,LegB);	
			}
			else{
				LegB+=2;
				LegC = CalcLegC(LegA,LegB);
			}
		}
	}

	private double CalcLegC(double LegA,double LegB){//Calculates Pythagorean theorem
		double C = (LegA*LegA)+(LegB*LegB);
		return Math.Sqrt(C);
	}

	public void AutoResize(int screenWidth, int screenHeight){ //Resizes Screen to a specified Dimention
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	public bool HasValidInput(){ //Checks if this is a valid input
		double number;
		return (UserAnswer != "" && Double.TryParse(UserAnswer, out number)? true : false);
	}

	public void ClearInput(){
		UserAnswer = "";
		NumPad.GetComponent<Numpad>().Clear();
	}

}
