using UnityEngine;

public class UseBattery : MonoBehaviour {

    private useFlashlight flashlightScript;
    private Inventory inv;
    private InventorySystem invSystem;

    [SerializeField] slotData data;

    void Start ()
    {
        flashlightScript = useFlashlight.instance;
        inv = Inventory.inventory;
        invSystem = InventorySystem.invSystem;
    }

	public void _UseBattery ()
    {
        if (inv.collectables[0] <= 0 || flashlightScript.batteryLife >= flashlightScript.maxLife || inv.items[0] == false) return;

        inv.collectables[0]--;
        data.amount = inv.collectables[0];
        data.amountText.text = "<color=#128CE3FF>Amount:</color> " + data.amount + "x";
        if (inv.collectables[0] <= 0)
        {
            data.gameObject.SetActive(false);
        }
        invSystem.CheckItemsCount();
        flashlightScript.UpdateBattery();
    }
}
