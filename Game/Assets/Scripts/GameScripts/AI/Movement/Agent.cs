using UnityEngine;
using System.Collections;

using Pathfinding.Core;
using Pathfinding.Graph;

public class Agent : MonoBehaviour {
	
	/* Public parameters */
	public Transform floor;
	public float speed = 200;
	public float wayPointDistance = 0.3f;
	
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
	
	void Start () {
		
		controller = GetComponent<CharacterController>();
		
		//targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
		currentPoint = new Vector3(0,0,0);
		targetPosition = transform.position;
		
	}
	
	/* When a new path has been computed */
	void onCallback(Path newPath) {
		path = newPath;
		currentPos = 1;
	}
	
	void Update () {
		graph = floor.GetComponent<TileGraphGenerator>().tileGraph;
		if (graph == null) return;
		
		lastPath += Time.deltaTime;
		
		if (lastPath > TIME_REPATH && Vector3.Distance(transform.position, targetPosition) > 0.3) {
			lastPath = 0;
			graph.AStar(transform.position, targetPosition, new OnPathComputed(onCallback));
		}
		
		if (path == null || currentPos >= path.Count) return;
		
		currentPoint.Set(path[currentPos].x, transform.position.y, path[currentPos].z);
		
		Vector3 direction = (currentPoint - transform.position).normalized;
		direction *= speed * Time.deltaTime;
		controller.SimpleMove(direction);
		
		if (Vector3.Distance(transform.position, currentPoint) < wayPointDistance) {
			currentPos++;
		}
		
		path.drawDebugPath(Color.green);
	}
	
	public void MoveTo(Vector3 pos) {
		targetPosition = pos;
	}
}
