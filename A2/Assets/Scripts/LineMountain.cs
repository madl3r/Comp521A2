using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LineMountain : MonoBehaviour {


	public GameObject mtnCube;
	public Vector3 foot;
	public Vector3 tip;
	public bool isRightMountain;

	//Generic list or array list... not sure
	//At the very least we will use insert.
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

		spawnMountain(pointList, 4);
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
			}

		}

		// getting the perpendicular lines and moving the midpoints along it
		for (int i = 0; i < pList.Count; i++)
		{
			if (i != 0 && i != pList.Count -1)
			{
				Vector3 theLine = pList[i+1] - pList[i];
				Vector3 perpLine = new Vector3(-theLine.y, theLine.x, 0);
				
				pList[i] = pList[i] + (Random.Range(-0.5f, 0.5f) * perpLine);
			}

		}

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

		//Debug.Log("The Length of the list is now: " + pList.Count);

		//for each (except the last) point in the list
		//find the mid point, and insert that point.
		//Get the orthog of the line between pt A & B
		//move the new point some amount + or -ly along that line.
		//Move forward (make sure you pass the new point.
		//repeat.

		//Once that is done, loop again doing the midpoints with the now existant points
		//loop this until we are happy with the number of (depth times)


		//When we're finally done with all of that go between all of the points and spawn a square on the midpoint
		//Rotate the square so that it's x axis is on the line between the two points
		//scale the square along the x to the length of the distance between the two
		// make sure that the squares y scale is low.
	}

	Vector3 getMidPoint (Vector3 pA, Vector3 pB)
	{
		Vector3 C = new Vector3((pA.x + pB.x) / 2, (pA.y + pB.y) / 2, 0);
		return C;
	}

	void gimmieYourList (int i)
	{
		bulletScript.mountainBlocksList[i] = theMountainPieces;
	}

	void updatePoints (int i)
	{
		//pointList[i] = pointList[i] + new Vector3 (0.2f, -0.2f, 0);
		//pointList[i].y -= 0.5f;
		if (i != 0)
		{
			pointList[i-1] = pointList[i-1] + new Vector3 (0.1f, -0.1f, 0);
		}
		pointList[i] = pointList[i] + new Vector3 (0.2f, -0.2f, 0);
		if (i < theMountainPieces.Count - 1)
		{
			pointList[i+1] = pointList[i+1] + new Vector3 (0.1f, -0.1f, 0);

		}

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

	void redrawBlock(int i)
	{
		theMountainPieces[i].transform.position = getMidPoint(pointList[i], pointList[i+1]);
		theMountainPieces[i].transform.rotation = (Quaternion.LookRotation(pointList[i+1] - pointList[i]));
		theMountainPieces[i].transform.localScale = new Vector3 (0.05f, 0.05f, Vector3.Distance(pointList[i], pointList[i+1]));

	}


}
