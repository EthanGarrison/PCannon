using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour {

	public string displayText = "0", operSymbol = "";
	public float varA = 0f, varB = 0f;
	public float? firstValue = null, secondValue = null;
	public GUIStyle calcButton;
	public int x1, y1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnGUI () {
		GUI.contentColor = Color.black;
		

		GUI.BeginGroup(new Rect(x1,y1,182,222), "", calcButton);
		
			GUI.Label(new Rect(0,0,182,19), displayText);

			GUI.BeginGroup(new Rect(0,36,154,24), "");
				//A
				if (GUI.Button(new Rect(0,0,32,24), "")){
					if(firstValue != null){
						secondValue = varA;
						displayText = secondValue + "";
					}
					else{
						firstValue = varA;
						displayText = firstValue + "";
					}
				}
				//B
				if (GUI.Button(new Rect(40,0,32,24), "")){
					if(firstValue != null){
						secondValue = varB;
						displayText = secondValue + "";
					}
					else{
						firstValue = varB;
						displayText = firstValue + "";
					}
				}
				//Blank
				if (GUI.Button(new Rect(80,0,33,24), "-")){
					operSymbol = "-";
				}
				//Clr
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Calculate("clr");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,68,154,24), "");
				//Store A
				if (GUI.Button(new Rect(0,0,32,24), "")){
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
				//Store B
				if (GUI.Button(new Rect(40,0,32,24), "")){
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
				//Blank
				if (GUI.Button(new Rect(80,0,33,24), "*"))
					operSymbol = "*";
				//Blank
				if (GUI.Button(new Rect(120,0,34,24), "/"))
					operSymbol = "/";
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,100,154,24), "");
				//7
				if (GUI.Button(new Rect(0,0,32,24), "")){
					InputValue(7);
				}
				//8
				if (GUI.Button(new Rect(40,0,32,24), "")){
					InputValue(8);
				}
				//9
				if (GUI.Button(new Rect(80,0,33,24), "")){
					InputValue(9);
				}
				//Square Root
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Calculate("squareRoot");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,133,154,24), "");
				//4
				if (GUI.Button(new Rect(0,0,32,24), "")){
					InputValue(4);
				}
				//5
				if (GUI.Button(new Rect(40,0,32,24), "")){
					InputValue(5);
				}
				//6
				if (GUI.Button(new Rect(80,0,33,24), "")){
					InputValue(6);
				}
				//Square
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Calculate("square");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,166,113,24), "");
				//1
				if (GUI.Button(new Rect(0,0,32,24), "")){
					InputValue(1);
				}
				//2
				if (GUI.Button(new Rect(40,0,33,24), "")){
					InputValue(2);
				}
				//3
				if (GUI.Button(new Rect(80,0,33,24), "")){
					InputValue(3);
				}
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,199,113,24), "");
				//0
				if (GUI.Button(new Rect(0,0,75,24), "")){
					InputValue(0);
				}
				//Equals
				if (GUI.Button(new Rect(80,0,33,24), "")){
				
					Calculate((float)secondValue, operSymbol);
					if(!operSymbol.Equals("")){
						firstValue = null;
						secondValue = null;
						operSymbol = "";
					}
				}
					
			GUI.EndGroup();
			//Plus
			if (GUI.Button(new Rect(120,166,33,60), "")){
				operSymbol = "+";
			}
		GUI.EndGroup();
	}

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

		if(error){
			displayText = "ERROR";
			error = false;
		}
		else
			displayText = firstValue + "";
	}

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

		if(error){
			displayText = "ERROR";
			error = false;
		}
		else
			displayText = firstValue + "";
	}

	void InputValue(int val){
		if(firstValue != null){
			if(!operSymbol.Equals("")){
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