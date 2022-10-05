using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utilities : MonoBehaviour
{
    public static void FormatTimeText(Text text, int time)
    {
        int h = time / 60 / 60;
        int m = time / 60 % 60;
        int s = time % 60;
        text.text = (h > 0 ? h + "h" : "") + (m > 0 ? m + "m" : "") + (s > 0 ? s + "s" : "");
    }
}
