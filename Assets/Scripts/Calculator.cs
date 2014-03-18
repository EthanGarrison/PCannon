using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour {

	// public bool nullFirst, nullSecond;
	private bool isCalculated = false;
	public GUIStyle calcButton, calcLabel, calcBackground;
	private string displayText = "0", operSymbol = "";
	private float varA = 0f, varB = 0f;
	private float? firstValue = null, secondValue = null;
	private Rect button1, button2, button3, button4,calcWrapper;
	public int calcWidth, calcHeight, buttonWidth, buttonHeight;
	public GameMaster GM;
	public int x=0, y=0;

	void Start () {
		//Initializing the variables
		

		
	}
	
	// Update is called once per frame
	void Update () {
		// if(firstValue == null)
		// 	nullFirst = true;
		// else
		// 	nullFirst = false;
		// if(secondValue == null)
		// 	nullSecond = true;
		// else
		// 	nullSecond = false;
		calcWrapper = new Rect(GM.NativeWidth-200,
								GM.NativeHeight-190,
								calcWidth,
								calcHeight
								);
		buttonWidth = x;
		buttonHeight = y;
		calcWidth = (buttonWidth)*4;
		calcHeight = (buttonHeight*6)+19;
		button1 = new Rect(0,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button2 = new Rect(buttonWidth,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button3 = new Rect((buttonWidth*2),
							0,
							buttonWidth,
							buttonHeight
							);
		
		button4 = new Rect((buttonWidth*3),
							0,
							buttonWidth,
							buttonHeight
							);
			
	}

	void OnGUI () {
		Debug.Log(GM.NativeWidth);
		Debug.Log(GM.NativeHeight);
		GM.AutoResize(GM.NativeWidth, GM.NativeHeight);

		GUI.Label(new Rect(0,0,100,10), "This is a test");
		
		//The wrapper for the entire calculator
		GUI.BeginGroup(calcWrapper, "", calcBackground);
			
			//Output
			GUI.Label(new Rect(0,0,calcWidth,19), displayText, calcLabel);

			
			GUI.BeginGroup(new Rect(0,buttonHeight,calcWidth,buttonHeight), "");
				
				//A variable.  Puts A into whatever value isn't null
				if (GUI.Button(button1, "A", calcButton)){
					DisplayStoredValue("A");
				}
				
				//B variable.  Puts B into whatever value isn't null
				if (GUI.Button(button2, "B", calcButton)){
					DisplayStoredValue("B");
				}
				
				//Subtraction
				if (GUI.Button(button3, "-", calcButton)){
					operSymbol = "-";
				}

				//Clear function.
				if (GUI.Button(button4, "Clr", calcButton))
					Calculate("clr");

			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,buttonHeight*2,calcWidth,buttonHeight), "");
				
				//Stores A value.  If all values are null, throws an error.  Need to work on this.
				if (GUI.Button(button1, "Str A", calcButton)){
					StoreValue("A");
				}
				
				//Stores B value.  If all values are null, throws an error.  Need to work on this.
				if (GUI.Button(button2, "Str B", calcButton)){
					StoreValue("B");
				}

				//Multiplication
				if (GUI.Button(button3, "*", calcButton))
					operSymbol = "*";

				//Division
				if (GUI.Button(button4, "/", calcButton))
					operSymbol = "/";

			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,buttonHeight*3,calcWidth,buttonHeight), "");
				
				//7
				if (GUI.Button(button1, "7", calcButton)){
					InputValue(7);
				}

				//8
				if (GUI.Button(button2, "8", calcButton)){
					InputValue(8);
				}

				//9
				if (GUI.Button(button3, "9", calcButton)){
					InputValue(9);
				}

				//Square Root function
				if (GUI.Button(button4, "√", calcButton))
					Calculate("squareRoot");

			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,buttonHeight*4,calcWidth,buttonHeight), "");
				
				//4
				if (GUI.Button(button1, "4", calcButton)){
					InputValue(4);
				}
				
				//5
				if (GUI.Button(button2, "5", calcButton)){
					InputValue(5);
				}
				
				//6
				if (GUI.Button(button3, "6", calcButton)){
					InputValue(6);
				}
				
				//Square
				if (GUI.Button(button4, "X²", calcButton))
					Calculate("square");

			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,buttonHeight*5,calcWidth,buttonHeight), "");
			
				//1
				if (GUI.Button(button1, "1", calcButton)){
					InputValue(1);
				}
			
				//2
				if (GUI.Button(button2, "2", calcButton)){
					InputValue(2);
				}
			
				//3
				if (GUI.Button(button3, "3", calcButton)){
					InputValue(3);
				}
			
			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,buttonHeight*6,calcWidth,buttonHeight), "");
			
				//0
				if (GUI.Button(button1, "0", calcButton)){
					InputValue(0);
				}
			
				//Equals function.  Sets the values to null.
				if (GUI.Button(button2, "=", calcButton)){
				
					Calculate((float)secondValue, operSymbol);
					if(!operSymbol.Equals("")){
						secondValue = null;
						operSymbol = "";
					}
				}

				if (GUI.Button(button3, "+", calcButton)){
					operSymbol = "+";
				}
					
			GUI.EndGroup();

		//End of the calculator wrapper.
		GUI.EndGroup();
	}

	//Function for two number functions.
	void Calculate(float number, string operSym) {
		bool error = false;
		float value = (float)firstValue;

		switch (operSym){
			case "+":
				value += number;
				break;
			case "-":
				value -= number;
				break;
			case "*":
				value *= number;
				break;
			case "/":
				if(number != 0)
					value /= number;
				else
					error = true;
				break;
			case "":
				break;
			default:
				error = true;
				break;
		}

		//In case something breaks
		if(error){
			displayText = "ERROR";
			error = false;
		}
		else{
			displayText = value + "";
			firstValue = value;
			isCalculated = true;
		}
	}

	//Function for single number functions and the clear function.
	void Calculate(string operSym) {
		bool error = false;
		float? value = firstValue;

		switch (operSym){
			case "square":
				value = Mathf.Pow((float)value,2f);
				break;
			case "squareRoot":
				value = Mathf.Pow((float)value,0.5f);
				break;
			case "clr":
				firstValue = null;
				secondValue = null;
				value = null; 						
				break;
			case "":
				break;
			default:
				error = true;
				break;
		}

		//In case something breaks
		if(error){
			displayText = "ERROR";
			error = false;
		}
		else{
			displayText = value + "";
			firstValue = value;
			operSymbol = "";
			if(!operSym.Equals("clr"))
				isCalculated = true;
			else
				isCalculated = false;
		}
	}

	//Value input function.  Checks to see which value we are working with and then if it already has a value
	//If it has a value, we append the entered number onto the end of the number.
	void InputValue(int val){
		if(isCalculated && operSymbol.Equals("")){
			firstValue = null;
		}
		if(firstValue != null){
			if(!operSymbol.Equals("")){					//If the operSymbol is not empty, then we have started inputing a second value
				if(secondValue != null){
					secondValue = (secondValue*10)+val;
					displayText = secondValue + "";
				}
				else{
					secondValue = (float)val;
					displayText = secondValue + "";
				}
			}
			else{
				firstValue = (firstValue*10)+val;
				displayText = firstValue + "";
			}
		}
		else{
			firstValue = (float)val;
			displayText = firstValue + "";
		}
		isCalculated = false;		
	}

	void DisplayStoredValue(string keyPressed){
		
		float variable;

		if(keyPressed == "A")
			variable = varA;
		else
			variable = varB;
		
		if(firstValue != null && operSymbol != ""){
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

		if(firstValue != null){
			if(secondValue != null)
				variable = (float)secondValue;
			else
				variable = (float)firstValue;
		}
		else
			displayText = "ERROR";
		if(keyPressed == "A")
			varA = variable;
		else
			varB = variable;
	}
}