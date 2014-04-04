using UnityEngine;
using System.Collections;
using System;

public class GameMaster : MonoBehaviour {
	public Calculator Calc;
	public BallFlightPath CannonBall;
	public GameObject StatScreenPrefab;

	int[,] BelowLvl10LegsAB = new int[10, 2] { 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }, 
		{ 3, 4 }, { 5, 12 }, { 9, 12 }, { 12, 16 }, { 24, 7 }
	};
	private double LegA, LegB, LegC;	
	
	public GUIStyle GUITheme;

	public static int level = 1;
	public static int FuseLife = 10;
	public static int point = 0;
	public static int nativeWidth = 1920, nativeHeight = 1200;
	private Rect userAnswerRect; 

	void Start () {
		LegGenerator();
		userAnswerRect = new Rect(0, nativeHeight-15,75,15);

	}
	void Update(){
	}

	private void OnGUI(){
		AutoResize(nativeWidth, nativeHeight);	//Allows dynamic GUI Scaling.
		GUI.depth = 1;

		GUI.contentColor = Color.black;

		GUILayout.Label("Leg A: " + LegA, GUITheme);
		GUILayout.Label("Leg B: " + LegB, GUITheme);

		if(!GameStat.gameStatDisplayUp){
			if(GUILayout.Button ("Fire!")){ExecuteHitResult();} //For Debug Purpose			
		}
	}
	
	public void ExecuteHitResult(){//Executes the animation of the castle being hit range	
		double UserAnswerDouble = Double.Parse(Calc.displayText);
		//TODO: Round the Double	
		if(UserAnswerDouble < LegC){
			CannonBall.PlayShort();
			FuseLife--;
		}
		else if(UserAnswerDouble > LegC){
			CannonBall.PlayLong();
			IncPoint(5);
			FuseLife-=2;
		}
		else{
			CannonBall.PlayExact();
			IncPoint(10);
			Instantiate(StatScreenPrefab);
			level++;
			LegGenerator();	
		}

		Calc.Clear();
		Calc.padDisplay = false;
		Calc.functDisplay = false;
		if(FuseLife <= 0)
			Instantiate(StatScreenPrefab);
	}

	private void IncPoint(int p){
		point += p;
		gameObject.guiText.text = point.ToString();
	}

	//TODO Round LegC
	private void LegGenerator(){ //Determines which Legs to use
		if(level <=10){ //use the array
			
			LegA = BelowLvl10LegsAB[level-1,0];
			LegB = BelowLvl10LegsAB[level-1,1];
			LegC = CalcLegC(LegA,LegB);
		}
		else{ //increment ever other leg by 2
			LegA = Math.Round(LegA);
			LegB = Math.Round(LegB);
			if(level%2==0){
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

	public static void AutoResize(int screenWidth, int screenHeight){ //Resizes Screen to a specified Dimention
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	public bool HasValidInput(){ //Checks if this is a valid input
		double number;
		return (Calc.displayText != "" && Double.TryParse(Calc.displayText, out number)? true : false);
	}

	public void ClearInput(){
		Calc.Clear();
	}
}
