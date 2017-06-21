using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Patrol : MonoBehaviour {

	public string NavPathName;
	public List<Transform> waypoints;
	private int destPoint = 0;
	private UnityEngine.AI.NavMeshAgent agent;


	void Start () {
		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
		agent.autoBraking = true; 

		// assign path of waypoints here
		//PutOnGround();
		AssignPath ();
		GotoNextPoint();
	}

	void Update () {
		// Choose the next destination point when the agent gets
		// close to the current one.
		//PutOnGround();
		float dist=agent.remainingDistance; 
		//if (dist!=Mathf.Infinity && agent.pathStatus==NavMeshPathStatus.completed && agent.remainingDistance==0) //Arrived.
		//{Debug.Log (destPoint);}

		if (destPoint != 0) { //&& dist > 1.0f) { 
			//Debug.Log (waypoints.Count);
			if (agent.remainingDistance < 10.1f)
				GotoNextPoint ();
		} else {
			agent.destination = gameObject.transform.localPosition;
			Destroy (gameObject); 
		}

	}


	void AssignPath() {
		//take empty gameobject name from traffic manaager script
		//NavPathName = "NavPath2"; // FOR TESTING ONLY
		NavPathName = GameObject.Find("TrafficManager").GetComponent<TrafficController>().NavPathName;
		Transform PathRoot = GameObject.Find (NavPathName).GetComponent<Transform>();
		Debug.Log ("PATHNAME: "+ NavPathName); 

		foreach (Transform child in PathRoot) {
			//print ("Foreach loop: " + child); 
			waypoints.Add (child.transform);

		}
	}

	// make sure game object is in contact with ground 
	// ground detection and placement via raycasting to ground 
	void PutOnGround() {
		LayerMask mask = -1; 
		float radius; 
		if (GetComponent<Collider> () != null)
			radius = (GetComponent<BoxCollider>().bounds.extents.y);
		else
			radius = 1.0f; 
		//Debug.Log (radius);

		RaycastHit hit;
		Ray ray = new Ray (transform.position + Vector3.up * 10, Vector3.down); // ray starts @ 100 units 
		Debug.DrawRay(transform.position + Vector3.up * 10, Vector3.down, Color.red);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask)) {
			if (hit.collider != null)
				transform.position = new Vector3 (transform.position.x, hit.point.y + radius, transform.position.z); 
		}
	}
		
	void GotoNextPoint() {
		// Returns if no waypoints have been set up
		if (waypoints.Count == 0)
			return;

		//var dist = Vector3.Distance(targets[i].position,transform.position);
		// Set the agent to go to the currently selected destination.
		agent.destination = waypoints[destPoint].position;

		// Choose the next point in the array as the destination,
		destPoint = (destPoint + 1) % waypoints.Count;
	}



}
