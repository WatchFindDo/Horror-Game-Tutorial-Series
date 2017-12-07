using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class useFlashlight : MonoBehaviour {

    #region Variables
    public static useFlashlight instance;

    [Header("Parameters")]
    [SerializeField] private bool usingFlashlight = false; //Checking if we currently using flashlight.
    [SerializeField] private bool toggleFlashlight = false; //Checking if flashlight is currently on or off.
    public const int ID = 0;
    private bool canUse = true;
    [SerializeField] float waitTime = 2.5f;
    [SerializeField] bool ToggleFlashlightAfterReload = false;

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
    private Inventory inv;

    [Header("Colors")]
    [SerializeField] private Color equipedColor;
    [SerializeField] private Color equipColor;

    [Header("Battery Life - Parameters")]
    public bool reduceBatteryLife = true;
    [SerializeField] float reduceSpeed = 0.065f;
    [SerializeField] float lightReduceSpeed = 0.15f;
    [SerializeField] float lightDefaultIntesity = 5;
    [SerializeField] bool increaseLife = true;
    [SerializeField] float increaseSpeed = 0.05f;
    public float maxLife = 1;
    [HideInInspector] public float batteryLife = 1;
    [SerializeField] RectTransform bar;
    [SerializeField] GameObject batteryHolder;

    [Header("Coroutines")]
    private IEnumerator Wait_Coroutine;
    #endregion

    void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        inv = Inventory.inventory;

        //Set the light default intensity
        flashlightLight.intensity = lightDefaultIntesity;
    }

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.F)) 
        {
            switchState();
        }

        if (Input.GetKeyDown(KeyCode.L) && usingFlashlight && canUse) //Checking if we currently using the flashlight.
        {
            if (batteryLife <= 0) return;

            toggleFlashlight = !toggleFlashlight; //Switching the state.
            ToggleFlashlight(toggleFlashlight); //Checking the current flashlight state and updating the flashlight.
        }

        if (reduceBatteryLife && inv.items[0])
        {
            if (toggleFlashlight && canUse && usingFlashlight)
            {
                if(batteryLife > 0)
                {
                    batteryLife -= reduceSpeed * Time.deltaTime;
                    if (batteryLife <= 0)
                    {
                        batteryLife = 0;
                        if (!increaseLife)
                        {
                            batteryHolder.SetActive(false);
                        }
                        toggleFlashlight = false;
                        flashlightLight.gameObject.GetComponent<Animation>().Play("BatteryOutAnimation");
                        if (increaseLife)
                        {
                            if (Wait_Coroutine != null)
                            {
                                StopCoroutine(Wait_Coroutine);
                            }
                            Wait_Coroutine = WaitTillAble(waitTime);
                            StartCoroutine(Wait_Coroutine);
                        }
                    }
                    if (batteryLife < 0.3f) // At which point start to reduce light.
                    {
                        flashlightLight.intensity -= lightReduceSpeed * Time.deltaTime;
                    }
                    bar.localScale = new Vector3(batteryLife, 1, 1);
                }
            }
            else if (!toggleFlashlight || !usingFlashlight)
            {
                if (increaseLife)
                {
                    if (batteryLife < maxLife)
                    {
                        batteryLife += increaseSpeed * Time.deltaTime;
                        if (batteryLife >= maxLife)
                        {
                            batteryLife = maxLife;
                        }
                        if (batteryLife > 0.3f) // At which point start to reduce light.
                        {
                            if(flashlightLight.intensity < lightDefaultIntesity)
                            {
                                flashlightLight.intensity += lightReduceSpeed * Time.deltaTime;
                                if(flashlightLight.intensity >= lightDefaultIntesity)
                                {
                                    flashlightLight.intensity = lightDefaultIntesity;
                                }
                            }
                        }
                        bar.localScale = new Vector3(batteryLife, 1, 1);
                    }

                    if (!toggleFlashlight || !usingFlashlight)
                    {
                        if (batteryLife >= maxLife)
                        {
                            batteryHolder.SetActive(false);
                        }
                    }
                }
            }
        }
	}

    public void switchState ()
    {
        if (!inv.items[0]) return; //Checking if we currently picked up the flashlight, If not we return from this function

        usingFlashlight = !usingFlashlight; //Switching the state.
        updateFlashlight(usingFlashlight); //Checking the current flashlight state and updating the flashlight.
    }

    void updateFlashlight (bool state)
    {
        UsingFlashlight(state);
        if (state)
        {
            if (toggleFlashlight)
            {
                batteryHolder.SetActive(true);
            }
        }
        else
        {
            if (!increaseLife && inv.items[0])
            {
                batteryHolder.SetActive(false);
            }
            else if (!inv.items[0])
            {
                batteryHolder.SetActive(false);
            }
        }
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
        if (state)
        {
            batteryHolder.SetActive(true);
            flashlightLight.enabled = true;
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else
        {
            if (!increaseLife)
            {
                batteryHolder.SetActive(false);
            }
            flashlightLight.enabled = false;
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
    }

    public void dropItem ()
    {
        inv.items[0] = false;
        usingFlashlight = false;
        updateFlashlight(false);
        inv.DropItem(ID);
    }

    private IEnumerator WaitTillAble (float waitTime)
    {
        canUse = false;
        yield return new WaitForSeconds(waitTime);
        canUse = true;
        flashlightLight.intensity = lightDefaultIntesity;
    }

    public void UpdateBattery ()
    {
        batteryLife = maxLife;
        bar.localScale = new Vector3(1, 1, 1);
        flashlightLight.intensity = lightDefaultIntesity;

        if (Wait_Coroutine != null)
        {
            StopCoroutine(Wait_Coroutine);
        }
        canUse = true;
        
        if (ToggleFlashlightAfterReload)
        {
            if (usingFlashlight)
            {
                toggleFlashlight = true;
                ToggleFlashlight(toggleFlashlight);
            }
        }
        else
        {
            if (!toggleFlashlight || !usingFlashlight)
            {
                if (increaseLife)
                {
                    batteryHolder.SetActive(false);
                }
            }
        }
    }

    public void CheckStartup ()
    {
        if (increaseLife && batteryLife < maxLife)
        {
            batteryHolder.SetActive(true);
        }
    }
}