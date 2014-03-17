using UnityEngine;
using System.Collections;
using System;

public class GameMaster : MonoBehaviour {

	int[,] BelowLvl10LegsAB = new int[10, 2] { 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }, 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }
	};

	private double LegA, LegB, LegC;
	public int Level = 1;
	public int FuseLife = 10;
	private int Points = 0;

	public GameObject Calculator;
	private bool toggleCalcActive = false;
	
	public GameObject CannonBall;
	
	public GUIStyle Style;
	public String UserAnswer= "";
	
	//This will be the default width/height
	public int NativeWidth, NativeHeight;
	
	// Use this for initialization
	void Start () {
		LegGenerator();		
	}
	
	// Update is called once per frame
	void Update () {

	}

	//GUI for all display
	private void OnGUI(){
		//Allows dynamic GUI Scaling.
		AutoResize(NativeWidth, NativeHeight);	

		//Once Calculator GameObject is created, attach it to the GM.
		//Toggles the ActiveState of the Calculator GameObject
		if(GUILayout.Button ("Calculator")){
			Calculator.SetActive(!toggleCalcActive);
			toggleCalcActive = !toggleCalcActive;
		}
		if(GUILayout.Button ("Fire!")){
			ExecuteHitResult();
		}


		//Displays the UserAnswer Text Box
		UserAnswer = GUI.TextField(new Rect(0,NativeHeight-30,100,30), UserAnswer);

		//Points DisplayBox
		GUI.Label(new Rect(NativeWidth-100,NativeHeight-30, 100, 30),Points.ToString(),Style);
	}


	//Executes the animation of the castle being hit range
	public void ExecuteHitResult(){
		double UserAnswerDouble = Double.Parse(UserAnswer);
		//TODO: Round the Double	
		if(UserAnswerDouble < LegC){
			CannonBall.GetComponent<BallFlightPath>().PlayShort();
			FuseLife--;
		}else if(UserAnswerDouble > LegC){
			CannonBall.GetComponent<BallFlightPath>().PlayLong();
			Points+=5;
			FuseLife-=2;
		}else{
			CannonBall.GetComponent<BallFlightPath>().PlayExact();
			Points+=10;
			//Need go to next level
		}	
		ClearInput();
	}

	//Checks if this is a valid input
	public bool HasValidInput(){
		double number;
		if(UserAnswer != "" && Double.TryParse(UserAnswer, out number)){
			return true;
		}
		return false;
	}

	/***RESIZEING*****************/
	//Resizes Screen to a specified Dimention
	public static void AutoResize(int screenWidth, int screenHeight){
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	//This is just for debug purpose to get the size of rectangles under AutoResize
	public static Rect GetResizedRect(Rect rect){
		Vector2 position = GUI.matrix.MultiplyVector(new Vector2(rect.x, rect.y));
		Vector2 size = GUI.matrix.MultiplyVector(new Vector2(rect.width, rect.height));

		return new Rect(position.x, position.y, size.x, size.y);
	}
	
	public void ClearInput(){
		UserAnswer = "";
	}
	
	//Determines which Legs to use
	//TODO Round LegC
	private void LegGenerator(){
		if(Level <=10){
			//use the array
			LegA = BelowLvl10LegsAB[Level-1,0];
			LegB = BelowLvl10LegsAB[Level-1,1];
			LegC = CalcLegC(LegA,LegB);
		}
		else{
			//increment ever other leg by 2
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

	//Calculates Pythagorean theorem
	private double CalcLegC(double LegA,double LegB){
		double C = (LegA*LegA)+(LegB*LegB);
		return Math.Sqrt(C);
	}
}
