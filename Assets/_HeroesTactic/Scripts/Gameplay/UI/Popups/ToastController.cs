using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ToastController : MonoBehaviour {
	public Text textInfo;
	public Animator myAnimator;
    float y;
    private void Start()
    {
        y = gameObject.transform.localPosition.y;
    }

    public void Show(string mess="null") {

        gameObject.SetActive(true);
        StopAllCoroutines();
        if (SceneManager.GetActiveScene().name=="GamePlay") gameObject.transform.DOLocalMoveY(y-70f, 0).OnComplete(() => gameObject.transform.DOLocalMoveY(y, .3f).SetEase(Ease.Linear));
        else gameObject.transform.DOMoveY(-70f, 0).OnComplete(() => gameObject.transform.DOMoveY(70, .3f).SetEase(Ease.Linear));
        textInfo.text = mess;
        StartCoroutine(WaitToClose());
    }
    IEnumerator WaitToClose() {
        yield return new WaitForSeconds(2f);
        Close();

    }
    public void Close() {

        if(SceneManager.GetActiveScene().name=="GamePlay")gameObject.transform.DOLocalMoveY(y-70, .3f).OnComplete(()=> gameObject.SetActive(false));//.OnComplete(() => gameObject.transform.DOMoveY(70f, 0f));
        else gameObject.transform.DOMoveY(-70, .3f).OnComplete(() => gameObject.SetActive(false));//.OnComplete(() => gameObject.transform.DOMoveY(70f, 0f));

    }
}
