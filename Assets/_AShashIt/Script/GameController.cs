using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    // Start is called before the first frame update
    public GameObject worldTextPrefab;
    public GameObject poofEffectPrefab;
    public GameObject hitEffectPrefab;

    public GameObject redPanel;
    public GameObject winPanel, losePanel,tutpanel;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHitPlay() {
        tutpanel.gameObject.SetActive(false);
    }
    public void ShowTextAt(string message, Vector3 pos = new Vector3(),float scale=1 ,Color color= new Color() ) {
        var gameObj = Instantiate(worldTextPrefab, pos, Quaternion.identity);
        gameObj.transform.localScale = gameObj.transform.localScale * scale;
        Text text = gameObj.GetComponentInChildren<Text>();
        if (color == new Color()) color = new Color(0.8f, 0.2f, 0.2f);
        text.color = color;
        text.text= message;
        gameObj.transform.DOMove(new Vector3(pos.x + Random.Range(-1f, 1f), pos.y + Random.Range(-1f, 1f), 0), 1f).OnComplete(() => Destroy(gameObj));
    }

    public void Win() {
        winPanel.gameObject.SetActive(true);
    }
    public void Lose() {
        losePanel.gameObject.SetActive(true);
    }

    public void OnHitReplay() {
        NextStage();
        SceneManager.LoadScene("GamePlay");
    }
    public void NextStage() {
        DataManager.Instance.stagePlay++;
        if (DataManager.Instance.stagePlay == GameInformation.Instance.stages[DataManager.Instance.levelPlay - 1].Count + 1) {
            DataManager.Instance.stagePlay = 1;
            DataManager.Instance.levelPlay++;
        }
    }
    public void ShowPoofFx(Vector3 pos, float scale=1) {
        var x = Instantiate(poofEffectPrefab, pos, Quaternion.identity);
        x.transform.localScale *= scale;
        Destroy(x, 1);
    }
    public void ShowHitFx(Vector3 pos, float scale=1) {
        var x = Instantiate(hitEffectPrefab, pos, Quaternion.identity);
        x.transform.localScale *= scale;
        Destroy(x, 1);
    }
    public void ShowRedPanel() {
        redPanel.SetActive(true);
        UnActiveGameObjectAfter(redPanel);
    }
    public void UnActiveGameObjectAfter( GameObject gObject, float sec = 0.2f) {
        StartCoroutine(UnActiveAfterCoroutine( gObject,sec));
    }
    IEnumerator UnActiveAfterCoroutine( GameObject gameObject, float sec = 0.2f) {
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }
    public void LoadStage() { 
        
    }

}
