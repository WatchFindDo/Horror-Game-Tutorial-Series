using UnityEngine.UI;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

	[SerializeField] private GameObject         invHolder       = null;
    [SerializeField] private bool               usingInv        = false;
    [SerializeField] private GameObject         NoData          = null;

    public static InventorySystem               invSystem;
    private Inventory                           inv;

    void Awake ()
    {
        //Set the singleton.
        invSystem = this;
    }

    void Start ()
    {
        //Set the 'Inventory' reference.
        inv = Inventory.inventory;
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

        CheckItemsCount();

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

    public void CheckItemsCount ()
    {
        bool noItems = true;
        for (int i = 0; i < inv.invSlots.Length; i++)
        {
            if (inv.invSlots[i].gameObject.activeSelf)
            {
                noItems = false;
            }
        }

        if (noItems)
        {
            NoData.SetActive(true);
        }
        else
        {
            NoData.SetActive(false);
        }
    }
}