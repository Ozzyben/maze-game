using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Vector3 text_scaling = new Vector3(0.4f, 0.4f, 0.4f);


    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale += text_scaling;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale -= text_scaling;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        SceneManager.LoadScene("Game");

    }
}
