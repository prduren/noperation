using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // public static bool FullScreenMode = true;
    public GameObject optionsObject;
    public GameObject optionsButton;
    public GameObject quitButton;
    public GameObject pauseMenuImage;
    string currentSceneName;

    void Start() {
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame() {
        SceneManager.LoadScene("L1");
    }

    public void Options() {
        if (optionsObject.activeSelf == true) {
            optionsObject.SetActive(false);
        } else if (optionsObject.activeSelf == false) {
            optionsObject.SetActive(true);
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void ToggleFullScreen() {
        if (Screen.fullScreen) {
            SM.FullScreenMode = false;
            Screen.fullScreen = false;
        } else if (!Screen.fullScreen) {
            SM.FullScreenMode = true;
            Screen.fullScreen = true;
        }   
    }

/*
// enable all this once you want menu for in-game. Prob need menu object in each scene with Menu.cs attached.

    void Update() {
        if (currentSceneName != "Menu") {
            if (!ApplicationData.gamePaused) {
                // turn on pause menu things
                pauseMenuImage.SetActive(false);
                optionsButton.SetActive(false);
                optionsObject.SetActive(false);
                quitButton.SetActive(false);

            } else if (ApplicationData.gamePaused) {
                pauseMenuImage.SetActive(true);
                optionsButton.SetActive(true);
                quitButton.SetActive(true);
            }
        }
    }
*/
}
