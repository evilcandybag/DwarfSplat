using UnityEngine;
using System.Collections;
using System;

using Pathfinding.Core;
using Pathfinding.Graph;

public class MovementAgent : MonoBehaviour {
	
	/* Public parameters */
	public float speed = 200;
	public float wayPointDistance = 0.3f;
	private float realWayPointDistance;
	public bool smoothPath = false;
	
	/* Debugging lines in the editor */
	public bool drawPath = false;
	public string scriptsName = "Scripts";
	
	/* Requires a CharacterController to move the agent
	 * (may change in the future)
	 */ 
	protected CharacterController controller;
	protected Graph graph = null;
	
	private Vector3 targetPosition;
	private Path path;
	private int currentPos = 0;

	/* 
	 * Refresh interval for the path
	 * It prevents the path to be recomputed too often, and during 
	 * this time the AI does not get stuck, it keep moving
	 */
	public float TIME_REPATH = 1;
	private float lastPath = 0;
	private Vector3 currentPoint;
	
	/** Callback after the agent reached the target */
	private Action<Result> endCallback = null;
	private Result result;
	
	void Start () {
		// avoid having to add a controller by hand from the editor
		controller = gameObject.AddComponent<CharacterController>();
		// init positions
		currentPoint = new Vector3(0,0,0);
		//targetPosition = transform.position;

	}
	
	/* When a new path has been computed */
	void onCallback(Path newPath) {
		path = newPath;
		if (path.Count == 0) {
			if (endCallback != null) endCallback(Result.FAIL);
		} else {
			if (smoothPath) path = Smoother.smoothPath(path);
			currentPos = 1; // avoid backward movement bug (temp solution)
		}
	}
	
	void Update () {
		
		// scale distance with the size of the agent
		realWayPointDistance = wayPointDistance;// * transform.localScale.magnitude;
		
		if (graph == null) {
			// graph is cached as soon as possible
			graph = GameObject.Find(scriptsName).GetComponent<TileGraphGenerator>().tileGraph;
			return; // skip the first frame
		}
		
		
		lastPath += Time.deltaTime;
		
		// update the path every TIME_REPATH
		if (lastPath > TIME_REPATH) {
			lastPath = 0;
			graph.AStar(transform.position, targetPosition, new OnPathComputed(onCallback));
		}
		
		if (path == null || path.Count == 0 || currentPos >= path.Count) return;
		
		// set the target y to be the same as the agent
		currentPoint.Set(path[currentPos].x, transform.position.y, path[currentPos].z);
		
		// move agent
		Vector3 direction = (currentPoint - transform.position).normalized;
		direction *= speed * Time.deltaTime;
		controller.SimpleMove(direction);
		
		// if close enough to the next way point
		if (Vector3.Distance(transform.position, currentPoint) < realWayPointDistance) {
			currentPos++;
		}
		
		// reached the end position?
		if (currentPos == path.Count-1) {
			if (endCallback != null) {
				endCallback(Result.SUCCESS); 
				endCallback = null;
			}
			//targetPosition = transform.position;
		}
		
		if (drawPath) path.drawDebugPath(Color.green);
	}
	
	public void MoveTo(Vector3 pos) {
		targetPosition = pos;	
	}
	
	public void MoveTo(Vector3 pos, int newSpeed, Action<Result> callback) {
		MoveTo(pos);
		speed = newSpeed;
		endCallback = callback;
	}
	
	/** Set an offset to divide the computation of the path 
	 * 	between all the agent.
	 * */
	public void SetOffset(float offset) {
		lastPath += offset;	
	}
}
