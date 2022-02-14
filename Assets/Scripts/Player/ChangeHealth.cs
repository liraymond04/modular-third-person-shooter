using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : MonoBehaviour {
    
    public float amount;

    public float knockbackStrength;
    public bool yVel;
    public float yVelAngle;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            HealthManager health = other.gameObject.GetComponent<HealthManager>();
            if (amount > 0) {
                health.Heal(amount);
            } else if (amount < 0) {
                health.Damage(-amount);
                health.Knockback(knockbackStrength, transform.position, yVel, yVelAngle);
            }
        }
    }
}
