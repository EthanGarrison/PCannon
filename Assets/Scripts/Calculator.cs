using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour {

	public bool isCalculated = false;
	public bool nullFirst, nullSecond;
	public GUIStyle calcButton, calcLabel, calcBackground;
	public string displayText = "0", operSymbol = "";
	private float varA = 0f, varB = 0f;
	private float? firstValue = null, secondValue = null;
	private Rect button1, button2, button3, button4, calcWrapper;
	public int calcWidth, calcHeight, buttonWidth, buttonHeight, buttonSpacingX, buttonSpacingY;

	// Use this for initialization
	void Start () {
		//Initializing the variables
		buttonWidth = 32;
		buttonHeight = 48;
		buttonSpacingX = 0;
		buttonSpacingY = 0;
		calcWidth = ((buttonWidth+buttonSpacingX)*4);
		calcHeight = ((buttonHeight+buttonSpacingY)*7+19);

		button1 = new Rect(0,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button2 = new Rect(buttonWidth+buttonSpacingX,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button3 = new Rect((buttonWidth*2)+buttonSpacingX,
							0,
							buttonWidth,
							buttonHeight
							);
		
		button4 = new Rect((buttonWidth*3)+buttonSpacingX,
							0,
							buttonWidth,
							buttonHeight
							);

		calcWrapper = new Rect(0,
								0,
								calcWidth,
								calcHeight
								);
	}
	
	// Update is called once per frame
	void Update () {
		if(firstValue == null)
			nullFirst=true;
		else
			nullFirst=false;
		if(secondValue == null)
			nullSecond=true;
		else
			nullSecond=false;
	}

	void OnGUI () {
		GUI.contentColor = Color.black;
		
		//The wrapper for the entire calculator
		GUI.BeginGroup(calcWrapper, "", calcBackground);
			
			//Output
			GUI.Label(new Rect(0,0,calcWidth,19), displayText, calcLabel);

			
			GUI.BeginGroup(new Rect(0,(buttonHeight+19),calcWidth,buttonHeight+buttonSpacingY), "");
				
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

			
			GUI.BeginGroup(new Rect(0,((buttonHeight+buttonSpacingY)*2)+19,calcWidth,buttonHeight+buttonSpacingY), "");
				
				//Stores A value.  If all values are null, throws an error.  Need to work on this.
				if (GUI.Button(button1, "Str A", calcButton)){
					if(firstValue != null){
						if(secondValue != null)
							varA = (float)secondValue;
						else
							varA = (float)firstValue;
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

			
			GUI.BeginGroup(new Rect(0,((buttonHeight+buttonSpacingY)*3)+19,calcWidth,buttonHeight+buttonSpacingY), "");
				
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

			
			GUI.BeginGroup(new Rect(0,((buttonHeight+buttonSpacingY)*4)+19,calcWidth,buttonHeight+buttonSpacingY), "");
				
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

			
			GUI.BeginGroup(new Rect(0,((buttonHeight+buttonSpacingY)*5)+19,calcWidth,buttonHeight+buttonSpacingY), "");
			
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

			
			GUI.BeginGroup(new Rect(0,((buttonHeight+buttonSpacingY)*6)+19,calcWidth,buttonHeight+buttonSpacingY), "");
			
				//0
				if (GUI.Button(new Rect(0,0,buttonWidth*2,buttonHeight), "0", calcButton)){
					InputValue(0);
				}
			
				//Equals function.  Sets the values to null.
				if (GUI.Button(button3, "=", calcButton)){
				
					Calculate((float)secondValue, operSymbol);
					if(!operSymbol.Equals("")){
						secondValue = null;
						operSymbol = "";
					}
				}
					
			GUI.EndGroup();
			
			//Addition function.  Doesn't have a group because of its awkward size.
			if (GUI.Button(new Rect(buttonWidth*3,((buttonHeight+buttonSpacingY)*5)+19,buttonWidth,60), "+", calcButton)){
				operSymbol = "+";
			}
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
}