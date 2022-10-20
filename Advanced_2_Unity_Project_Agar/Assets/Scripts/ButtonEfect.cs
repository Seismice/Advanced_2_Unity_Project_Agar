using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEfect : MonoBehaviour
{
    private bool scale = true;
    private float defaultX = 1f;
    private float defaultY = 1f;
    private float x = 1.1f;
    private float y = 1.1f;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnPointerEnter()
    {
        if (scale == true)
        {
            GetComponent<RectTransform>().localScale = new Vector2(x, y);
        }
    }

    public void OnPointerExit()
    {
        if (scale == true)
        {
            GetComponent<RectTransform>().localScale = new Vector2(defaultX, defaultY);
        }
    }
}
