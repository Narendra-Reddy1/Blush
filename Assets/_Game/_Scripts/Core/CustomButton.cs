using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomButton : Button, IPointerDownHandler, IPointerUpHandler
{

    //[SerializeField]
    //private ButtonClickedEvent m_onButtonClick = new ButtonClickedEvent();
    [FormerlySerializedAs("OnRelease")]
    [SerializeField]
    private ButtonClickedEvent m_onRelease = new ButtonClickedEvent();

    //public ButtonClickedEvent OnButtonClick
    //{
    //    get
    //    {
    //        return m_onButtonClick;
    //    }
    //    set
    //    {
    //        m_onButtonClick = value;
    //    }
    //}
    public ButtonClickedEvent OnRelease
    {
        get
        {
            return m_onRelease;
        }
        set
        {
            m_onRelease = value;
        }
    }


    //public override void OnPointerDown(PointerEventData eventData)
    //{
    //    OnButtonClick?.Invoke();
    //    base.OnPointerDown(eventData);
    //}

    public override void OnPointerUp(PointerEventData eventData)
    {
        OnRelease?.Invoke();
        base.OnPointerUp(eventData);
    }
}
