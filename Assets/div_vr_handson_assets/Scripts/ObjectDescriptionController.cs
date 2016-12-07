using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectDescriptionController
:SingletonMonoBehaviour<ObjectDescriptionController> {

	[SerializeField] private Canvas canvas;
	[SerializeField] private Text descriptionLabel;
	private PlayerController player;
	private SelectableObject target;

	private float remainingDesplayTime;
	const float desplayTime = 2.5f;

	private void Update () {
		
		if (player == null) { 
			if (PlayerController.instance == null) return;
			else player = PlayerController.instance;
		}
		canvas.enabled = target == null ? false : true;
		if (!canvas.enabled) return;

		transform.LookAt(player.transform);

		if (target != null) {
			transform.position = player.transform.position 
				+ (target.Center.position - player.transform.position).normalized * 2.0f;
		}

		remainingDesplayTime -= Time.deltaTime;
		if (remainingDesplayTime <= 0) {
			canvas.enabled = false;
			target = null;
		}

	}

	public void SetTarget (SelectableObject t) {
		if (target != t) {
			remainingDesplayTime = desplayTime;
			target = t;
			StartCoroutine(PutText(t.description));
		}
	}

	private IEnumerator PutText (string text) {
		for (int i=0; i<text.Length; i++) {
			descriptionLabel.text = text.Substring(0, i+1);
			yield return new WaitForSeconds(0.08f);
		}
	}
}
