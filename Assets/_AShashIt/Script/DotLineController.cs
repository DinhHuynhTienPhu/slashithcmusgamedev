using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotLineController : MonoBehaviour
{
    [SerializeField]
    public float offset = .62f;
    public GameObject dot;
    float myLength;
    public void SetLength(float length) {
        if (length < 0.0001f) return;
        length -= 0.1f/3.425f;
        transform.localScale = new Vector3(-length, transform.localScale.y, 0);
        dot.transform.localScale = new Vector3(1/3.425f / length, .5f, 0);
        myLength = length;
    }
    private void Start()
    {
        //Animation();
    }
    void Animation() {
        try
        {
            float moveOffset = offset / myLength / 3.425f;
            dot.transform.DOLocalMoveX(moveOffset, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                dot.transform.DOLocalMoveX(-moveOffset, 0);
                Animation();
            });
        }
        catch { }
        //while (Vector3.Distance(dotPos, dot.transform.position) < 1) {
        //    yield return new WaitForEndOfFrame();
        //    dot.transform.localPosition = new Vector3(dot.transform.localPosition.x + 0.0005f, dot.transform.localPosition.y, dot.transform.localPosition.z);
        //}
        //dot.transform.localPosition = dotPos;
        //StartCoroutine(Animation(dotPos));
    }
    public void DrawLine(Vector3 A,Vector3 B)

    {
        Vector3 pos = new Vector3 ((A.x + B.x) / 2, (A.y + B.y) / 2, 0);
        transform.position = pos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0,  CalculateAngle(A, B));
        SetLength(Vector3.Distance(A, B) / 10);
        gameObject.SetActive(true);

    }


    public  float CalculateAngle(Vector3 A,Vector3 B)
    {
        double delta_x = B.x - A.x;
        double delta_y = B.y - A.y;
        float theta_radians =  Mathf.Atan2((float)delta_y, (float)delta_x) * 180 / 3.14159f;
        return theta_radians;
    }
}
