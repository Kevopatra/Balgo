using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonAddition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool HasText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySFXEvent PSFXE = new PlaySFXEvent();
        PSFXE.ArrayName = "HoverOverButton";
        EventSystem.FireEvent(PSFXE);
        if (HasText)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HasText)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void Start()
    {
        //UnityEngine.EventSystems.EventSystem ES = GameObject.Find("UES").GetComponent<UnityEngine.EventSystems.EventSystem>();
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //ES.SetSelectedGameObject(null);
            PlaySFXEvent PSFXE = new PlaySFXEvent();
            PSFXE.ArrayName = "Button";
            EventSystem.FireEvent(PSFXE);
            if (HasText)
            {
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            }
        });
    }
}