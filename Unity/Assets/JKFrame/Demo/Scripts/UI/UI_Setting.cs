using DG.Tweening;
using JKFrame;
using UnityEngine;
using UnityEngine.UI;

[UIWindowData(typeof(UI_Setting), true, "UI_Setting", 1)]
public class UI_Setting : UI_WindowBase
{
    private const string DataBag = "UI_Setting";
    private const string Btn = "Button";
    private const string Language = "Language";
    [SerializeField] CanvasGroup m_CGroup;
    [SerializeField] SelectableGroup m_SGroup;
    [SerializeField] Text m_TextSetting;
    [SerializeField] Text m_TextMusic;
    [SerializeField] Text m_TextMusicVolume;
    [SerializeField] Text m_TextSound;
    [SerializeField] Text m_TextSoundVolume;
    [SerializeField] Text m_TextUISound;
    [SerializeField] Text m_TextUISoundVolume;
    [SerializeField] Text m_TextTutorial;
    [SerializeField] Text m_TextQuality;
    [SerializeField] Text m_TextLow;
    [SerializeField] Text m_TextMedium;
    [SerializeField] Text m_TextHigh;
    [SerializeField] Text m_TextLanguage;
    [SerializeField] Text m_TextLabelLanguage;
    [SerializeField] Text m_TextVibration;
    [SerializeField] Text m_TextBtnSave;
    [SerializeField] Text m_TextBtnCancel;

    [SerializeField] BaseToggle m_ToggleMusicMute;
    [SerializeField] BaseSlider m_SliderMusicVolume;
    [SerializeField] BaseToggle m_ToggleSoundMute;
    [SerializeField] BaseSlider m_SliderSoundVolume;
    [SerializeField] BaseToggle m_ToggleUISoundMute;
    [SerializeField] BaseSlider m_SliderUISoundVolume;
    [SerializeField] BaseToggle m_ToggleTutorial;
    [SerializeField] BaseToggle[] m_ToggleQualitys;
    [SerializeField] BaseToggle m_ToggleGamepadVibration;

    [SerializeField] BaseButton m_BtnSave;
    [SerializeField] BaseButton m_BtnCancel;

    public override void Init()
    {
        base.Init();
        m_CGroup.alpha = 0;
    }

    SettingConfig m_Setting;
    SettingConfig m_SettingTemp;

    bool HasChange
    {
        get
        {
            if (m_SettingTemp.MusicIsMute == m_Setting.MusicIsMute && m_SettingTemp.MusicVolume == m_Setting.MusicVolume && m_SettingTemp.SoundIsMute == m_Setting.SoundIsMute &&
            m_SettingTemp.SoundVolume == m_Setting.SoundVolume && m_SettingTemp.UISoundIsMute == m_Setting.UISoundIsMute && m_SettingTemp.UISoundVolume == m_Setting.UISoundVolume &&
            m_SettingTemp.Quality == m_Setting.Quality && m_SettingTemp.Tutorial == m_Setting.Tutorial && m_SettingTemp.Language == m_Setting.Language &&
            m_SettingTemp.GamepadVibration == m_Setting.GamepadVibration)
                return false;
            return true;
        }
    }

    void SetAudioMusicMute(bool isMute)
    {
        //AudioManager.Instance.MusicIsMute = isMute;
        //m_SettingTemp.MusicIsMute = isMute;
    }

    void SetAudioMusicVolume(float volume)
    {
        //AudioManager.Instance.MusicVolume = volume;
        //m_SettingTemp.MusicVolume = volume;
    }

    void SetAudioSoundMute(bool isMute)
    {
        //AudioManager.Instance.SoundIsMute = isMute;
        //m_SettingTemp.SoundIsMute = isMute;
    }

    void SetAudioSoundVolume(float volume)
    {
        //AudioManager.Instance.SoundVolume = volume;
        //m_SettingTemp.SoundVolume = volume;
    }

    void SetAudioUISoundMute(bool isMute)
    {
        //AudioManager.Instance.UISoundIsMute = isMute;
        //m_SettingTemp.UISoundIsMute = isMute;
    }

    void SetAudioUISoundVolume(float volume)
    {
        //AudioManager.Instance.UISoundVolume = volume;
        //m_SettingTemp.UISoundVolume = volume;
    }

    void SetTutorial(bool isActive)
    {
        m_SettingTemp.Tutorial = isActive;
    }

    void SetQuality(int quality)
    {
        m_SettingTemp.Quality = quality;
    }

    void SetGamepadVibration(bool isActive)
    {
        m_SettingTemp.GamepadVibration = isActive;
    }

    void ResetAllSetting()
    {
        //AudioManager.Instance.MusicIsMute = m_Setting.MusicIsMute;
        //AudioManager.Instance.MusicVolume = m_Setting.MusicVolume;
        //AudioManager.Instance.SoundIsMute = m_Setting.SoundIsMute;
        //AudioManager.Instance.SoundVolume = m_Setting.SoundVolume;
        //AudioManager.Instance.UISoundIsMute = m_Setting.UISoundIsMute;
        //AudioManager.Instance.UISoundVolume = m_Setting.UISoundVolume;
        LocalizationSystem.CurrentLanguageType = (LanguageType)m_Setting.Language;
    }

    public override void OnShow()
    {
        m_Setting = ConfigManager.Instance.Setting;
        m_SettingTemp = new SettingConfig(m_Setting);
        base.OnShow();
        m_ToggleMusicMute.isOn = m_Setting.MusicIsMute;
        m_SliderMusicVolume.value = m_Setting.MusicVolume;
        m_ToggleSoundMute.isOn = m_Setting.SoundIsMute;
        m_SliderSoundVolume.value = m_Setting.SoundVolume;
        m_ToggleUISoundMute.isOn = m_Setting.UISoundIsMute;
        m_SliderUISoundVolume.value = m_Setting.UISoundVolume;
        m_ToggleTutorial.isOn = m_Setting.Tutorial;
        m_ToggleQualitys[m_Setting.Quality].isOn = true;
        m_ToggleGamepadVibration.isOn = m_Setting.GamepadVibration;
        m_ToggleMusicMute.onValueChanged.AddListener(SetAudioMusicMute);
        m_SliderMusicVolume.onValueChanged.AddListener(SetAudioMusicVolume);
        m_ToggleSoundMute.onValueChanged.AddListener(SetAudioSoundMute);
        m_SliderSoundVolume.onValueChanged.AddListener(SetAudioSoundVolume);
        m_ToggleUISoundMute.onValueChanged.AddListener(SetAudioUISoundMute);
        m_SliderUISoundVolume.onValueChanged.AddListener(SetAudioUISoundVolume);
        m_ToggleTutorial.onValueChanged.AddListener(SetTutorial);
        for (int i = 0; i < m_ToggleQualitys.Length; i++)
        {
            int qualityIndex = i;
            m_ToggleQualitys[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                    SetQuality(qualityIndex);
            });
        }
        m_ToggleGamepadVibration.onValueChanged.AddListener(SetGamepadVibration);
        SelectableSystem.AddSelectable(m_SGroup);
        m_CGroup.DOFade(1, 0.5f).OnComplete(delegate
        {
            JKInputSystem.AddListenerStart(OnClickSave);
            JKInputSystem.AddListenerCancel(OnClickCancel);
            m_BtnSave.onClick.AddListener(OnClickSave);
            m_BtnCancel.onClick.AddListener(OnClickCancel);
        });
    }

    void SetCGroup(bool isActive)
    {
        if (isActive)
        {
            m_CGroup.DOFade(1, 0.5f);
            return;
        }
        m_CGroup.DOFade(0, 0.5f);
    }

    public override void OnClose()
    {
        base.OnClose();
        m_ToggleMusicMute.onValueChanged.RemoveListener(SetAudioMusicMute);
        m_SliderMusicVolume.onValueChanged.RemoveListener(SetAudioMusicVolume);
        m_ToggleSoundMute.onValueChanged.RemoveListener(SetAudioSoundMute);
        m_SliderSoundVolume.onValueChanged.RemoveListener(SetAudioSoundVolume);
        m_ToggleUISoundMute.onValueChanged.RemoveListener(SetAudioUISoundMute);
        m_SliderUISoundVolume.onValueChanged.RemoveListener(SetAudioUISoundVolume);
        m_ToggleTutorial.onValueChanged.RemoveListener(SetTutorial);
        foreach (var toggleQuality in m_ToggleQualitys)
            toggleQuality.onValueChanged.RemoveAllListeners();
        m_ToggleGamepadVibration.onValueChanged.RemoveListener(SetGamepadVibration);
        SelectableSystem.Return();
    }

    public void OnClickLastLanguage()
    {
        LocalizationSystem.LastLanguage();
        m_SettingTemp.Language = (int)LocalizationSystem.CurrentLanguageType;
    }

    public void OnClickNextLanguage()
    {
        LocalizationSystem.NextLanguage();
        m_SettingTemp.Language = (int)LocalizationSystem.CurrentLanguageType;
    }

    public void SetLanguageSelectEvent(bool isActive)
    {
        LocalizationSystem.SetInputLanguage(isActive);
    }

    public void RemoveAllListener()
    {
        JKInputSystem.RemoveListenerStart(OnClickSave);
        JKInputSystem.RemoveListenerCancel(OnClickCancel);
        m_BtnSave.onClick.RemoveListener(OnClickSave);
        m_BtnCancel.onClick.RemoveListener(OnClickCancel);
    }

    public void AddAllListener()
    {
        JKInputSystem.AddListenerStart(OnClickSave);
        JKInputSystem.AddListenerCancel(OnClickCancel);
        m_BtnSave.onClick.AddListener(OnClickSave);
        m_BtnCancel.onClick.AddListener(OnClickCancel);
    }

    void OnClickSave()
    {
        RemoveAllListener();
        if (HasChange)
            ConfigManager.Instance.SaveSetting(m_SettingTemp);
        CloseReset();
    }

    void OnClickCancel()
    {
        RemoveAllListener();
        if (HasChange)
        {
            SetCGroup(false);
            UISystem.Show<UI_Tip>().Show("SaveSetting", TipType.Confirm, delegate
            {
                ConfigManager.Instance.SaveSetting(m_SettingTemp);
                CloseReset();
            }, delegate
            {
                ResetAllSetting();
                CloseReset();
            }, "Save", "Unsave", delegate
            {
                AddAllListener();
                SetCGroup(true);
            });
            return;
        }
        CloseReset();
    }

    void CloseReset()
    {
        //ResetSelect();
        m_Setting = null;
        m_SettingTemp = null;
        m_CGroup.DOFade(0, 0.5f).OnComplete(Close);
        UISystem.Show<UI_StartGame>();
    }

    protected override void OnUpdateLanguage()
    {
        base.OnUpdateLanguage();
        m_SettingTemp.Language = (int)LocalizationSystem.CurrentLanguageType;
        m_TextSetting.text = LocalizationSystem.GetContentText(DataBag, "Title");
        m_TextMusic.text = LocalizationSystem.GetContentText(DataBag, "Music");
        m_TextMusicVolume.text = LocalizationSystem.GetContentText(DataBag, "MusicVolume");
        m_TextSound.text = LocalizationSystem.GetContentText(DataBag, "Sound");
        m_TextSoundVolume.text = LocalizationSystem.GetContentText(DataBag, "SoundVolume");
        m_TextUISound.text = LocalizationSystem.GetContentText(DataBag, "UISound");
        m_TextUISoundVolume.text = LocalizationSystem.GetContentText(DataBag, "UISoundVolume");
        m_TextTutorial.text = LocalizationSystem.GetContentText(DataBag, "Tutorial");
        m_TextQuality.text = LocalizationSystem.GetContentText(DataBag, "Quality");
        m_TextLow.text = LocalizationSystem.GetContentText(Btn, "Low");
        m_TextMedium.text = LocalizationSystem.GetContentText(Btn, "Medium");
        m_TextHigh.text = LocalizationSystem.GetContentText(Btn, "High");
        m_TextLanguage.text = LocalizationSystem.GetContentText(DataBag, Language);
        m_TextLabelLanguage.text = LocalizationSystem.GetContentText(Language, Language);
        m_TextVibration.text = LocalizationSystem.GetContentText(DataBag, "GamepadVibration");
        m_TextBtnSave.text = LocalizationSystem.GetContentText(Btn, "Save");
        m_TextBtnCancel.text = LocalizationSystem.GetContentText(Btn, "Cancel");
    }
}
