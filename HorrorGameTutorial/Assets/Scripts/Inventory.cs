using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public static Inventory inventory;

    public bool[] items = { false, false };
    public int[] collectables = {0, 0};
    public int amountOfItems; //Amount of items currently inside the invetory.

    [SerializeField] private Text pickUpText;
    public RectTransform[] invSlots;

    [Header("Audio (Pick Up Sounds)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    [Header("Transform's")]
    [SerializeField] Transform dropPos;

    [Header("Items")]
    [SerializeField] GameObject[] itemsPickUps;

    [Header("References")]
    private useFlashlight flashlightScript;

    void Awake ()
    {
        inventory = this;
    }

    void Start ()
    {
        flashlightScript = useFlashlight.instance;
    }
	
	public void AddItem (string ItemID, GameObject Object)
    {
        int amount = Object.GetComponent<CheckCount>().amount;

		if (ItemID == TagManager.flashlight) // item 0
        {
            items[0] = true;
            flashlightScript.CheckStartup();
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
        amountOfItems++;
        slotData data = invSlots[item].GetComponent<slotData>();

        data.amount += amount;
        data.amountText.text = "<color=#128CE3FF>Amount:</color> " + data.amount + "x";

        audioSource.clip = audioClips[item];
        audioSource.Play();
    }

    private void textAnimation (string ItemID, int amount)
    {
        pickUpText.text = "Picked up a " + ItemID + " (" + amount + "x)";

        pickUpText.gameObject.GetComponent<Animation>().Stop();
        pickUpText.gameObject.GetComponent<Animation>().Play();

        Debug.Log(ItemID + " Added");
    }

    public void DropItem (int item)
    {        
        invSlots[item].gameObject.SetActive(false);
        amountOfItems--;
        GameObject _item = Instantiate(itemsPickUps[item], dropPos.position, Quaternion.identity, null);
        if(_item.GetComponent<Rigidbody>() != null)
        {
            _item.GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
        }
        InventorySystem.invSystem.HideInv(1);
    }
}