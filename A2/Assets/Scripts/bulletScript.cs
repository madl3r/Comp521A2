using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bulletScript : MonoBehaviour {
	
	private Vector3 shootDirection;

	Vector3 velocity;// = new Vector3(0.0f, 0.0f, 0.0f);
	Vector3 maxVelocity;// = new Vector3(0.2f, 0.2f, 0.0f); // Change this to your desired maximum velocity


	public static List<GameObject>[] mountainBlocksList;

	GameObject[] mtnList;

	public float windPower;

	bool addingForce;
	bool hit;

	float startTime;
	float gravity = 0.5f;
	float shootPow;

	// Use this for initialization
	void Start () {

		mtnList = GameObject.FindGameObjectsWithTag("MountainParent");

		mountainBlocksList = new List<GameObject>[mtnList.Length];


		for (int i = 0; i < mtnList.Length; i++)
		{
			mtnList[i].SendMessage("gimmieYourList", i);
		}


		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		//maxVelocity = new Vector3(0.2f, 0.2f, 0.0f); // Change this to your desired maximum velocity
		startTime = Time.time;
		addingForce = true;
		hit = false;
		windPower = 1.0f;

		//shootPow = 1;
		//shoot (shootDirection);
		//velocity += (new Vector3(shootDirection.x * 100, shootDirection.y * 100, 0));


	}
	
	// Update is called once per frame
	void FixedUpdate () {


		for (int i = 0; i < mountainBlocksList.Length; i++)
		{
			int j = 0;
			foreach (GameObject mtnLine in mountainBlocksList[i])
			{
				if ((transform.position.x - mtnLine.transform.position.x) > (transform.localScale.x / 2) &&
				    (transform.position.y - mtnLine.transform.position.y) < (transform.localScale.y / 2) &&
				    (transform.position.x - mtnLine.transform.position.x) < (transform.localScale.x * 8))
				{
					//Debug.Log("COLLISION");
					//Destroy(gameObject);
					velocity = new Vector3(0, 0, 0);
					transform.position = new Vector3(transform.position.x - 0.01f, mtnLine.transform.position.y + transform.localScale.y / 2, 0);
					if (!hit)
						mtnList[i].SendMessage("updatePoints", j);
					hit = true;
				}
				j++;
			}
		}


		if (transform.position.y <= -3.0f)
		{
			Destroy(gameObject);
		}

		if (Time.time - startTime > 0.3f && addingForce)
		{
			addingForce = false;
			//Debug.Log("ADDING FORCE IS FALSE");
		}

		//velocity += Vector3.Lerp(new Vector3(shootDirection.x * Time.deltaTime * shootPow, shootDirection.y * Time.deltaTime *shootPow
		 //                        , 0), new Vector3 (0, -gravity * Time.deltaTime, 0), Time.deltaTime);

//		//WTF!? Make it go up and then down....
		if (addingForce)
		{
			//Debug.Log("shoot po: "  + shootPow);
			velocity += (new Vector3(shootDirection.x * Time.deltaTime * shootPow + windPower * 0.01f, shootDirection.y * Time.deltaTime * shootPow, 0));

		}
		if (!addingForce)
		{
			velocity.x += windPower * 0.001f * Time.deltaTime;
			velocity.y -= gravity * Time.deltaTime;
		}
		
		// Update position
		transform.position = transform.position + new Vector3((velocity.x * 60) * Time.deltaTime, (velocity.y * 60) * Time.deltaTime, 0);


		//dah widn
		windPower += (Mathf.PerlinNoise(Time.time, transform.position.y) - 0.47f) * 0.05f; //(Mathf.PerlinNoise(Time.time, 0) * 6) - 3 ;//Random.Range(-3.0f, 3.0f);
		
		if (windPower < -3)
			windPower = -3;
		if (windPower > 3)
			windPower = 3;


	}

	void shoot(Vector3 direction)
	{
		//Debug.Log("AM I HERE!:!");
		//shooting = true;
		//Debug.Log("~~~~~~~~~~SHOOTING IS: " + shooting);

		shootDirection = direction;
		shootDirection.Normalize();
		//velocity += (new Vector3(shootDirection.x * 100, shootDirection.y * 100, 0));
	}

	void shootPower(float power)
	{
		shootPow = power;
		Debug.Log("THE POWER IS: " + shootPow);
	}
	

}
