using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    public List<Vector3> movePoses = new List<Vector3>();
    public Vector3 myCenter;
    public SpriteRenderer spriteRenderer;
    public float mySpeed = 10;
    public float stayDelay = 5f;
    public float heath = 2;
    public GameObject swordfx;
    int desnumber = 0;
    Vector3 oldPos, newPos;
    
    public bool isTackingDamage = false;

    
    // Start is called before the first frame update
    void Start()
    {
        myCenter = transform.position;
        StartCoroutine(MoveToDes());
        gameObject.tag = "enemy";
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        oldPos = newPos;
        newPos = transform.position;
        if (oldPos.x > newPos.x)
            transform.localScale = new Vector3(Mathf.Abs( transform.localScale.x)*1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(Mathf.Abs( transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);

        if (heath <= 0) {
            GameController.Instance.ShowPoofFx(transform.position);
            Destroy(gameObject);
        }

    }
    IEnumerator MoveToDes() {
        float timer = 0;
        Vector3 endPos = movePoses[desnumber]+myCenter;
        while (timer<stayDelay)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * mySpeed);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        desnumber++;
        if (desnumber == movePoses.Count) desnumber = 0;
        StartCoroutine(MoveToDes());
    }
    public void TakeDamage(int dam=1) {
        //if (isTackingDamage) return;

        isTackingDamage = true;
        heath -= dam;
        spriteRenderer.color = Color.red;
        swordfx.SetActive(true);
        swordfx.transform.DOShakeScale(0.27f);
        transform.DOShakePosition(0.3f,0.5f).OnComplete(()=> {
            isTackingDamage = false;
            spriteRenderer.color = Color.white;
            swordfx.SetActive(false);
        });
        GameController.Instance.ShowTextAt("-" + dam, transform.position);
    }
}
