using UnityEngine;
using System.Collections;
using System;

public class Calculator : MonoBehaviour {

	//These are necessary for the calculator funtionality
	private bool isDecimal = false,  isCalculated = false, isAddition = false;
	public string displayText = "";
	private float varA = 0f, varB = 0f;
	private float? firstValue = null, secondValue = null;
	public GameMaster GM;

	//These are the things for the layout
	public GUIStyle calcLabel, calcBackground, calcHelp;
	public GUIStyle calcButton1, calcButton2, calcButton3, calcButton4;
	private Rect button1, button2, button3, button4, labelRect, helpRect;
	private Rect group1, group2, group3, group4, group5;
	private Rect bgCalc;

	//These are for Calculator Display
	public bool padDisplay = false;
	public bool functDisplay = false;

	private Rect createCRect(float x){
		return new Rect((bgCalc.width)*(x/16.5f),(bgCalc.height)*(3.5f/16.5f),(bgCalc.width)*(2.5f/16.5f),(bgCalc.height)*(13f/16.5f));
	}

	private Rect createBRect(float y){
		return new Rect(0,group1.height* (y/13),group1.width,group1.height*(2.5f/13));
	}

	void Start () {
		//Initializing the variables
		bgCalc = new Rect(
			GameMaster.nativeWidth*(9f/32),
			GameMaster.nativeHeight*(1f/20),
			GameMaster.nativeWidth*(17f/32),
			GameMaster.nativeHeight*(17f/20)
			);

		labelRect = new Rect(
			0,
			0,
			bgCalc.width*(9.5f/16.5f),
			bgCalc.height*(2.5f/16.5f)
			);

		helpRect = new Rect(
			bgCalc.width*(10.5f/16.5f) ,
			0,
			bgCalc.width*(6f/16.5f),
			bgCalc.height*(2.5f/16.5f)
			);

		group1 = createCRect(0f);
		group2 = createCRect(3.5f);
		group3 = createCRect(7f);
		group4 = createCRect(10.5f);
		group5 = createCRect(14f);

		button1 = createBRect(0);
		button2 = createBRect(3.5f);
		button3 = createBRect(7);
		button4 = createBRect(10.5f);
	}

	void Update(){
	}
	
	void OnGUI () {
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);
		GUI.depth = 0;

		GUI.BeginGroup(bgCalc,calcBackground);
			if(!GameStat.gameStatDisplayUp){
			//Calculator Label
				if(GUI.Button(labelRect, padDisplay ? displayText : "Input", calcLabel)){
					padDisplay = !padDisplay;
					functDisplay = false;
				}
			}

			if(padDisplay){
				//Expand Button
				if(GUI.Button(helpRect, functDisplay ? "" : "Help!", functDisplay ? calcBackground :calcHelp )){
					functDisplay = true;
				}


				GUI.BeginGroup(group1);
					if(GUI.Button(button1, "7", calcButton1))
						InputValue("7");
					if(GUI.Button(button2, "4", calcButton2))
						InputValue("4");
					if(GUI.Button(button3, "1", calcButton3))
						InputValue("1");
					if(GUI.Button(button4, "0", calcButton4))
					InputValue("0");
				GUI.EndGroup();

				GUI.BeginGroup(group2);
					if(GUI.Button(button1, "8", calcButton1))
						InputValue("8");
					if(GUI.Button(button2, "5", calcButton2))
						InputValue("5");
					if(GUI.Button(button3, "2", calcButton3))
						InputValue("2");
					if(GUI.Button(button4, ".", calcButton4))
						InputValue(".");
				GUI.EndGroup();
				
				GUI.BeginGroup(group3);
					if(GUI.Button(button1, "9", calcButton1))
						InputValue("9");
					if(GUI.Button(button2, "6", calcButton2))
						InputValue("6");
					if(GUI.Button(button3, "3", calcButton3))
						InputValue("3");
					if(GUI.Button(button4, "Clr", calcButton4))
						Clear();
				GUI.EndGroup();

			if(functDisplay){

				GUI.BeginGroup(group4);
					if(GUI.Button(button1, "√", calcButton1))
						Calculate(null,"squareRoot");
					if(GUI.Button(button2, "X²", calcButton2))
						Calculate(null,"square");
					if(GUI.Button(button3, "+", calcButton3))
						InputValue("+");
					if(GUI.Button(button4, "=", calcButton4)){
						try{
							Calculate((float)secondValue, "+");
						}
						catch(Exception e){
							Debug.Log("This is from the equals button: " + e);
						}
					}
				GUI.EndGroup();

				GUI.BeginGroup(group5);
					if(GUI.Button(button1, "A", calcButton1))
						DisplayStoredValue("A");
					if(GUI.Button(button2, "s A", calcButton2))
						StoreValue("A");
					if(GUI.Button(button3, "B", calcButton3))
						DisplayStoredValue("B");
					if(GUI.Button(button4, "s B", calcButton4))
						StoreValue("B");
				GUI.EndGroup();
			}
		}

		GUI.EndGroup();
		}

	//Function for two number functions.
	void Calculate(float? number, string operSym) {
		float? value = firstValue;
		try{
			switch (operSym){
				case "+":
					value += number;
					break;
				case "square":
					value = Mathf.Pow((float)value,2f);
					break;
				case "squareRoot":
					value = Mathf.Pow((float)value,0.5f);
					break;
				default:
					break;
			}
		}catch(Exception e){
			Debug.Log("Cannot square or Sqrt a null: " + e);
		}
		
		if(!operSym.Equals("")){
			Clear();
			isCalculated = true;
		}
		else
			isCalculated = false;
		displayText = value + "";
		firstValue = value;
	}

	public void Clear(){
		firstValue = null;
		secondValue = null;
		isAddition = false;
		isCalculated = false;
		isDecimal = false;
		displayText =  "";
	}

	//Value input function.  Checks to see which value we are working with and then if it already has a value
	//If it has a value, we append the entered number onto the end of the number.
	void InputValue(string input){
		if(isCalculated)
			displayText = "";

		if(input == "."){
			if(!isDecimal){
				displayText += input;
				GetValueOfInput();
			}
		}
		else if(input != "+"){
			displayText += input;
			GetValueOfInput();
		}
		else{
			displayText = "";
			isAddition = true;
			isDecimal = false;
		}

		isCalculated = false;		
	}

	void DisplayStoredValue(string keyPressed){
		float variable;

		if(keyPressed == "A")
			variable = varA;
		else
			variable = varB;
		
		if(firstValue != null && isAddition){
			secondValue = variable;
			displayText = secondValue + "";
		}
		else{
			firstValue = variable;
			displayText = firstValue + "";
		}

		isCalculated = true;
	}

	void StoreValue(string keyPressed){
		float variable = 0f;

		if(isAddition)
			variable = (float)secondValue;
		else
			variable = (float)firstValue;

		if(keyPressed == "A")
			varA = variable;
		else
			varB = variable;
	}





	private void GetValueOfInput(){
		if(!isAddition)
			firstValue = Single.Parse(displayText);
		else
			secondValue = Single.Parse(displayText);
	}

	public string GetDisplayText(){
		return displayText;
	}
}
