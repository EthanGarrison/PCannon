using UnityEngine;
using System.Collections;

public class CannonTouchScript : MonoBehaviour {

	Ray touchInput;
	RaycastHit2D cannonTouchTest;
	bool isFired;
	string attachedName;


	// Use this for initialization
	void Start () {
		//This is the name of the GameObject that this script is attached to.
		attachedName = gameObject.transform.name;
	}
	
	// Update is called once per frame
	void Update () {

		//Checks to see if the user has touched the screen, then preforms a raycast where the user touched
		if(Input.touchCount == 1){
			touchInput = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			cannonTouchTest = Physics2D.GetRayIntersection(touchInput, Mathf.Infinity);
		}

		//If the hit object is the GameObject that this script is attached to and
		//the cannon hasn't been fired, then call AnimateCannon() and FireBall()
		if(cannonTouchTest.collider.name == attachedName && !isFired){
			AnimateCannon();
			FireBall();
			isFired = true;
		}

		//Displays the results of the hit after the cannon fired
		if(isFired){
			ExecuteHitResults();
			isFired = false;
		}
	}

	//Calls the animation script for the cannon
	void AnimateCannon(){
		print("This is the AnimateCannon() function");
	}

	//Stand in function for the FireBall() of the GM
	void FireBall(){
		print("This is the FireBall() function");
	}

	//Stand in function for the ExecuteHitResults() of the GM
	void ExecuteHitResults(){
		print("This is the FireBall() function");
	}
}
