using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModules {
	[RequireComponent (typeof(CameraModules.CameraMain))]
	public class CameraFollowY : MonoBehaviour {
		private CameraModules.CameraMain main;
		[Range(0, 1f)]
		public float yRelax = 1f;

		Vector3 playerPrev;

		void Start() {
			main = GetComponent<CameraModules.CameraMain>();

			playerPrev = main.player.transform.position;
		}

		void Update() {
			Vector3 delta = Vector3.Project((main.player.transform.position - playerPrev), main.player.transform.up);
			if (delta == Vector3.zero) {
				return;
			}
			transform.position += delta * yRelax;
			playerPrev = main.player.transform.position;
		}

	}
}
