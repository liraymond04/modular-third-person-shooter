using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public Controller3D controller;

    [Header("Health")]
    public float health;
    public float healthMin;
    public float healthMax;

    [Header("Mana")]
    public float mana;
    public float manaMin;
    public float manaMax;

    public float GetHealth() {
        return health;
    }

    public void SetHealth(float amount) {
        health = amount;
    }

    public float GetMana() {
        return mana;
    }

    public void SetMana(float amount) {
        mana = amount;
    }

    public void Heal(float amount) {
        health += amount;

        if (health > healthMax) {
            health = healthMax;
        }
    }

    public void Damage(float amount) {
        health -= amount;

        if (health < healthMin) {
            health = healthMin;
        }
    }

    public void Recover(float amount) {
        mana += amount;

        if (mana > manaMax) {
            mana = manaMax;
        }
    }

    public void Use(float amount) {
        mana -= amount;

        if (mana < manaMin) {
            mana = manaMin;
        }
    }

    public void Knockback(float strength, Vector3 pos, bool yVel, float yVelAngle) {
        if (controller != null) {
            controller.Knockback(strength, pos, yVel, yVelAngle);
        }
    }
}
