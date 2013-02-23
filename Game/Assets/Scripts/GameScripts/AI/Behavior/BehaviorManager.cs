using System;
using UnityEngine;
using System.Collections.Generic;
using BehaviorTrees;

/// <summary>
/// Handles a collection of GameObjects(subject to change to a type with some kind of RunAI method).
/// </summary>
public abstract class BehaviorManager<TKey,TObject> : MonoBehaviour where TObject : AbstractAIActor {
	
	private IDictionary<TKey,TObject> objects; 
	
	private TObject proto;
	private int frameCounter = 0; 
	
	public float Interval;
	public static readonly float DEFAULT_INTERVAL = 0.1f;
	public static readonly int UPDATE_FREQ = 10;
	
	// Use this for initialization
	protected virtual void Start () {
		Interval = DEFAULT_INTERVAL;
		objects = new Dictionary<TKey, TObject>();
		
	}
	
	void Periodic() {
		foreach (KeyValuePair<TKey,TObject> kvp in objects) {
			kvp.Value.RunAI();
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (frameCounter == 0) {
			Periodic();
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
	public void Spawn(TObject proto, TKey key, Vector3 pos, Quaternion rot) {
		TObject o = Instantiate(proto, pos, rot) as TObject; 
		
		objects.Add(key,o);
	}
	/// <summary>
	/// Spawn a ****GameObject**** with the given properties if there is a
	/// unique key to assign, otherwise return null.
	/// </summary>
	public TKey Spawn (TObject proto, Vector3 pos,Quaternion rot) {
		TKey k = GetUniqueKey();
		if (k != null) 
			Spawn(proto,k,pos,rot);
		return k;
	}
	
	public TKey Spawn(TObject proto) {
		return Spawn (proto, transform.position, proto.transform.rotation);
	}
	
	public TKey Spawn() {
		return Spawn (proto);
	}
	
	/// <summary>
	/// Get a unique value of type TKey to be used when storing.
	/// </summary>
	/// <returns>
	/// The unique key. If no unique keys can be given, return null.
	/// </returns>
	protected abstract TKey GetUniqueKey();
}
