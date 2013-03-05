using UnityEngine;
using System.Collections;

public class SpawnMineScript : MonoBehaviour {
	
	public GameObject minePrefab;
	//GameObject powerUp;
	MineActivated mine;
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
			
			if(Physics.OverlapSphere(pos, minePrefab.GetComponent<CapsuleCollider>().radius).Length == 0){
				
				mine = InstantiationUtils.GetNewInstance<MineActivated>(minePrefab, pos);//(GameObject) Instantiate(powerUpPrefab, pos, Quaternion.identity);
				mine.setProperties(ActorController.getActorController().getBallActors());
				GroundStuffController.getGroundStuffController().addMine(mine);
				removeMine(mine);
			}
			nextSpawnTime = Time.time + spawnPeriod;
		}
	}
	
	void removeMine(MineActivated mineToRemove){
		StartCoroutine(Wait(20.0f, mineToRemove));
	}
	
	private IEnumerator Wait(float seconds, MineActivated g) {
		
        yield return new WaitForSeconds(seconds);
		//Destroy(g);
		GroundStuffController.getGroundStuffController().destoyMine(g);
	
	}
}
