using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController
: SingletonMonoBehaviour<PlayerController> {

	[SerializeField] private Transform diveCameraOrigin;
	[SerializeField] private Image lookGaugeImage;

	const float lookGaugeSpeed = 0.02f;

	private void Update () {
		Ray ray = new Ray(diveCameraOrigin.position, diveCameraOrigin.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
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
