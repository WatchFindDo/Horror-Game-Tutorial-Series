using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour {

    [SerializeField] private GameObject settingsHolder;
	[SerializeField] private GameObject pauseMenuHolder;

	public void ResumeGame ()
    {
        PauseSystem.pauseSystem.ResumeGame();
	}

    public void Settings ()
    {
        settingsHolder.SetActive(true);
        pauseMenuHolder.SetActive(false);
    }

    public void SettingsBack()
    {
        settingsHolder.SetActive(false);
        pauseMenuHolder.SetActive(true);
    }

    public void SettingsApply ()
    {
        settingsHolder.SetActive(false);
        pauseMenuHolder.SetActive(true);
    }

    public void MainMenu ()
    {
        Debug.Log("Go to Main Menu.");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
