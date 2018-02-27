using UnityEngine;

public class InventoryButtons : MonoBehaviour {

	public void closeInventory ()
    {
        //Hide inventory
        InventorySystem.invSystem.HideInv(1);
    }
}