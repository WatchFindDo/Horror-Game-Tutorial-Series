using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour {

	public void closeInventory ()
    {
        InventorySystem.invSystem.HideInv(1);
    }
}
