using UnityEngine;
using System.Collections;


public class flagScript : MonoBehaviour {

	public GameObject flagCube;
	public Material tameColor, maxColor;
	float windPower;

	// Use this for initialization
	void Start () {
		windPower = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		//same perlin noise as with the bullet (time might not be 100% exactly the same... but is definitely close enough!!
		windPower += (Mathf.PerlinNoise(Time.time, transform.position.y) - 0.47f) * 0.05f;
			
		if (windPower < -3)
			windPower = -3;
		if (windPower > 3)
			windPower = 3;

		transform.localScale =  new Vector3 (windPower, 0.5f, 1.0f);
		flagCube.renderer.material.color = Color.Lerp(tameColor.color, maxColor.color, Mathf.Abs(transform.localScale.x / 3));
	}

}
