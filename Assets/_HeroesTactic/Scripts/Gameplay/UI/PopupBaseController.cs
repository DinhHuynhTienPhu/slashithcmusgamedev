using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class PopupBaseController : MonoBehaviour
{
    public Action onClosed;
    public Image imgBackground;
    public Transform panelContent;
    public bool backButtonHandle = true;
    
    private Action onHideFinished;
    private bool isTemporaryHide = false;

    protected void Show(Action onFinished = null)
    {
        AudioController.Play("switch_button_push_small_03");
        isTemporaryHide = false;

        gameObject.SetActive(true);
        StartCoroutine(ShowAnimation());

        if (backButtonHandle)
        {
            PressBackKeyController.instance.RegisterNewCallback(OnCloseClicked);
        }

        //TODO: play sound open
        //		AudioController.Play(GameInformation.Instance.soundCollection.popupOpen.name);
    }

    protected void Hide(Action onFinished = null)
    {
        AudioController.Play("switch_button_push_small_03");
        isTemporaryHide = false;
        onHideFinished = onFinished;

        StartCoroutine(HideAnimation(OnHideFinished));
        
        if (backButtonHandle)
        {
            PressBackKeyController.instance.RemoveCurrentCallback(OnCloseClicked);
        }

        //TODO: play sound close
        //		AudioController.Play(GameInformation.Instance.soundCollection.popupClose.name);

    }

    // HideTemporary mean: it will be hidden on screen, but onClosed event will not be triggered
    protected void HideTemporary(Action onFinished = null)
    {
        isTemporaryHide = true;
        onHideFinished = onFinished;

        StartCoroutine(HideAnimation(OnHideFinished));

        //TODO: play sound close
        //		AudioController.Play(GameInformation.Instance.soundCollection.popupClose.name);
    }

    public virtual void OnCloseClicked()
    {
        Hide(null);
    }
    public virtual void OnOpenClicked() {
       this.Show();
    }

    void OnHideFinished()
    {
        gameObject.SetActive(false);
        if (!isTemporaryHide)
        {
            if (onClosed != null)
            {
                Action tempOnClosed = onClosed;
                onClosed = null;
                tempOnClosed();
            }
        }

        if (onHideFinished != null)
        {
            onHideFinished();
            onHideFinished = null;
        }
    }

    private IEnumerator ShowAnimation(System.Action onFinished = null, bool isIndependentUpdate = false)
    {

        BlockInteractionCanvasController.Instance.Show();
        List<Tweener> tweens = new List<Tweener>();

        // Sometime, gameObject.setActive (true) affect the anim
        if (imgBackground != null)
        {
            Color c = imgBackground.color;
            c.a = 0;
            imgBackground.color = c;
        }

        Vector3 cachedPanelContentLocalPosition = Vector3.zero;
        if (panelContent != null)
        {
            cachedPanelContentLocalPosition = panelContent.transform.localPosition;
            panelContent.localPosition = new Vector3(99999, 99999);

            Vector3 localScale = panelContent.localScale;
            localScale.y = 0.01f;
            panelContent.localScale = localScale;
        }

        yield return null;

        if (panelContent != null)
        {
            panelContent.localPosition = cachedPanelContentLocalPosition;

            var zoomPanel = panelContent.DOScaleY(1f, 0.22f)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate)
                .SetEase(Ease.OutQuart);
            tweens.Add(zoomPanel);
        }

        if (imgBackground != null)
        {
            var fadeBackground = imgBackground.DOFade(255f / 255f, 0.22f)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate)
                .SetEase(Ease.OutQuart);
            tweens.Add(fadeBackground);
        }

        for (int i = 0; i < tweens.Count; i++)
        {
            if (tweens[i].IsActive())
                yield return tweens[i].WaitForCompletion();
        }

        yield return null;
        BlockInteractionCanvasController.Instance.Hide();
        if (onFinished != null)
            onFinished();
    }

    private IEnumerator HideAnimation(System.Action onFinished = null, bool isIndependentUpdate = false)
    {
        BlockInteractionCanvasController.Instance.Show();
        List<Tweener> tweens = new List<Tweener>();

        if (panelContent != null)
        {
            Vector3 localScale = panelContent.localScale;
            localScale.y = 1;
            panelContent.localScale = localScale;
            var zoomPanel = panelContent.DOScaleY(0.01f, 0.17f)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate)
                .SetEase(Ease.InCubic);
            tweens.Add(zoomPanel);
        }

        if (imgBackground != null)
        {
            Color c = imgBackground.color;
            c.a = 170f / 255f;
            imgBackground.color = c;

            var fadeBackground = imgBackground.DOFade(0f, 0.17f)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate)
                .SetEase(Ease.InCubic);
            tweens.Add(fadeBackground);
        }

        for (int i = 0; i < tweens.Count; i++)
        {
            if (tweens[i].IsActive())
                yield return tweens[i].WaitForCompletion();
        }

        Vector3 cachedPanelContentLocalPosition = Vector3.zero;
        if (panelContent != null)
        {
            cachedPanelContentLocalPosition = panelContent.transform.localPosition;
            panelContent.localPosition = new Vector3(99999, 99999);
        }

        yield return null;
        if (panelContent != null) panelContent.localPosition = cachedPanelContentLocalPosition;

        BlockInteractionCanvasController.Instance.Hide();
        if (onFinished != null)
        {
            onFinished();
        }
    }
}
