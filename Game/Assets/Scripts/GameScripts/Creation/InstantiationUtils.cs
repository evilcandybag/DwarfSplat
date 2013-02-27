using UnityEngine;

public class InstantiationUtils
{
	public static T GetNewInstance<T> (GameObject obj,Vector3 loc, Quaternion rot) where T : MonoBehaviour {
		GameObject go = Object.Instantiate(obj,loc,rot) as GameObject;
		return go.GetComponent<T>();
	}
	
	public static T GetNewInstance<T> (GameObject obj,Vector3 loc) where T : MonoBehaviour {
		return GetNewInstance<T>(obj,loc,obj.transform.rotation);
	}
	public static T GetNewInstance<T> (GameObject obj) where T : MonoBehaviour {
		return GetNewInstance<T>(obj,obj.transform.position,obj.transform.rotation);
	}
}


