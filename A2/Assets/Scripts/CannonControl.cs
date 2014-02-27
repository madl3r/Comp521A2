using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {


	public GameObject bullet;
	public GameObject basePoint, lookingPoint;
	public GameObject powerText, armedText;
	private float dahPower;
	float shootTime;
	private bool canShoot;

	// Use this for initialization
	void Start () {
		dahPower = 1.0f;
		canShoot = true;
		powerText.guiText.text = "GUN POWER: " + dahPower;
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKey("up") && 
		   (Vector3.Dot((basePoint.transform.position - lookingPoint.transform.position), Vector3.right) <  0.025f)) //&& the rotation isn't equal to forward
			transform.Rotate(Vector3.forward * Time.deltaTime * 500);
		if(Input.GetKey("down") && 
		   (Vector3.Dot((basePoint.transform.position - lookingPoint.transform.position), Vector3.up) <  0.025f))
			transform.Rotate(Vector3.back * Time.deltaTime * 500);

		if(Input.GetKey("space") && canShoot)
		{
			spawnAndShoot();
			canShoot = false;
			armedText.guiText.text = "LOADING";
			shootTime = Time.time;
		}

		if (Time.time - shootTime > 0.5f && !canShoot)
		{
			canShoot = true;
			armedText.guiText.text = "ARMED";
		}

		if(Input.GetKeyDown("left") && dahPower > 0)
		{
			dahPower -= 0.1f;
			powerText.guiText.text = "GUN POWER: " + dahPower.ToString("0.0");;
			//tempBullet.SendMessage("shootPower", dahPower);
		}
		else if (Input.GetKeyDown("left") && dahPower <= 0)
		{
			dahPower = 0;
			powerText.guiText.text = "GUN POWER: " + dahPower.ToString("0.0");
			//tempBullet.SendMessage("shootPower", dahPower);
		}
		if (Input.GetKeyDown("right"))
		{
			dahPower += 0.1f;
			powerText.guiText.text = "GUN POWER: " + dahPower.ToString("0.0");
			//tempBullet.SendMessage("shootPower", dahPower);
		}
	}

	void spawnAndShoot()
	{
		GameObject tempBullet = (GameObject) Instantiate(bullet, lookingPoint.transform.position, Quaternion.identity);
		tempBullet.SendMessage("shootPower", dahPower);
		tempBullet.SendMessage("shoot", (lookingPoint.transform.position - basePoint.transform.position));
	}

}
