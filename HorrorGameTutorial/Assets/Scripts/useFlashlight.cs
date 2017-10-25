using UnityEngine;
using UnityEngine.UI;

public class useFlashlight : MonoBehaviour {

    public static useFlashlight instance;

    [Header("Parameters")]
    [SerializeField] private bool usingFlashlight = false; //Checking if we currently using flashlight.
    [SerializeField] private bool toggleFlashlight = false; //Checking if flashlight is currently on or off.
    public const int ID = 0;

    [Header("Lights")]
    [SerializeField] private Light flashlightLight;

    [Header("Audio")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;

    [Header("GameObject's")]
    [SerializeField] GameObject[] flashlightHolder;
    
    [Header("Inventory")]
    [SerializeField] private Text UIText;
    [SerializeField] private string equipedText = "<color=#128CE3FF>E</color>QUIPED";
    [SerializeField] private string equipText = "<color=#128CE3FF>E</color>QUIP";

    [Header("Colors")]
    [SerializeField] private Color equipedColor;
    [SerializeField] private Color equipColor;

    void Start ()
    {
        instance = this;
    }

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.F)) 
        {
            switchState();
        }

        if(Input.GetKeyDown(KeyCode.L) && usingFlashlight) //Checking if we currently using the flashlight.
        {
            toggleFlashlight = !toggleFlashlight; //Switching the state.
            ToggleFlashlight(toggleFlashlight); //Checking the current flashlight state and updating the flashlight.
        }
	}

    public void switchState ()
    {
        if (!Inventory.inventory.items[0]) return; //Checking if we currently picked up the flashlight, If not we return from this function

        usingFlashlight = !usingFlashlight; //Switching the state.
        updateFlashlight(usingFlashlight); //Checking the current flashlight state and updating the flashlight.
    }

    void updateFlashlight (bool state)
    {
        UsingFlashlight(state);
    }

    void UsingFlashlight (bool state)
    {
        for (int i = 0; i < flashlightHolder.Length; i++) // Enable every item inside the array.
        {
            flashlightHolder[i].SetActive(state);
        }

        UpdateInv(state);
    }

    void UpdateInv (bool state)
    {
        if (UIText == null) return; //Checking if text reference exists. If not we return from this function.

        if(state)
        {
            UIText.text = equipedText;
            UIText.color = equipedColor;
        }
        else
        {
            UIText.text = equipText;
            UIText.color = equipColor;
        }
    }

    void ToggleFlashlight (bool state)
    {
        if(state)
        {
            flashlightLight.enabled = true;
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else
        {
            flashlightLight.enabled = false;
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
    }

    public void dropItem ()
    {
        Inventory.inventory.items[0] = false;
        usingFlashlight = false;
        updateFlashlight(false);
        Inventory.inventory.DropItem(ID);
    }
}
