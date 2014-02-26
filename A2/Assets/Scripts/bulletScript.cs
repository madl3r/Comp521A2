using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	
	private Vector3 shootDirection;

	Vector3 velocity;// = new Vector3(0.0f, 0.0f, 0.0f);
	Vector3 maxVelocity;// = new Vector3(0.2f, 0.2f, 0.0f); // Change this to your desired maximum velocity

	bool addingForce;

	float startTime;
	float gravity = 0.5f;
	float shootPow;

	// Use this for initialization
	void Start () {
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		maxVelocity = new Vector3(0.2f, 0.2f, 0.0f); // Change this to your desired maximum velocity
		startTime = Time.time;
		addingForce = true;
		//shootPow = 1;
		//shoot (shootDirection);
		//velocity += (new Vector3(shootDirection.x * 100, shootDirection.y * 100, 0));


	}
	
	// Update is called once per frame
	void FixedUpdate () {


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
			velocity += (new Vector3(shootDirection.x * Time.deltaTime * shootPow, shootDirection.y * Time.deltaTime * shootPow, 0));

		}
		if (!addingForce)
			velocity.y -= gravity * Time.deltaTime;
		
		// Update position
		transform.position = transform.position + new Vector3((velocity.x * 60) * Time.deltaTime, (velocity.y * 60) * Time.deltaTime, 0);


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
