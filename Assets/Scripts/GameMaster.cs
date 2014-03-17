using UnityEngine;
using System.Collections;
using System;

		//first create a input box.
		//User is only presented with a number and punctuation keyboard
		//take in input 
		//validate input
		//assign the input to Ans
		//clear input box

public class GameMaster : MonoBehaviour {

	int[,] BelowLvl10LegsAB = new int[10, 2] { 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }, 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }
	};

	private double LegA, LegB, LegC;
	public int Level = 1;
	private int Points = 0;
	public GameObject Calculator;
	private bool toggleCalcActive = false;
	public GUIStyle Style;
	public String UserAnswer= "";
	private TouchScreenKeyboard keyboard;
	
	//This will be the default width/height
	public int NativeWidth, NativeHeight;
	
	// Use this for initialization
	void Start () {
		LegGenerator();		
		
		//For debug purpose
		//Debug.Log(LegC);
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

		//------For Debug Purpose. Clears the Input box 
//		if(GUILayout.Button ("Clear Input")){
//			ClearInput();
//		}

		//------For Debug Purpose.  Increments points until fire function can be implemented
		if(GUILayout.Button ("Fire")){
			IncreasePoints(20);
			ClearInput();
		}

		//Displays the UserAnswer Text Box
		UserAnswer = GUI.TextField(new Rect(0,NativeHeight-30,100,30), UserAnswer);

		//------For Debug Purpose. Sees if the UserAnswer is valid				
		ValidateUserInput(UserAnswer);
		
		//YYYYYY ProtoType: suppose to only bring up keyboard and punctuation only
		//Need to check if this actually works on Android
		keyboard = TouchScreenKeyboard.Open(UserAnswer, 
			TouchScreenKeyboardType.NumbersAndPunctuation, //Define Keyboard Type
			false, //autorcorrection
			false, //multiline
			false, //secure ***
			false  //alert
			);

		//Points DisplayBox
		GUI.Label(new Rect(NativeWidth-100,NativeHeight-30, 100, 30),Points.ToString(),Style);
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
	/**********************************/

	/***POINTS INCREMENT*******/
	//This function is publically accessable and increases the points by whatever was given.
	public void IncreasePoints(int Points){
		this.Points += Points;
	}
	/**********************************/
	
	/***INPUT ***********************/
	//This function is publically accessable and valididates the take in input
	private bool ValidateUserInput(String input){
		double number;
		return Double.TryParse(input, out number) ? true : false;
	}

	private void ClearInput(){
		UserAnswer = "";
	}
	/**********************************/
	
	//Determines which Legs to use
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
