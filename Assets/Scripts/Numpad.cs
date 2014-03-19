using UnityEngine;
using System.Collections;

public class Numpad : MonoBehaviour {

	private Rect numpadBg, group1, group2, group3, group4;
	private Rect button1, button2, button3, helpButton;
	public GameObject Calculator;
	private string displayText = "";
	private float? inputValue = null;
	private float counterDecimal = 0f;
	private bool isDecimal = false;
	public GameMaster GM;

	// Use this for initialization
	void Start () {
		numpadBg = new Rect(	GM.NativeWidth/4f,
								0,
								(GM.NativeWidth*(3f/4f)),
								GM.NativeHeight
							);
		helpButton = new Rect(	numpadBg.width*(10f/13),
								numpadBg.height*(1f/13),
								numpadBg.width*(2f/13),
								numpadBg.height*(11f/13)
							);
		group1 = createRRect(1f);
		group2 = createRRect(4f);
		group3 = createRRect(7f);
		group4 = createRRect(10f);
		button1 = createBRect(1f);
		button2 = createBRect(4f);
		button3 = createBRect(7f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GM.AutoResize(GM.NativeWidth, GM.NativeHeight);
		GUI.depth = 0;

		GUI.BeginGroup(numpadBg);

			GUI.BeginGroup(group1);
			
				if(GUI.Button(button1, "7"))
					Input(7);

				if(GUI.Button(button2, "8"))
					Input(8);

				if(GUI.Button(button3, "9"))
					Input(9);

			GUI.EndGroup();

			GUI.BeginGroup(group2);
			
				if(GUI.Button(button1, "4"))
					Input(4);

				if(GUI.Button(button2, "5"))
					Input(5);

				if(GUI.Button(button3, "6"))
					Input(6);

			GUI.EndGroup();

			GUI.BeginGroup(group3);
			
				if(GUI.Button(button1, "1"))
					Input(1);

				if(GUI.Button(button2, "2"))
					Input(2);

				if(GUI.Button(button3, "3"))
					Input(3);

			GUI.EndGroup();

			GUI.BeginGroup(group4);
			
				if(GUI.Button(button1, "0"))
					Input(0);

				if(GUI.Button(button2, "."))
					isDecimal = true;

				if(GUI.Button(button3, "Clr"))
					Clear();

			GUI.EndGroup();

			if(GUI.Button(helpButton, "Need Help?")){
				DisplayCalculator();
				GM.needHelpActive = true;
			}

		//End Main Group
		GUI.EndGroup();
	}

	public void Clear(){
		inputValue = null;
		isDecimal = false;
		displayText = "";
		counterDecimal = 0;
	}

	//value should never be above one digit
	private void Input(int value){
		if(!isDecimal){
			if(inputValue != null){
				inputValue = (inputValue*10f)+value;
				displayText = inputValue + "";
			}
			else{
				inputValue = value;
				displayText = inputValue + "";
			}
		}
		else{
			counterDecimal++;
			if(inputValue != null){
				inputValue += (value/(Mathf.Pow(10f, counterDecimal)));
				displayText = inputValue + "";
			}
			else{
				inputValue = value/10f;
				displayText = inputValue + "";
			}
		}
	}

	private Rect createRRect(float y){
		return new Rect(numpadBg.width*(1f/13),numpadBg.height*(y/13),numpadBg.width*(9f/13),numpadBg.height*(3f/13));
	}

	private Rect createBRect(float x){
		return new Rect(numpadBg.width*(x/13),0,numpadBg.width*(2f/13),numpadBg.width*(2f/13));
	}

	private void DisplayCalculator(){
		Calculator.SetActive(true);
		gameObject.SetActive(false);
	}

	public string GetDisplayText(){
		return displayText;
	}
}
