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
		if(Input.GetKeyDown(KeyCode.Space)){	
			PlayShort();
		}
		if(Input.GetKeyDown(KeyCode.L)){	
			PlayLong();
		}
		if(Input.GetKeyDown(KeyCode.E)){	
			PlayExact();
		}
	}

	void PlayShort(){		
		anim.SetTrigger("Short");
	}
	void PlayLong(){		
		anim.SetTrigger("Long");
	}
	void PlayExact(){		
		anim.SetTrigger("Exact");
	}



}
