using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LineMountain : MonoBehaviour {


	public GameObject mtnCube;
	public Vector3 foot;
	public Vector3 tip;
	public bool isRightMountain;

	//Generic list or array list... not sure
	List<Vector3> pointList;
	public List<GameObject> theMountainPieces;

	//maybe have a list of all of the other points. Go from the previous to the next always.
	private Vector3 cbSpawnPt;

	// Use this for initialization
	void Start () {
		theMountainPieces = new List<GameObject>();
		pointList = new List<Vector3>();

		pointList.Add(foot);
		pointList.Add(tip);

		//Spawning the mountain at 5 lvls deep(I like that look the most)
		spawnMountain(pointList, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnMountain(List<Vector3> pList, int depth)
	{

		//testing the midpoint
		for (int j = 0; j < depth; j++)
		{
			int oldCount = pList.Count;
			//Debug.Log("New count is: " + oldCount);

			//plus two to do every other one!
			for (int i = 0; i < pList.Count - 1; i += 2)
			{
				pList.Insert(i+1, getMidPoint(pList[i], pList[i+1]));

				//decided to put the point movement inside of the point creation (so new points now are at different distances)
				if (i+1 != 0 && i+1 != pList.Count -1)
				{
					Vector3 theLine = pList[i+2] - pList[i];
					Vector3 perpLine = new Vector3(-theLine.y, theLine.x, 0);
					
					pList[i+1] = pList[i+1] + (Random.Range(-0.1f, 0.1f) * perpLine);
				}


			}

		}


		//point movement used to be out here... points were originally all made along a line then moved...
		//I sorted liked how this looked more but I don't think that that's what midpoint bisection actually is...

		// getting the perpendicular lines and moving the midpoints along it
//		for (int i = 0; i < pList.Count; i++)
//		{
//			if (i != 0 && i != pList.Count -1)
//			{
//				Vector3 theLine = pList[i+1] - pList[i];
//				Vector3 perpLine = new Vector3(-theLine.y, theLine.x, 0);
//				
//				pList[i] = pList[i] + (Random.Range(-0.5f, 0.5f) * perpLine);
//			}
//
//		}

		//making mountain cubes, rotating and then scaling them
		for (int i = 0; i < pList.Count - 1; i++)
		{

			GameObject tempCube;

			tempCube = (GameObject) Instantiate(mtnCube, getMidPoint(pList[i], pList[i+1]), Quaternion.identity);

			if (isRightMountain)
				tempCube.tag = "dahMountain";

			tempCube.transform.rotation = (Quaternion.LookRotation(pList[i+1] - pList[i]));
			tempCube.transform.localScale = new Vector3 (0.05f, 0.05f, Vector3.Distance(pList[i], pList[i+1]));

			theMountainPieces.Add(tempCube);

		}

	}

	//gets and returns the midpoint between two lines
	Vector3 getMidPoint (Vector3 pA, Vector3 pB)
	{
		Vector3 C = new Vector3((pA.x + pB.x) / 2, (pA.y + pB.y) / 2, 0);
		return C;
	}

	//updates the bullet lists to the newest mountain
	void gimmieYourList (int i)
	{
		bulletScript.mountainBlocksList[i] = theMountainPieces;
		bulletScript.thePointList[i] = pointList;
	}

	//Get hit by a bullet.
	//move mountain shit around it a bit, and move the thing hit the most.
	void updatePoints (int i)
	{
		//move dah points
		if (i != 0)
		{
			pointList[i-1] = pointList[i-1] + new Vector3 (0.1f, -0.1f, 0);
		}
		pointList[i] = pointList[i] + new Vector3 (0.2f, -0.2f, 0);
		if (i < theMountainPieces.Count - 1)
		{
			pointList[i+1] = pointList[i+1] + new Vector3 (0.2f, -0.2f, 0);

		}
		if (i < theMountainPieces.Count - 2)
			pointList[i+2] = pointList[i+2] + new Vector3 (0.1f, -0.1f, 0);


		//move the blocks
		if (i != 0)
		{
			redrawBlock(i-1);
			if (i != 1)
				redrawBlock(i-2);
		}
		redrawBlock(i);
		if (i < theMountainPieces.Count - 1)
		{		redrawBlock(i+1);
			if (i < theMountainPieces.Count - 2)
				redrawBlock(i+2);
		}

	}

	//Redraws the block at i.
	void redrawBlock(int i)
	{
		theMountainPieces[i].transform.position = getMidPoint(pointList[i], pointList[i+1]);
		theMountainPieces[i].transform.rotation = (Quaternion.LookRotation(pointList[i+1] - pointList[i]));
		theMountainPieces[i].transform.localScale = new Vector3 (0.05f, 0.05f, Vector3.Distance(pointList[i], pointList[i+1]));
	}


}
