using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using UnityEngine.SceneManagement;


public class SettingController : PopupBaseController
{

    public Slider sound;
    public Slider music;

    public Toggle toggleEnglish;
    public Toggle toggleVietnamese;
    public Toggle togglePortuguese;
    public Toggle toggleRussian;

    const string kVietnameseCode = "vi";
    const string kEnglishCode = "en";
    const string kPortugeseCode = "pt";
    const string kRussianCode = "ru";
    string initialLanguageCode;

    bool userChangedLanguge = false;
    // bool opened = false;

    private void Start()
    {
        Debug.Log("sound volume: " + DataManager.Instance.soundvolume);
        Debug.Log("music volume: " + DataManager.Instance.musicvolume);
        sound.value = DataManager.Instance.soundvolume;
        music.value = DataManager.Instance.musicvolume;

        AudioController.SetCategoryVolume("SFX",(float) sound.value/100);
        AudioController.SetCategoryVolume("Music",(float) music.value/100);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Debug.Log("setting panel enabled");
        SetUI();
    }
    
    void TestShowValues() {

        Debug.Log("sound volume: " + DataManager.Instance.soundvolume);
        Debug.Log("music volume: " + DataManager.Instance.musicvolume);
    }

    public void SetUI()
    {

        Debug.Log("sound volume: "+DataManager.Instance.soundvolume);
        Debug.Log("music volume: " + DataManager.Instance.musicvolume);
        sound.value = DataManager.Instance.soundvolume ;
        music.value = DataManager.Instance.musicvolume ;

        AudioController.SetCategoryVolume("SFX",(float)sound.value/100f );
        AudioController.SetCategoryVolume("Music", (float)music.value/100f);


        initialLanguageCode = LocalizationManager.CurrentLanguageCode;
        Debug.Log(initialLanguageCode);


        if (initialLanguageCode == kVietnameseCode)
        {
            toggleEnglish.isOn = false;
            toggleVietnamese.isOn = true;
          if(togglePortuguese!=null)  togglePortuguese.isOn = false;
          if(toggleRussian!=null)  toggleRussian.isOn = false;
        }
        else if (initialLanguageCode == kEnglishCode)
        {
            toggleEnglish.isOn = true;
            toggleVietnamese.isOn = false;
            if (togglePortuguese != null) togglePortuguese.isOn = false;
            if (toggleRussian != null) toggleRussian.isOn = false;
        }
        else if (initialLanguageCode == kPortugeseCode)
        {
            toggleEnglish.isOn = false;
            toggleVietnamese.isOn = false;
            if (togglePortuguese != null) togglePortuguese.isOn = true;
            if (toggleRussian != null) toggleRussian.isOn = false;
        }
        else
        {
            toggleEnglish.isOn = false;
            toggleVietnamese.isOn = false;
            if (togglePortuguese != null) togglePortuguese.isOn = false;
            if (toggleRussian != null) toggleRussian.isOn = true;
        }


    }

    private void OnDisable()
    {
        string currentLanguageCode;
        if (toggleEnglish.isOn) currentLanguageCode = kEnglishCode;
        else if (toggleVietnamese.isOn) currentLanguageCode = kVietnameseCode;
        else if (togglePortuguese.isOn) currentLanguageCode = kPortugeseCode;
        else currentLanguageCode = kRussianCode;

        if (userChangedLanguge)
        {
            //LocalizationManager.CurrentLanguageCode = currentLanguageCode;
            //PopupManager.Instance.CreatePopupMessage(LocalizationManager.GetTermTranslation("Common/LanguageChanged"),
            //    LocalizationManager.GetTermTranslation("Common/LanguageChangedDesc"),
            //    LocalizationManager.GetTermTranslation("Common/OK"),
            //    () => {

            //    });

            
            Invoke("ReloadScene", 0.1f);
            return;
        }

    }

    void ReloadScene() {
        SceneLoader.LoadLevel(SceneLoader.SceneMainMenu);
    }
    public void OnSoundValueChanged(float value)
    {
        Debug.Log("value changed:" +value);
        DataManager.Instance.soundvolume = (int)value;
        AudioController.SetCategoryVolume("SFX", value/100f);
    }

    public void OnMusicValueChanged(float value)
    {
        Debug.Log("value changed: "+value);
        DataManager.Instance.musicvolume = (int)value;
        AudioController.SetCategoryVolume("Music", value/100f);
    }


    public void ViToogleChangeValue(bool value) {
        if (value)
        {
            if (LocalizationManager.CurrentLanguageCode != kVietnameseCode) userChangedLanguge = true;
            LocalizationManager.CurrentLanguageCode = kVietnameseCode;
            
        }
        SetUI();
    }
    public void EnToogleChangeValue(bool value)
    {
        if (value)
        {
            if (LocalizationManager.CurrentLanguageCode != kEnglishCode) userChangedLanguge = true;
            LocalizationManager.CurrentLanguageCode = kEnglishCode;
        }

        SetUI();
    }
    public void PtToogleChangeValue(bool value)
    {
        if (value)
        {
            if (LocalizationManager.CurrentLanguageCode != kPortugeseCode) userChangedLanguge = true;
            LocalizationManager.CurrentLanguageCode = kPortugeseCode;
        }
        SetUI();
    }
    public void RuToogleChangeValue(bool value)
    {
        if (value)
        {
            if (LocalizationManager.CurrentLanguageCode != kRussianCode) userChangedLanguge = true;
            LocalizationManager.CurrentLanguageCode = kRussianCode;
        }

        SetUI();
    }

    public void Show() {
        base.Show();
    }
    public void Hide() {
        base.Hide();
    }

}
