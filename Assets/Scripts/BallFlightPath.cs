using UnityEngine;
using System.Collections;

public class BallFlightPath : MonoBehaviour {

	Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		//AnimatorStateInfo shortFire = Animat
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){	
			anim.SetTrigger("Short");
		}

	}


}
