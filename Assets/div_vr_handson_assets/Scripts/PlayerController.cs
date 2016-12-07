using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController
: SingletonMonoBehaviour<PlayerController> {

	public Transform diveCameraOrigin;
	[SerializeField] private Image lookGaugeImage;
	[SerializeField] private GameObject taraiPrefab;

	const float lookGaugeSpeed = 0.02f;
	private readonly Vector3
		taraiSpawnOffset = new Vector3 (0, 3f, 0);

	private void Update () {
		Ray ray = new Ray(diveCameraOrigin.position, diveCameraOrigin.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {

			var enemy = hit.transform.GetComponent<EnemyController>();
			if (enemy != null) {
				if (Input.GetKeyDown(KeyCode.T)) {
					var spawnPos = enemy.transform.position + taraiSpawnOffset;
					var tarai = (GameObject)Instantiate(taraiPrefab, spawnPos, Quaternion.identity);
					Destroy(tarai, 4.0f);
				}
			}

			var selectable = hit.transform.GetComponent<SelectableObject>();
			if (selectable != null) {
				lookGaugeImage.fillAmount += lookGaugeSpeed;
				if (lookGaugeImage.fillAmount == 1.0f) {
					ObjectDescriptionController.instance.SetTarget(selectable);
				}
			} else {
				lookGaugeImage.fillAmount -= lookGaugeSpeed * 3.0f;
			}
		} else {
			lookGaugeImage.fillAmount -= lookGaugeSpeed * 3.0f;
		}
	}

}
