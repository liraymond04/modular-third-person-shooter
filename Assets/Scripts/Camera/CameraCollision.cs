using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModules {
	[RequireComponent (typeof(CameraModules.CameraMain))]
	public class CameraCollision : MonoBehaviour {
		private CameraModules.CameraMain main;

        private RaycastHit hit;

		void Start() {
			main = GetComponent<CameraModules.CameraMain>();
		}

		void FixedUpdate() {
    	    if (Physics.Linecast(main.player.transform.position, main.container.transform.position, out hit)) {
    	        main._camera.transform.position = hit.point;
    	    } else {
				main._camera.transform.position = main.container.transform.position;
			}
		}
	}
}
