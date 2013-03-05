using UnityEngine;
using System.Collections;

/// <summary>
/// Spawn power ups script.
/// Julia Adamsson 2013
/// </summary>

public class SpawnPowerUpsScript : MonoBehaviour {
	
	public GameObject powerUpPrefab;
	//GameObject powerUp;
	ItemOnGround powerUp;
	public float x_min;
	public float x_max;
	public float z_min;
	public float z_max;
	public float y_min;
	public float y_max;
	
	float spawnPeriod = 2f;
	float nextSpawnTime;
	
	void Start () {
		nextSpawnTime = 0f;
	}
	
	void Update(){
		if(Time.time > nextSpawnTime){
			float x = Random.Range(x_min,x_max);
			float y = Random.Range(y_min,y_max);
			float z = Random.Range(z_min,z_max);
			
			Vector3 pos = new Vector3(x,y,z);
			
			if(Physics.OverlapSphere(pos, powerUpPrefab.GetComponent<CapsuleCollider>().radius).Length == 0){
				
				powerUp = InstantiationUtils.GetNewInstance<ItemOnGround>(powerUpPrefab, pos);//(GameObject) Instantiate(powerUpPrefab, pos, Quaternion.identity);
				powerUp.setProperties(ActorController.getActorController().getBallActors(), new DestroyWallsPowerup());
				GroundStuffController.getGroundStuffController().addItemOnGround(powerUp);
				removePowerUp(powerUp);
			}
			nextSpawnTime = Time.time + spawnPeriod;
		}
	}
	
	void removePowerUp(ItemOnGround powerUpToRemove){
		StartCoroutine(Wait(20.0f, powerUpToRemove));
	}
	
	private IEnumerator Wait(float seconds, ItemOnGround g) {
		
        yield return new WaitForSeconds(seconds);
		//Destroy(g);
		GroundStuffController.getGroundStuffController().destroyItem(g);
	
	}
	
}

