using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Player))]
public class Shoot : MonoBehaviour {

    private Player player;

    public float maxDistance;

    public GameObject arm;

    public bool showDebugRays = false;

    private Camera _camera;

    private RaycastHit hit;
    private Vector3 hitPoint;

    void Start() {
        player = GetComponent<Player>();
        _camera = player._camera.GetComponent<CameraModules.CameraMain>()._camera;
    }

    void Update() {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);

        Ray ray = _camera.ViewportPointToRay(rayOrigin);

        if (showDebugRays) Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, maxDistance) && hit.collider.gameObject.tag != "Player") {
            hitPoint = hit.point;
            if (showDebugRays) Debug.DrawLine(arm.transform.position, hit.point, Color.green);
        } else {
            hitPoint = ray.origin + ray.direction * maxDistance;
            if (showDebugRays) Debug.DrawLine(arm.transform.position, ray.origin + ray.direction * maxDistance, Color.green);
        }

        arm.transform.LookAt(hitPoint);
    }
}
