using UnityEngine;
using System.Collections;

public class BallFlightPath : MonoBehaviour {

	Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlayShort(){		
		anim.SetTrigger("Short");
	}
	public void PlayLong(){		
		anim.SetTrigger("Long");
	}
	public void PlayExact(){		
		anim.SetTrigger("Exact");
	}



}
