using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    [HideInInspector] public bool inGameFunction;

    private CharacterController charController;
    private FirstPersonController firstPersonController;
    private GameObject firstPersonCharacter;

    private Blur blur;

    void Awake ()
    {
        gameManager = this;
    }

    void Start ()
    {
        SetReferences();

        UpdateMotion(1);
        LockCursor();
    }

    private void SetReferences ()
    {
        charController = FindObjectOfType<CharacterController>();
        firstPersonController = FindObjectOfType<FirstPersonController>();

        firstPersonCharacter = GameObject.Find("FirstPersonCharacter").gameObject;
        blur = firstPersonCharacter.GetComponent<Blur>();
    }

    public void LockCursor ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EnableControls ()
    {
        charController.enabled = true;
        firstPersonController.enabled = true;
    }

    public void DisableControls ()
    {
        charController.enabled = false;
        firstPersonController.enabled = false;
    }

    public void EnableBlurEffect ()
    {
        blur.enabled = true;
    }

    public void DisableBlurEffect ()
    {
        blur.enabled = false;
    }

    public void UpdateMotion (int time)
    {
        Time.timeScale = time;
    }
}
