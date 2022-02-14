using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModules{
	[RequireComponent (typeof(CameraModules.CameraMain))]
	public class CameraLookAt : MonoBehaviour {
		private CameraModules.CameraMain main;
		public Vector3 offset;
		public float easiness;
		public bool fast;

		void Start() {
			main = GetComponent<CameraModules.CameraMain>();

			print ("container.transform.rotation");
		}

		void Update() {
			if (fast) {
				main.container.LookAt(transform.rotation * offset + main.player.transform.position);
				return;
			}

			Quaternion lookrot = Quaternion.LookRotation(transform.rotation * offset + main.player.transform.position	- main.container.position, transform.up);
			main.container.rotation = Quaternion.Lerp(main.container.rotation, lookrot, easiness*Time.deltaTime);
		}
	}
}
