using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour { 

	private UnityEngine.AI.NavMeshAgent agent; 
	Quaternion startingAngle = Quaternion.AngleAxis(-80, Vector3.up); 
	Quaternion stepAngle = Quaternion.AngleAxis(10, Vector3.up); 
	Vector3 origin; 
	float maxspeed;

	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
		maxspeed = agent.speed;
		origin = new Vector3( 0f, 0f, 0f );
	}
	void Update () {
		DetectThings (); //move this to RepeatInstance later
	}



	void DetectThings()
	{
		bool superslow = false; 
		bool slow = false;
		float mydistance;
		float yourdistance;


		RaycastHit hit;
		var angle = transform.rotation * startingAngle;
		var direction = angle * Vector3.forward;
		var pos = transform.position;
		for(var i = 0; i < 14; i++)
		{
			Debug.DrawRay(pos, direction, Color.green);
			if(Physics.Raycast(pos, direction, out hit, 20f))
			{
				Debug.DrawRay(pos, direction, Color.green);
				if(hit.collider.tag == "Enemy")
				{
					//Debug.Log ("Hit something!"); 
					//hit.collider.gameObject.transform.GetChild (0).gameObject.SetActive (false);

					// also include distance check towards center 
					// if hit object is closer to center, then I yield 
					// 0.5f buffer added to compensate for adjacent circulating cars (no yeild desired)
					yourdistance = Vector3.Distance(origin, hit.collider.transform.position);
					mydistance = Vector3.Distance (origin, transform.position);
					if (mydistance - yourdistance > 0.5f) {
						if (i / 2 == 3 || i / 2 == 4)
							superslow = true;
						slow = true; 
					}
				}
			}
			direction = stepAngle * direction;
		}
			
		if (slow && superslow)
			agent.speed -= 0.09f;
		else if (slow)
			agent.speed -= 0.2f;
		else {
			agent.speed = maxspeed;
		}
	}



	// PRACTICE FUNCTIONS - NOT IN USE 

	// FIX THIS JUST USE RAYCASTING INSTEAD GEEEZ
	//OnCollisionEnter  OnCollisionStay  OncollisionExit
	/*
	void OnCollisionStay(Collision col) { 
		if (col.transform.tag == "Enemy" || col.transform.tag == "Player") {
			agent.acceleration = -5f;
			//Debug.Log ("Collided with something!");
		}
	}
	void OnCollisionExit(Collision col) {
		agent.acceleration = 8f;
	}*/
		
	// While this is hilarious, I'm not going to use it
	// Makes objects turn to face the next waypoint
	// Generates a jerky motion. Use in Update()
	void TurnToFace()
	{
		Vector3 relativePos = agent.destination - transform.position;
		transform.rotation =  Quaternion.LookRotation(relativePos);
	}


}
