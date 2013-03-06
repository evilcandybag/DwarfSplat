using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BehaviorTrees;

/// <summary>
/// Handles a collection of GameObjects(subject to change to a type with some kind of RunAI method).
/// </summary>
public abstract class BehaviorManager<TKey,TObject> : MonoBehaviour where TObject : AbstractAIActor {
	
	private IDictionary<TKey,TObject> objects;
	public IDictionary<TKey,TObject> Objects {
		get { return objects; }
	}
	
	private bool runAI_;
	public bool Run {
		get { return runAI_;}
		set { runAI_ = value; }
	}
	
	public GameObject proto;
	private int frameCounter = 0; 
	
	public float Interval;
	public static readonly float DEFAULT_INTERVAL = 0.1f;
	public int UPDATE_FREQ = 60;
	
	void Awake() {
		objects = new Dictionary<TKey, TObject>();
	}
	
	// Use this for initialization
	public virtual void Start () {
		Interval = DEFAULT_INTERVAL;
		
	}
	
	System.Collections.IEnumerator Periodic() {
		foreach (KeyValuePair<TKey,TObject> kvp in objects) {
			kvp.Value.RunAI();
			yield return null;
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (runAI_ && frameCounter == 0) {
			StartCoroutine( Periodic());
		}
		frameCounter = (frameCounter + 1) % UPDATE_FREQ;
	}
	
	/// <summary>
	/// Spawn a ****GameObject**** and add it to the manager.
	/// </summary>
	/// <param name='proto'>
	/// The prototype for the TObject.
	/// </param>
	/// <param name='key'>
	/// The desired key to save the object under.
	/// </param>
	/// <param name='pos'>
	/// The desired position of the newly Spawned object.
	/// </param>
	/// <param name='rot'>
	/// The desired rotation of the newly Spawned object.
	/// </param>
	public virtual TObject Spawn(GameObject proto, TKey key, Vector3 pos, Quaternion rot) {
		
		TObject obj = InstantiationUtils.GetNewInstance<TObject>(proto,pos,rot);
		objects.Add(key,obj);
		return obj;
	}
	/// <summary>
	/// Spawn a ****GameObject**** with the given properties if there is a
	/// unique key to assign, otherwise return null.
	/// </summary>
	public TKey Spawn (GameObject proto, Vector3 pos,Quaternion rot) {
		TKey k = GetUniqueKey();
		if (k != null) 
			Spawn(proto,k,pos,rot);
		return k;
	}
	
	public TKey Spawn(Vector3 location) {
		return Spawn (this.proto, location, this.proto.transform.rotation);
	}
	
	public TKey Spawn(GameObject proto) {
		return Spawn (proto, transform.position, proto.transform.rotation);
	}
	
	public TKey Spawn() {
		return Spawn (proto);
	}
	
	public void Decommission(TObject o) {
		foreach(var item in objects.Where(kvp => kvp.Value == o).ToList()) {
    		objects.Remove(item.Key);
		}
	}

	
	/// <summary>
	/// Get a unique value of type TKey to be used when storing.
	/// </summary>
	/// <returns>
	/// The unique key. If no unique keys can be given, return null.
	/// </returns>
	protected abstract TKey GetUniqueKey();
}
