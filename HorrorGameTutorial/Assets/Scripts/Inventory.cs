using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public static Inventory inventory;

    public bool[] items = { false, false };
    public int[] collectables = {0, 0};

    [SerializeField] private Text pickUpText;

	void Start ()
    {
        inventory = this;
    }
	
	public void AddItem (string ItemID)
    {
		if(ItemID == TagManager.flashlight)
        {
            items[0] = true;
        }
        if (ItemID == TagManager.battery)
        {
            collectables[0]++;
        }
        if (ItemID == TagManager.note)
        {
            collectables[1]++;
        }

        textAnimation(ItemID);
    }

    private void textAnimation (string ItemID)
    {
        pickUpText.text = "Picked up a " + ItemID;

        pickUpText.gameObject.GetComponent<Animation>().Stop();
        pickUpText.gameObject.GetComponent<Animation>().Play();

        Debug.Log(ItemID + " Added");
    }
}
