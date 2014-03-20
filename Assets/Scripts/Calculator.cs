using UnityEngine;
using System.Collections;
using System;

public class Calculator : MonoBehaviour {

	//These are necessary for the calculator funtionality
	private bool isDecimal = false,  isCalculated = false, isAddition = false;
	private string displayText = "";
	private float varA = 0f, varB = 0f;
	private float? firstValue = null, secondValue = null;
	public GameMaster GM;

	//These are the things for the layout
	public GUIStyle calcButton, calcLabel, calcBackground;
	private Rect button1, button2, button3, button4, button5, labelRect;
	private Rect labelGroup, group1, group2, group3, group4, sendButton;
	private Rect bgCalc;

	void Start () {
		//Initializing the variables
		bgCalc = new Rect(	GameMaster.nativeWidth/4,0,(GameMaster.nativeWidth*(3f/4f)),GameMaster.nativeHeight);
		labelRect = new Rect(bgCalc.width*(6f/16),0,bgCalc.width*(8f/16),bgCalc.height*(3f/17));
		sendButton = new Rect(	0,0,bgCalc.width*(5f/16),bgCalc.height*(2.5f/16));

		labelGroup = createRRect(1.5f,3f);
		group1 = createRRect(5f, 2f);
		group2 = createRRect(7.5f, 2f);
		group3 = createRRect(10f, 2f);
		group4 = createRRect(12.5f, 2f);
		button1 = createBRect(0);
		button2 = createBRect(3f);
		button3 = createBRect(6f);
		button4 = createBRect(9f);
		button5 = createBRect(12f);
	}

	void Update(){
		Debug.Log("First Value: " + firstValue);
		Debug.Log("Second Value: " + secondValue);
	}
	
	void OnGUI () {
		GameMaster.AutoResize(GameMaster.nativeWidth,GameMaster.nativeHeight);

		GUI.BeginGroup(bgCalc, calcBackground);

			//Label and Sendto button
			GUI.BeginGroup(labelGroup);

			GUI.Button(sendButton, "Sendto", calcButton);

			GUI.Label(labelRect, displayText, calcLabel);

			//Label and Sendto button
			GUI.EndGroup();

			GUI.BeginGroup(group1);

			if(GUI.Button(button1, "A", calcButton))
				DisplayStoredValue("A");

			if(GUI.Button(button2, "7", calcButton))
				InputValue("7");

			if(GUI.Button(button3, "8", calcButton))
				InputValue("8");

			if(GUI.Button(button4, "9", calcButton))
				InputValue("9");

			if(GUI.Button(button5, "Clr", calcButton))
				Clear();

			GUI.EndGroup();

			GUI.BeginGroup(group2);

			if(GUI.Button(button1, "StrA", calcButton))
				StoreValue("A");

			if(GUI.Button(button2, "4", calcButton))
				InputValue("4");

			if(GUI.Button(button3, "5", calcButton))
				InputValue("5");

			if(GUI.Button(button4, "6", calcButton))
				InputValue("6");

			if(GUI.Button(button5, "√", calcButton))
				Calculate(null,"squareRoot");

			GUI.EndGroup();

			GUI.BeginGroup(group3);

			if(GUI.Button(button1, "B", calcButton))
				DisplayStoredValue("B");

			if(GUI.Button(button2, "1", calcButton))
				InputValue("1");

			if(GUI.Button(button3, "2", calcButton))
				InputValue("2");

			if(GUI.Button(button4, "3", calcButton))
				InputValue("3");

			if(GUI.Button(button5, "X²", calcButton))
				Calculate(null,"square");

			GUI.EndGroup();

			GUI.BeginGroup(group4);

			if(GUI.Button(button1, "StrB", calcButton))
				StoreValue("B");

			if(GUI.Button(button2, "0", calcButton))
				InputValue("0");

			if(GUI.Button(button3, ".", calcButton)){
				InputValue(".");
				isDecimal = true;
			}

			if(GUI.Button(button4, "=", calcButton)){
				try{
					Calculate((float)secondValue, "+");
				}
				catch(Exception e){
					Debug.Log("This is from the equals button: " + e);
				}
			}

			if(GUI.Button(button5, "+", calcButton))
				InputValue("+");

			GUI.EndGroup();

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
		if(isCalculated && isAddition)
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

	private Rect createRRect(float y, float height){
		return new Rect((bgCalc.width)*(1f/16),(bgCalc.height)*(y/17),(bgCalc.width)*(14f/16),(bgCalc.height)*(height/17));
	}

	private Rect createBRect(float x){
		return new Rect(bgCalc.width*(x/16),0,(bgCalc.width*(2f/16)),bgCalc.height*(2f/17));
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
