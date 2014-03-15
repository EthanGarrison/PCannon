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


	// Use this for initialization
	void Start () {
		LegGenerator();		
		
		//For debug purpose
		//Debug.Log(LegC);
	}
	
	// Update is called once per frame
	void Update () {

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
