using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem pauseSystem;

    private bool isPaused = false;
    private CharacterController charController;
    private FirstPersonController firstPersonController;
    private GameObject firstPersonCharacter;
    private Blur blur;

    [SerializeField] private GameObject pauseMenuHolder;
    [SerializeField] private GameObject settingsHolder;

	void Start ()
    {
        pauseSystem = this;

        charController = FindObjectOfType<CharacterController>();
        firstPersonController = FindObjectOfType<FirstPersonController>();

        firstPersonCharacter = GameObject.Find("FirstPersonCharacter").gameObject;
        blur = firstPersonCharacter.GetComponent<Blur>();

        LockCursor();
    }
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;

            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;

            ResumeGame();
        }
    }

    public void PauseGame ()
    {
        charController.enabled = false;
        firstPersonController.enabled = false;
        blur.enabled = true;
        UnlockCursor();

        pauseMenuHolder.SetActive(true);
    }

    public void ResumeGame()
    {
        charController.enabled = true;
        firstPersonController.enabled = true;
        blur.enabled = false;
        LockCursor();

        pauseMenuHolder.SetActive(false);
        settingsHolder.SetActive(false);
    }
    
    void LockCursor ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
