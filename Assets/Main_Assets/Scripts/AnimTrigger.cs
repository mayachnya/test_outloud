using UnityEngine;
using System.Collections;

public class AnimTrigger : MonoBehaviour {
	public GameObject Obj;
	public string NameAnimation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Obj.GetComponent<Animation>().Play (NameAnimation);
		}
	}
}
