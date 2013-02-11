using UnityEngine;
using System.Collections;

using Pathfinding.Core;
using Pathfinding.Graph;

public class Follow : MonoBehaviour {
	
	public Transform target;
	public Transform floor;
	public float speed = 100;
	public float wayPointDistance = 0.3f;
	
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
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		
		targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
		graph = floor.GetComponent<TileGraphGenerator>().tileGraph;
		if (graph == null) {
			Debug.Log ("no graph");
			return;
		}
		
		
		lastPath += Time.deltaTime;
		
		if (target.position != targetPosition && lastPath > TIME_REPATH) {
			lastPath = 0;
			targetPosition.Set (target.position.x, target.position.y, target.position.z);
			path = graph.AStar(transform.position, target.position);
			currentPos = 0;
		}
		
		//path = graph.AStar(transform.position, target.position);
		
		if (path == null || currentPos >= path.Count) return;
		
		Vector3 direction = (path[currentPos] - transform.position).normalized;
		direction *= speed * Time.deltaTime;
		controller.SimpleMove(direction);
		
		if (Vector3.Distance(transform.position, path[currentPos]) < wayPointDistance) {
			currentPos++;
		}
		
		path.drawDebugPath(Color.green);
		
	}
	
}
