using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.05f, 1.05f, 1), 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1f, 1f, 1), 0.2f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(0.95f, 0.95f, 1), 0f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.05f, 1.05f, 1), 0.1f);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(gameObject);
        foreach (Transform child in transform)
        {
            DOTween.Kill(child);
        }
    }
}
