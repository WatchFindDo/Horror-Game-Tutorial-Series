using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem pauseSystem;

    private bool isPaused = false;

    [SerializeField] private GameObject pauseMenuHolder;
    [SerializeField] private GameObject settingsHolder;

	void Start ()
    {
        pauseSystem = this;
    }
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            if (GameManager.gameManager.inGameFunction) return;

            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }
    }

    public void PauseGame ()
    {
        isPaused = true;
        GameManager.gameManager.inGameFunction = true;

        GameManager.gameManager.UnlockCursor();
        GameManager.gameManager.EnableBlurEffect();
        GameManager.gameManager.UpdateMotion(0);
        GameManager.gameManager.DisableControls();

        pauseMenuHolder.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        GameManager.gameManager.inGameFunction = false;

        GameManager.gameManager.LockCursor();
        GameManager.gameManager.DisableBlurEffect();
        GameManager.gameManager.UpdateMotion(1);
        GameManager.gameManager.EnableControls();

        pauseMenuHolder.SetActive(false);
        settingsHolder.SetActive(false);
    }
    
    
}
