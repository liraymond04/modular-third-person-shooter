using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModules {
	[RequireComponent (typeof(CameraModules.CameraMain))]
	public class CameraOrbit : MonoBehaviour {
		private CameraModules.CameraMain main;
		public Vector2 sensitivity;
		public float yMin;
		public float yMax;
		public bool invertXAxis;
		public bool invertYAxis;

		void Start() {
			main = GetComponent<CameraModules.CameraMain>();
		}

		void Update() {
			Vector2 movements = Vector2.Scale(main._player.mouseInput, sensitivity) * Time.deltaTime;
			if (movements == Vector2.zero) {
				return;
			}

			if (invertXAxis) {
				movements.x *= -1;
			}
			if (invertYAxis) {
				movements.y *= -1;
			}

			transform.Rotate(0, movements.x, 0);
			if (main.container.localPosition.y >= yMax && movements.y > 0) {
				return;
			}
			if (main.container.localPosition.y <= yMin && movements.y < 0) {
				return;
			}

			main.container.RotateAround(transform.position, transform.right, movements.y);
		}
	}
}
