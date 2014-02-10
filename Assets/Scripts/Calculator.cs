using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour {

	public int x1, y1, x2, y2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnGUI () {
		GUI.contentColor = Color.black;
		

		GUI.BeginGroup(new Rect(27,23,182,222), "");
		
			GUI.Label(new Rect(0,0,182,19), "Hello");

			GUI.BeginGroup(new Rect(0,36,154,24), "");
				//A
				if (GUI.Button(new Rect(0,0,32,24), ""))
					Debug.Log("A");
				//B
				if (GUI.Button(new Rect(40,0,32,24), ""))
					Debug.Log("B");
				//Blank
				if (GUI.Button(new Rect(80,0,33,24), ""))
					Debug.Log("Blank");
				//Clr
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Debug.Log("Clr");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,68,154,24), "");
				//Store A
				if (GUI.Button(new Rect(0,0,32,24), ""))
					Debug.Log("Store A");
				//Store B
				if (GUI.Button(new Rect(40,0,32,24), ""))
					Debug.Log("Store B");
				//Blank
				if (GUI.Button(new Rect(80,0,33,24), ""))
					Debug.Log("Blank");
				//Blank
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Debug.Log("Blank");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,100,154,24), "");
				//7
				if (GUI.Button(new Rect(0,0,32,24), ""))
					Debug.Log("7");
				//8
				if (GUI.Button(new Rect(40,0,32,24), ""))
					Debug.Log("8");
				//9
				if (GUI.Button(new Rect(80,0,33,24), ""))
					Debug.Log("9");
				//Square Root
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Debug.Log("Square Root");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,133,154,24), "");
				//4
				if (GUI.Button(new Rect(0,0,32,24), ""))
					Debug.Log("4");
				//5
				if (GUI.Button(new Rect(40,0,32,24), ""))
					Debug.Log("5");
				//6
				if (GUI.Button(new Rect(80,0,33,24), ""))
					Debug.Log("6");
				//Square
				if (GUI.Button(new Rect(120,0,34,24), ""))
					Debug.Log("Square");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,166,113,24), "");
				//1
				if (GUI.Button(new Rect(0,0,32,24), ""))
					Debug.Log("1");
				//2
				if (GUI.Button(new Rect(40,0,33,24), ""))
					Debug.Log("2");
				//3
				if (GUI.Button(new Rect(80,0,33,24), ""))
					Debug.Log("3");
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,199,113,24), "");
				//0
				if (GUI.Button(new Rect(0,0,75,24), ""))
					Debug.Log("0");
				//Equals
				if (GUI.Button(new Rect(80,0,33,24), ""))
					Debug.Log("Equals");
			GUI.EndGroup();
			//Plus
			if (GUI.Button(new Rect(120,166,33,60), ""))
				Debug.Log("Plus");
		GUI.EndGroup();
	}

	void Calculate() {
		
	}
}
