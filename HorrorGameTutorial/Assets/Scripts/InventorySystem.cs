using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

	[SerializeField] private GameObject invHolder;
    [SerializeField] private bool usingInv;

    public static InventorySystem invSystem;

    void Awake ()
    {
        invSystem = this;
    }

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.I) && !usingInv)
        {
            if (GameManager.gameManager.inGameFunction) return;

            ShowInv(0);
        }
        else if (Input.GetKeyDown(KeyCode.I) && usingInv || Input.GetKeyDown(KeyCode.Escape) && usingInv)
        {
            HideInv(1);
        }
    }

    public void ShowInv (int time)
    {
        usingInv = true;
        GameManager.gameManager.inGameFunction = true;

        invHolder.SetActive(true);

        GameManager.gameManager.UnlockCursor();
        GameManager.gameManager.EnableBlurEffect();
        GameManager.gameManager.UpdateMotion(time);
        GameManager.gameManager.DisableControls();
    }

    public void HideInv (int time)
    {
        usingInv = false;
        GameManager.gameManager.inGameFunction = false;

        invHolder.SetActive(false);

        GameManager.gameManager.LockCursor();
        GameManager.gameManager.DisableBlurEffect();
        GameManager.gameManager.UpdateMotion(time);
        GameManager.gameManager.EnableControls();
    }
}
