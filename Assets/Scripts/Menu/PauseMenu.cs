using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject player;

    public bool hideCursor;
    public bool isCursorHidden;

    public bool isPaused;
    public GameObject pausePanel;

    private void Start() {
        isCursorHidden = hideCursor;

        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        playerInput .OnPausedDown += OnPauseHit;
    }

    private void Update() {
        if (hideCursor) {
            if (isPaused) {
                RevealCursor();
            } else {
                HideCursor();
            }
        }
    }

    public void OnPauseHit(object sender, EventArgs e) {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    public void Resume() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void HideCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isCursorHidden = true;
    }

    public void RevealCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isCursorHidden = false;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
