using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(HealthManager))]
public class HealthBar : MonoBehaviour {

    private HealthManager healthManager;

    public GameObject healthBar;
    private Slider healthSlider;

    private Slider manaSlider;
    public GameObject manaBar;

    private void Start() {
        healthManager = GetComponent<HealthManager>();

        if (healthBar != null) {
            healthSlider = healthBar.GetComponent<Slider>();
        }
        if (manaBar != null) {
            manaSlider = manaBar.GetComponent<Slider>();
        }
    }

    private void Update() {
        healthSlider.value = healthManager.health / healthManager.healthMax;
        manaSlider.value = healthManager.mana / healthManager.manaMax;
    }
}
