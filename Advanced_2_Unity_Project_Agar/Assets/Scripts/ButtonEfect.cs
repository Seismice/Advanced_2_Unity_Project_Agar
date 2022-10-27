using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void ChangeColorEnter()
    {
        GetComponent<Image>().color = new Color(0.3984375f, 0.66796875f, 0.921875f);
    }

    public void ChangeColorExit()
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }
}
