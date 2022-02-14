using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModules {
	[RequireComponent (typeof(CameraModules.CameraMain))]
	public class CameraFollow : MonoBehaviour {

		private CameraModules.CameraMain main;
		public float responsiveness;

		public bool smooth;

		private Vector3 up = Vector3.up;
		private Vector3 velocity = Vector3.zero;

		private Vector3 dist;

		void Start() {
			main = GetComponent<CameraModules.CameraMain>();

			up = transform.up;
		}

		void FixedUpdate() {
			// Vector3 dist = Vector3.ProjectOnPlane(main.player.transform.position - transform.position, up);

			// if (dist == Vector3.zero) {
			// 	return;
			// } else {
			// 	if (smooth) {
			// 		transform.Translate((dist) * responsiveness * Time.deltaTime, Space.World);
			// 	} else {
			// 		transform.Translate(dist, Space.World);
			// 	}
			// }
			dist = main.player.transform.position - transform.position;

			transform.position = Vector3.SmoothDamp(transform.position, transform.position + dist, ref velocity, responsiveness);
		}
	}
}
