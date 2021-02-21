using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    bool clicked;
    Vector3 distDifference;
    public bool foundSlot;
    public Slot selectedPin;

    public Sprite initialGear, finalGear;
    [HideInInspector] public enum Mode { INITIAL_SLOT, FINAL_SLOT, NOWHERE }
    public Mode currentMode;

    private void Update()
    {
        if (clicked)
        {
            Vector3 pos = Input.mousePosition + distDifference;

            transform.position = pos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //foundSlot = false;
        if (selectedPin) selectedPin.occupied = false;
        distDifference = transform.position - Input.mousePosition;
        clicked = true;
        currentMode = Mode.NOWHERE;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;

        if (foundSlot) Slot(selectedPin);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("InitialSlot") || other.CompareTag("FinalSlot"))
            && !other.GetComponent<Slot>().occupied)
        {
            foundSlot = true;
            selectedPin = other.GetComponent<Slot>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("InitialSlot") || other.CompareTag("FinalSlot"))
            && !other.GetComponent<Slot>().occupied)
        {
            if (other.gameObject == selectedPin.gameObject)
            {
                foundSlot = false;
                selectedPin = null;
            }
        }
    }

    public void Slot(Slot go)
    {
        if (go.CompareTag("InitialSlot"))
        {
            go.occupied = true;
            currentMode = Mode.INITIAL_SLOT;
            transform.position = go.transform.position;
            GetComponent<RectTransform>().sizeDelta = new Vector2(68f, 68f);
            GetComponent<SphereCollider>().radius = 27.5f;
            GetComponent<Image>().sprite = initialGear;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = target;
        }
        else if (go.CompareTag("FinalSlot"))
        {
            go.occupied = true;
            currentMode = Mode.FINAL_SLOT;
            transform.position = go.transform.position;
            GetComponent<RectTransform>().sizeDelta = new Vector2(110f, 110f);
            GetComponent<SphereCollider>().radius = 45f;
            GetComponent<Image>().sprite = finalGear;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = target;
        }
    }
}
