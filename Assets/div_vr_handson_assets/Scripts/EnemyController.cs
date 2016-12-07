using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	private int hitPoint = 5;
	private float speed = 1.0f;
	private PlayerController player;
	private Animator anim;

	private void Start () {
		anim = GetComponent<Animator>();
	}

	private void Update () {
		if (player == null) { 
			if (PlayerController.instance == null) return;
			else player = PlayerController.instance;
		}
		transform.LookAt(player.transform);

		float distance = Vector3.Distance(
			PlayerController.instance.transform.position, 
			transform.position);
		if (distance < 2.0f) {
			anim.SetBool("attack", true);
		} else {
			transform.position += transform.forward * Time.deltaTime * speed;
		}
	}

	private void OnCollisionEnter (Collision other) {
		var obj = other.transform.GetComponent<SelectableObject>();
		if (obj != null) {
			if (obj.objectType == Const.ObjectType.Tarai) {
				hitPoint -= 1;
				if (hitPoint == 0) {
					Destroy(gameObject);
				}
			}
		}
	}
	
}
