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
	private int Points = 0;
	public GameObject Calculator;
	private bool toggleCalcActive = false;
	public GUIStyle Style;
	
	
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

	//Prototype GUI
	private void OnGUI(){
		//Allows dynamic GUI Scaling.
		AutoResize(NativeWidth, NativeHeight);	

		//Once Calculator GameObject is created, attach it to the GM.
		//Toggles the ActiveState of the Calculator GameObject
		if(GUILayout.Button ("Calculator")){
			Calculator.SetActive(!toggleCalcActive);
			toggleCalcActive = !toggleCalcActive;
		}
		
			}

	//Resizes Screen
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
