using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class TrafficController : MonoBehaviour {

	public int numEnemies = 6; 
	public List<int> spawnPoints;         // not very flexible. may want to use list instead
	public Transform Enemy; //or GameObject 
	public string NavPathName; 

	public bool isStreaming; 			// can change this in inspector for testing 
	public bool isEOStream; 			// detecs end of round. be careful with mods

	private int enemiesLeft; 
	private int spawnPointIndex;
	private int safeGapIndex; 

	private float Timer; 
	private float gapTime = 1.5f; 
	private float safeGapTime = 3.0f; 


	void Start() {
		Timer = Time.time + 3; 
		isEOStream = true; 
		setSpawnPoints ();
		ResetValues (); 
	}

	// if triggered -> initiate traffic stream 
	// if not end of traffic stream -> continue traffic stream
	void Update() {

		// Begin traffic stream with input
		if (Input.GetKeyDown(KeyCode.A)) {
			ResetValues();
			isStreaming = true;
			isEOStream = false; 

		}


		// flags to detect is "green" traffic signal ;)
		// and to detect if all cars have "left"  the scene
		if (isStreaming == true) { 
			// if not all 6 cars spawned, then spawn at apro time interval!
			if (Timer < Time.time && spawnPointIndex < spawnPoints.Count) {  //This checks whether real time has caught up to timer
				SpawnEnemy();   			
			} // end if (timer)

			// detect if end of stream is reached. manipulate flags 
			isEOStream = isEndOfStream (); 
			if (isEOStream) {
				isStreaming = false; 
				Debug.Log ("END OF STREAM");
			}
		} // end (enemy loop)

	} // end update()




	void SpawnEnemy(){
		NavPathName = "NavPath" + (spawnPoints[spawnPointIndex]); // send NavPathName to enemy GObj
		//get position of first waypoint in list and force inital rotation towards second waypoint
		Transform FirstWayPoint = GameObject.Find(NavPathName).transform.GetChild(0); 
		Transform SecondWayPoint = GameObject.Find(NavPathName).transform.GetChild(1); 
		Vector3 relativePos = SecondWayPoint.position - FirstWayPoint.position;
		Instantiate (Enemy, FirstWayPoint.position, Quaternion.LookRotation(relativePos)); 

		if (spawnPointIndex == spawnPoints.Count)
			ResetValues (); // stop spawning & reset vals

		if (spawnPointIndex == safeGapIndex - 1)
			Timer = Time.time + safeGapTime; 
		else 
			Timer = Time.time + gapTime;  //Set Timer 3 seconds into the future 
		spawnPointIndex += 1;   	
	}

	// when cars reach goal waypoints, they will call Destroy() 		
	// so to detect end of stream, we just need to check how many "car"s are still active in the scene 
	// 	NOTE: FINDGAMEOBJECT(S) IS EXPENSIVE - REPLACE LATER WITH MORE EFFICIENT SOLUTION 
	// 	BAD PRACTICE TO USE IN UPDATE() LOOP 
	bool isEndOfStream() {
		// set isEOStream
		GameObject[] cars = GameObject.FindGameObjectsWithTag("Enemy");
		enemiesLeft = cars.Length;
		if (enemiesLeft == 0)
			return true;
		else return false; 
	}

	// common values reset at begining of each round 
	void ResetValues() {
		isStreaming = false;

		enemiesLeft = 0; 
		spawnPointIndex = 0; 
		safeGapIndex = Random.Range(1, spawnPoints.Count-1); 	// select random position in array of spawnPoints for safeIndex

		Shuffle (spawnPoints); 
	}

	// Fisher-Yates (or Knuth shuffle) 
	// Randomizes the order of elements in spawnPoints
	void Shuffle(List<int> spawnPoints)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < spawnPoints.Count; t++ )
		{
			int tmp = spawnPoints[t];
			int r = Random.Range(t, spawnPoints.Count);
			spawnPoints[t] = spawnPoints[r];
			spawnPoints[r] = tmp;
		}
	}

	// Assign each index of the spawnPoints array to an int val
	// The val is the same as the current index
	void setSpawnPoints() {
		for (int i = 0; i < numEnemies; i++)
			spawnPoints.Add(i+1);
	}

}

// Note: IN ENEMY SCRIPT
// remember to give GO a rigidbody (should have)
// void OnTriggerEnter(Collider other)  {
//  	if (other.gameObject.tag == "Player") <- not helpful
//		if (other.gameOBejct.tag == "final")
//			Destroy(this); 



// SELF NOTES
// You've forgotten a lot of the more zesty C# features
// Review Delegates, Virtual/Override Methods
// Lambdas, Action Functions 
// Actually, maybe just brush up on the Unity Adv prog. tuts again