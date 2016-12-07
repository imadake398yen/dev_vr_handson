using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour {

	public Const.ObjectType objectType;

	[SerializeField] private Transform center;
	public Transform Center {
		get {
			return center != null ? center : transform; 
		}
	}

	public string description;
	
}
