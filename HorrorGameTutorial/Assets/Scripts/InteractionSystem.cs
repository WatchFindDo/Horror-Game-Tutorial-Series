using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour {

    //For raycast
    [SerializeField] private Camera mainCam;
    [SerializeField] private float interactDistance = 5.0f;

    private bool canInteract;

    private GameObject interactingGameObject;
    private string interactingObjectName;

    [Header("Object Color Palettes")]
    [SerializeField] private Color interactionColor = Color.red;
    [SerializeField] private Color defaultColor = Color.white;

    void Start ()
    {
        InvokeRepeating("search", 0f, 0.5f);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if(interactingObjectName == TagManager.flashlight)
            {
                Inventory.inventory.AddItem(interactingObjectName);

                clearData();
                return;
            }
            if (interactingObjectName == TagManager.battery)
            {
                Inventory.inventory.AddItem(interactingObjectName);

                clearData();
                return;
            }
            if (interactingObjectName == TagManager.note)
            {
                Inventory.inventory.AddItem(interactingObjectName);

                clearData();
                return;
            }
        }
	}

    private void search ()
    {
        RaycastHit hit;

        //Checking the distance and the layer of the object
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable") && hit.distance <= interactDistance)
        {
            resetData();
            canInteract = true;

            //Getting info
            interactingObjectName = hit.collider.tag;
            interactingGameObject = hit.transform.gameObject;

            interactingGameObject.GetComponent<Renderer>().material.color = interactionColor;
        }
        else
        {
            canInteract = false;
            resetData();
        }
    }

    void resetData ()
    {
        if (interactingGameObject == null) return;

        interactingGameObject.GetComponent<Renderer>().material.color = defaultColor;

        interactingObjectName = null;
        interactingGameObject = null;
    }

    void clearData ()
    {
        if (interactingGameObject != null)
        {
            Destroy(interactingGameObject);
        }
        interactingGameObject = null;
        interactingObjectName = null;
    }
}
