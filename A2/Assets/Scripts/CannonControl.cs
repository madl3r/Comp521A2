using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("left"))
			transform.Rotate(Vector3.forward * Time.deltaTime * 500);
		if(Input.GetKey("right"))
			transform.Rotate(Vector3.back * Time.deltaTime * 500);
	}
}
