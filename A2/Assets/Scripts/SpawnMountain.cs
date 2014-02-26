using UnityEngine;
using System.Collections;

public class SpawnMountain : MonoBehaviour {

	//get a line, get the midpoint of that line.
	//move up or down on the perp of that line. Place the midpoint there and then recurse.
	//At the end spawn small cubes under that to fill up the mountain.

	public GameObject mtnCube;
	public Vector3 foot;
	public Vector3 tip;
	//maybe have a list of all of the other points. Go from the previous to the next always.
	private Vector3 cbSpawnPt;

	// Use this for initialization
	void Start () {


		cbSpawnPt = foot;//new Vector3(foot.x, foot.y, foot.z);

		NoiseLine(5, 1);
		spawn();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawn ()
	{
		while (cbSpawnPt.x <= tip.x)
		{
			while (cbSpawnPt.y >= foot.y)
			{
				//spawn block
				Instantiate(mtnCube, new Vector3(cbSpawnPt.x, cbSpawnPt.y), Quaternion.identity);

				//decrement y position by half block size
				cbSpawnPt = new Vector3(cbSpawnPt.x, cbSpawnPt.y - mtnCube.transform.localScale.y * 1.0f, 0);
			}
			//reset the y position... but along the line
			//increment the x position by half the block size
			cbSpawnPt = new Vector3(cbSpawnPt.x + mtnCube.transform.localScale.x * 1.0f, tip.y, 0); //replace tip.y with this x's top y thing.
		}
		//We've now made out mountain.

	}

	void NoiseLine(int numDivisions, int noiseAmt)
	{
		//TODO
	}
}
