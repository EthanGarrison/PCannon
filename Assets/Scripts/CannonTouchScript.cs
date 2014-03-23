using UnityEngine;
using System.Collections;

public class CannonTouchScript : MonoBehaviour {

	private Ray touchInput;
	private RaycastHit2D cannonTouchTest;
	private string attachedName;
	public GameMaster GM;
	Animator anim;
	public Transform target;
	// Use this for initialization
	void Start () {
		//This is the name of the GameObject that this script is attached to.
		attachedName = gameObject.name;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Checks to see if the user has touched the screen, then preforms a raycast where the user touched
		if(Input.touchCount == 1){
			touchInput = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			cannonTouchTest = Physics2D.GetRayIntersection(touchInput, Mathf.Infinity);

			//If the hit object is the GameObject that this script is attached to and
			//the cannon hasn't been fired, then call AnimateCannon()
			if(cannonTouchTest.collider.name == attachedName && GM.HasValidInput() && !GameStat.gameStatDisplayUp){
					AnimateCannon();	
					GM.ExecuteHitResult();		
			}
			GM.ClearInput();
		}
	}

	//Calls the animation script for the cannon
	void AnimateCannon(){
		anim.SetTrigger("cannonFireTrigger");
	}
}
