#pragma strict

var mainCam : Camera;
var firebutton : GUI;
var topWall : BoxCollider2D;
var bottomWall : BoxCollider2D;
var leftWall : BoxCollider2D;
var rightWall : BoxCollider2D;
var barrel : GameObject;
var userInput : String;
var userAnswer : float;
var correctAnswer : float;
var sideA : float;
var sideB : float;

//creates an Animator
var animCannonFire : Animator ;
//public Texture button;
var  buttonStyle : GUIStyle;

//Allows custom ball position
var  ball : Rigidbody2D;
var  ballPositionX : float;
var  ballPositionY : float;
var  ballVelocity : float;
var  ballAngle : float;

//Get position
var pos : Vector3;

function Start () {
	//gets the Animator Component
	animCannonFire = GetComponent(Animator);
	//gets position of screen
	pos = mainCam.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0f));
	//Move each wall to its edge location
	topWall.size = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f,0f,0f)).x,1f);
	topWall.center = new Vector2 (0f,mainCam.ScreenToWorldPoint(new Vector3(0f,Screen.height,0f)).y + 0.5);
	
	bottomWall.size = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f,0f,0f)).x,1f);
	bottomWall.center = new Vector2 (0f,mainCam.ScreenToWorldPoint(new Vector3(0f,0f,0f)).y - 0.5);
	
	leftWall.size = new Vector2 (1f,mainCam.ScreenToWorldPoint(new Vector3(0f,Screen.height * 2f,0f)).y);
	leftWall.center = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(0f,0f,0f)).x - 0.5, 0f);
	
	rightWall.size = new Vector2 (1f,mainCam.ScreenToWorldPoint(new Vector3(0f,Screen.height * 2f,0f)).y);
	rightWall.center = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f)).x + 0.5, 0f);

	generateNumbers();
}

function Update(){
	//the distance between cannon and projectile
	if (Mathf.Abs(-pos.x- ball.transform.position.x) > pos.x){
		resetBall();
	}
}

//places FireButton 1/3 of the screen
function OnGUI () {	
	firebutton.contentColor = Color.white;
	if(firebutton.Button(new Rect(Screen.width/3-33, Screen.height-22-10, 67,22),"",buttonStyle)){
		resetBall();
		fireBall();
	}

	userInput = GUI.TextField (Rect (300, 10, 100, 20), userInput);
	GUI.Label(Rect(300, 30, 100, 20), sideA.ToString());
	GUI.Label(Rect(300,50,100,20), sideB.ToString());



}

//reloads the cannon
function resetBall(){
	ball.velocity.x = 0;
	ball.velocity.y = 0;
	
	ball.transform.position.x = ballPositionX;
	ball.transform.position.y = ballPositionY;
}

//Animages the cannonBall and gives it velocity
function fireBall(){
	userAnswer = parseFloat(userInput);

	if(userAnswer < correctAnswer){
		//code for if it is less than the correct answer
		//default code for now
		
	}
	else if(userAnswer > correctAnswer){
		//code for if it is less than the correct answer
		//default code for now
		
	}
	else{
		//code for if it is the correct answer
		//default code for now
		animCannonFire.SetTrigger("cannonFireTrigger");
		ball.AddForce (new Vector2 (ballVelocity,ballAngle));
	}
}

function generateNumbers(){
		
	sideA = Mathf.Ceil(Random.value*100);

	sideB = Mathf.Ceil(Random.value*100);

	correctAnswer = Mathf.Floor(Mathf.Sqrt(Mathf.Pow(sideA,2) + Mathf.Pow(sideB,2)));
}

