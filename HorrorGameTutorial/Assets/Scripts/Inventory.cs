using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public static Inventory inventory;

    public bool[] items = { false, false };
    public int[] collectables = {0, 0};

    [SerializeField] private Text pickUpText;
    public RectTransform[] invSlots;
    [SerializeField] private GameObject noData;

    void Start ()
    {
        inventory = this;
    }
	
	public void AddItem (string ItemID, GameObject Object)
    {
        int amount = Object.GetComponent<CheckCount>().amount;

		if(ItemID == TagManager.flashlight) // item 0
        {
            items[0] = true;
            createData(0, amount);
        }
        if (ItemID == TagManager.note) // item 1
        {
            collectables[1] += amount;
            createData(1, amount);
        }
        if (ItemID == TagManager.battery) // item 2
        {
            collectables[0] += amount;
            createData(2, amount);
        }  

        textAnimation(ItemID, amount);
    }

    private void createData (int item, int amount)
    {
        invSlots[item].gameObject.SetActive(true);

        slotData data = invSlots[item].GetComponent<slotData>();

        data.amount += amount;
        data.amountText.text = "<color=#128CE3FF>Amount:</color> " + data.amount + "x";

        if (noData.activeSelf)
        {
            noData.SetActive(false);
        }
    }

    private void textAnimation (string ItemID, int amount)
    {
        pickUpText.text = "Picked up a " + ItemID + " (" + amount + "x)";

        pickUpText.gameObject.GetComponent<Animation>().Stop();
        pickUpText.gameObject.GetComponent<Animation>().Play();

        Debug.Log(ItemID + " Added");
    }
}
