using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateAppStarController : MonoBehaviour
{
    [NonSerialized]
    public int index;
    public Action<int> onClick;
    public Image foreground;
    public Image background;

    public void SetData(int index, bool enabled)
    {
        this.index = index;
        foreground.gameObject.SetActive(enabled);
    }

    public void OnClick()
    {
        if (onClick != null) onClick(index);
    }
}
