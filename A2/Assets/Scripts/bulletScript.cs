using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bulletScript : MonoBehaviour {

	//Variables
	private Vector3 shootDirection;
	Vector3 velocity;

	//Static because unity doesn't like to share... :(
	public static List<GameObject>[] mountainBlocksList;
	public static List<Vector3>[] thePointList;

	GameObject[] mtnList;

	public float windPower;

	bool addingForce;
	bool hit;
	
	int restCounter;

	float startTime;
	float gravity = 0.5f;
	float shootPow;

	// Use this for initialization
	void Start () {
		//Getting all of the mountains
		mtnList = GameObject.FindGameObjectsWithTag("MountainParent");

		//Instantializing the two static lists
		mountainBlocksList = new List<GameObject>[mtnList.Length];
		thePointList = new List<Vector3>[mtnList.Length];

		//Updating the two static lists
		for (int i = 0; i < mtnList.Length; i++)
		{
			mtnList[i].SendMessage("gimmieYourList", i);
		}

		//initializing schtuff
		restCounter = 0;
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		startTime = Time.time;
		addingForce = true;
		hit = false;
		windPower = 1.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Checking for collision
		for (int i = 0; i < mountainBlocksList.Length; i++)
		{
			//Do a foreach because it looks nicer
			//but still need a counter so make a j to count with.
			int j = 0;
			foreach (GameObject mtnLine in mountainBlocksList[i])
			{
				//if collided (just doing radius doesn't look as nice, so divided by 4 instead of 2
				if ((transform.position.x - mtnLine.transform.position.x) > (transform.localScale.x / 4) &&
				    (transform.position.y - mtnLine.transform.position.y) < (transform.localScale.y / 4) &&
				    (transform.position.x - mtnLine.transform.position.x) < (transform.localScale.x * 5)) //last one so that we don't slip through the cracks
				{

					//WE HAVE A COLLISION!
					//Stop the ball.
					velocity = new Vector3(0, 0, 0);

					//if negative slope, then move to the left:
					if (thePointList[i][j].y < thePointList[i][j+1].y)
					{
						transform.position = new Vector3(transform.position.x - 0.07f, mtnLine.transform.position.y + transform.localScale.y / 4, 0);
						restCounter = 0;
					}
					//else if (positive enough slope move to the right)
					else if (thePointList[i][j].y > thePointList[i][j+1].y + 0.3f && transform.position.y > mtnLine.transform.position.y 
					         && transform.position.x < mtnLine.transform.position.x)
					{
						transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y + gravity * Time.deltaTime, 0);
						restCounter = 0;
					}
					//Stop.... everybody clap your hands
					else
					{
						transform.position = new Vector3(transform.position.x, transform.position.y , 0);
						restCounter++;
					}

					//if it hasn't been hit yet damage the motha'
					if (!hit)
						mtnList[i].SendMessage("updatePoints", j); //i is the mountain side that we're dealing with. j is the mtnBlock that was hit.
					//The bullet has hit the mountain!
					hit = true;
				}
				j++;
			}
		}

		//if we've been at rest for 30 or more frames gtfo
		if (restCounter >= 30)
		{
			Destroy(gameObject);
		}

		//if we go low enough gtfo
		if (transform.position.y <= -3.0f)
		{
			Destroy(gameObject);
		}

		//Simulated wind resistance... after enough time we stop adding forward momentum
		if (Time.time - startTime > 0.3f && addingForce)
		{
			addingForce = false;
		}

		//make a pretty arc!
		if (addingForce)
		{
			velocity += (new Vector3(shootDirection.x * Time.deltaTime * shootPow + windPower * 0.01f, shootDirection.y * Time.deltaTime * shootPow, 0));
		}

		//Woop there goes the gravity (gravity and wind only effect if we're not resting
		if (!addingForce && restCounter < 10)
		{
			velocity.x += windPower * 0.001f * Time.deltaTime;
			velocity.y -= gravity * Time.deltaTime;
		}
		//if we've started resting, but aren't colliding any more then keep resting till death
		else if (restCounter >= 10)
			restCounter++;
		
		// Update position
		transform.position = transform.position + new Vector3((velocity.x * 60) * Time.deltaTime, (velocity.y * 60) * Time.deltaTime, 0);


		//dah widn
		//perlin noise is time on the x, and position on the y
		//subtract from it so that it goes between - to +'ve instead of 0 to 1.
		//Reduce the power significantly, cause dat shit be cray!
		windPower += (Mathf.PerlinNoise(Time.time, transform.position.y) - 0.47f) * 0.05f; //(Mathf.PerlinNoise(Time.time, 0) * 6) - 3 ;//Random.Range(-3.0f, 3.0f);
		//cap dah widn
		if (windPower < -3)
			windPower = -3;
		if (windPower > 3)
			windPower = 3;


	}

	//Make the shoot direction the direction that we want it to go
	void shoot(Vector3 direction)
	{
		shootDirection = direction;
		shootDirection.Normalize();
	}

	//it's over 9000
	void shootPower(float power)
	{
		shootPow = power;
	}
	

}
