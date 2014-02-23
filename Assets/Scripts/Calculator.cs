using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour {

	public GUIStyle calcButton, calcLabel, calcBackground;
	private string displayText = "0", operSymbol = "";
	private float varA = 0f, varB = 0f;
	private float? firstValue = null, secondValue = null, tempValue = null;
	private Rect button1, button2, button3, button4,calcWrapper;
	private int calcWidth, calcHeight, buttonWidth, buttonHeight, buttonSpacing;

	// Use this for initialization
	void Start () {
		//Initializing the variables
		buttonWidth = 32;
		buttonHeight = 24;
		buttonSpacing = 0;
		calcWidth = buttonWidth*4;
		calcHeight = 222;
		button1 = new Rect(0,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button2 = new Rect(buttonWidth+buttonSpacing,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button3 = new Rect((buttonWidth*2)+buttonSpacing,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button4 = new Rect((buttonWidth*3)+buttonSpacing,
							0,
							buttonWidth,
							buttonHeight
							);

		calcWrapper = new Rect(330,
								100,
								calcWidth,
								calcHeight
								);
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnGUI () {
		GUI.contentColor = Color.black;
		
		//The wrapper for the entire calculator
		GUI.BeginGroup(calcWrapper, "", calcBackground);
			
			//Output
			GUI.Label(new Rect(0,0,calcWidth,19), displayText, calcLabel);

			
			GUI.BeginGroup(new Rect(0,36,calcWidth,buttonHeight), "");
				
				//A variable.  Puts A into whatever value isn't null
				if (GUI.Button(button1, "A", calcButton)){
					if(firstValue != null){
						secondValue = varA;
						displayText = secondValue + "";
					}
					else{
						firstValue = varA;
						displayText = firstValue + "";
					}
				}
				
				//B variable.  Puts B into whatever value isn't null
				if (GUI.Button(button2, "B", calcButton)){
					if(firstValue != null){
						secondValue = varB;
						displayText = secondValue + "";
					}
					else{
						firstValue = varB;
						displayText = firstValue + "";
					}
				}
				
				//Subtraction
				if (GUI.Button(button3, "-", calcButton)){
					operSymbol = "-";
				}

				//Clear function.
				if (GUI.Button(button4, "Clr", calcButton))
					Calculate("clr");

			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,68,calcWidth,buttonHeight), "");
				
				//Stores A value.  If all values are null, throws an error.  Need to work on this.
				if (GUI.Button(button1, "Str A", calcButton)){
					if(firstValue != null){
						if(secondValue != null)
							varA = (float)secondValue;
						else
							varA = (float)firstValue;
					}
					else if(tempValue != null){
						varA = (float)tempValue;
					}
					else{
						displayText = "ERROR";
					}
				}
				
				//Stores B value.  If all values are null, throws an error.  Need to work on this.
				if (GUI.Button(button2, "Str B", calcButton)){
					if(firstValue != null){
						if(secondValue != null)
							varB = (float)secondValue;
						else
							varB = (float)firstValue;
					}
					else if(tempValue != null){
						varB = (float)tempValue;
					}
					else{
						displayText = "ERROR";
					}
				}

				//Multiplication
				if (GUI.Button(button3, "*", calcButton))
					operSymbol = "*";

				//Division
				if (GUI.Button(button4, "/", calcButton))
					operSymbol = "/";

			GUI.EndGroup();

			
			GUI.BeginGroup(new Rect(0,100,calcWidth,buttonHeight), "");
				
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

			
			GUI.BeginGroup(new Rect(0,133,calcWidth,buttonHeight), "");
				
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

			
			GUI.BeginGroup(new Rect(0,166,calcWidth,buttonHeight), "");
			
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

			
			GUI.BeginGroup(new Rect(0,199,calcWidth,buttonHeight), "");
			
				//0
				if (GUI.Button(new Rect(0,0,buttonWidth*2,buttonHeight), "0", calcButton)){
					InputValue(0);
				}
			
				//Equals function.  Sets the values to null.
				if (GUI.Button(button3, "=", calcButton)){
				
					Calculate((float)secondValue, operSymbol);
					if(!operSymbol.Equals("")){
						firstValue = null;
						secondValue = null;
						operSymbol = "";
					}
				}
					
			GUI.EndGroup();
			
			//Addition function.  Doesn't have a group because of its awkward size.
			if (GUI.Button(new Rect(buttonWidth*3,166,buttonWidth,60), "+", calcButton)){
				operSymbol = "+";
			}
		//End of the calculator wrapper.
		GUI.EndGroup();
	}

	//Function for two number functions.
	void Calculate(float number, string operSym) {
		bool error = false;
		switch (operSym){
			case "+":
				firstValue += number;
				break;
			case "-":
				firstValue -= number;
				break;
			case "*":
				firstValue *= number;
				break;
			case "/":
				if(number != 0)
					firstValue /= number;
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
		else
			displayText = firstValue + "";
			tempValue = firstValue;						//so that the store variable button works and I don't have to change how the input function works
	}

	//Function for single number functions and the clear function.
	void Calculate(string operSym) {
		bool error = false;
		switch (operSym){
			case "square":
				firstValue = Mathf.Pow((float)firstValue,2f);
				break;
			case "squareRoot":
				firstValue = Mathf.Pow((float)firstValue,0.5f);
				break;
			case "clr":
				firstValue = null;
				secondValue = null;
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
		else
			displayText = firstValue + "";
			tempValue = firstValue;						//so that the store variable button works and I don't have to change how the input function works
	}

	//Value input function.  Checks to see which value we are working with and then if it already has a value
	//If it has a value, we append the entered number onto the end of the number.
	void InputValue(int val){
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
	}
}