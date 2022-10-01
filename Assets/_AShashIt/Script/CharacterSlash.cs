using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DG.Tweening;
using UnityEngine.UI;
public class CharacterSlash : MonoBehaviour
{
    public static CharacterSlash Instance;
    // Start is called before the first frame update
    public Vector3 startMousePos, endMousePos, characterEndPos;
    public SpriteRenderer mySpriteRendererAvt;
    public DotLineController dotline;
    public Slider hpSlider;
    public int hp = 10, maxhp = 10;

    
    bool touched = false;
   public bool isSlashing = false;

    Vector3 oldPos, newPos;
    void Start()
    {
        Instance = this;
        if (mySpriteRendererAvt == null) mySpriteRendererAvt = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            characterEndPos = transform.position - (endMousePos - startMousePos);

            dotline.DrawLine(transform.position, GetVector3WithPercent(transform.position,characterEndPos,0.5f));
        }

        if (Input.GetMouseButtonUp(0))
        {

            RaycastHit2D hit = Physics2D.Linecast(transform.position, characterEndPos, LayerMask.GetMask("wall"));

            if (hit)
            {
                characterEndPos = hit.point;
            }
            StartSlashing();
            #region damage enemy 
            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, characterEndPos);
            int enemyHits = 0;
            foreach (var h in hits)
            {
                var enemy = h.collider.gameObject.GetComponent<EnemyController>();
                if (enemy != null&&enemy.isTackingDamage==false)
                {
                    enemy.TakeDamage();
                    enemyHits++;
                }
            }
            if (enemyHits > 1) GameController.Instance.ShowTextAt("COMBO x" + enemyHits, characterEndPos, scale: 2, color: Color.yellow);
            #endregion
        }
        if (transform.position.x > 0)
           mySpriteRendererAvt.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        else
            mySpriteRendererAvt.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);

        hpSlider.maxValue = maxhp;
        hpSlider.value = hp;
        if (hp <= 0) GameController.Instance.Lose();
    }
    private void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(0, Mathf.Lerp(Camera.main.transform.position.y, transform.position.y + 1, Time.deltaTime * 10f), -10);
     
        oldPos = newPos;
        newPos = transform.position;
        if (oldPos == newPos)
        {
            StopSlashing();
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag.Contains("wall"))
        {
            Debug.Log("trigger wal");
            StopSlashing();
        }
        else if (collision.gameObject.tag.Contains("enemy"))
        {
            Debug.Log("touch enemy");
            if (isSlashing == false)
            {
                hp -= 1;
                Camera.main.transform.DOShakePosition(0.2f, 1);
                GameController.Instance.ShowRedPanel();
            }
        }
        else if (collision.gameObject.tag.Contains("winPortal"))
        {
            GameController.Instance.Win();
        }
    }
    public void StartSlashing()
    {
        touched = false;
        isSlashing = true;
        mySpriteRendererAvt.sortingOrder = -1000;
        GameController.Instance.ShowHitFx(transform.position);
        dotline.gameObject.SetActive(false);
        StartCoroutine(Slash());
    }
    public void StopSlashing()
    {
        if (isSlashing)
        {
            touched = true;
            isSlashing = false;
            mySpriteRendererAvt.sortingOrder = 10;
        }
    }
    public IEnumerator Slash() {
        while  (!touched && isSlashing)
        {
            transform.position = Vector3.Lerp(transform.position, characterEndPos, Time.deltaTime * 20);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    public Vector3 GetVector3WithPercent(Vector3 A, Vector3 B, float percent = 0.5f) {
        float x = (B.x - A.x) * percent;
        float y = (B.y - A.y) * percent;
        float z = (B.z - A.z) * percent;
        return new Vector3(A.x+x, A.y+y, A.z+z);
    }
}
