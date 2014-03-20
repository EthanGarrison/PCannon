using UnityEngine;
using System.Collections;

public class BallFlightPath : MonoBehaviour {
	Animator anim;
	
	void Start () {
		anim = GetComponent<Animator>();
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
