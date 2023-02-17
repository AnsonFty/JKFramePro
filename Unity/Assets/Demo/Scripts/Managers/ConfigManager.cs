using JKFrame;

public class ConfigManager : Singleton<ConfigManager>
{
    public SettingConfig Setting;
    public SaveConfig SaveConfigTemp;

    public void Init()
    {
        InitSetting();

        SaveConfigTemp = new SaveConfig();
        SaveConfigTemp.EventConfig = new EventConfig();
    }

    #region Setting

    void InitSetting()
    {
        SettingConfig config = SaveSystem.LoadSetting<SettingConfig>();
        if (config != null)
        {
            Setting = config;
            InitSettingValue();
            return;
        }
        Setting = new SettingConfig();
        SaveSystem.SaveSetting(Setting);
        InitSettingValue();
    }

    void InitSettingValue()
    {
        //AudioManager audioManager = AudioManager.Instance;
        //audioManager.MusicIsMute = Setting.MusicIsMute;
        //audioManager.MusicVolume = Setting.MusicVolume;
        //audioManager.SoundIsMute = Setting.SoundIsMute;
        //audioManager.SoundVolume = Setting.SoundVolume;
        //audioManager.UISoundIsMute = Setting.UISoundIsMute;
        //audioManager.UISoundVolume = Setting.UISoundVolume;
        LocalizationSystem.CurrentLanguageType = (LanguageType)Setting.Language;
    }

    public void SaveSetting(SettingConfig setting)
    {
        SaveSystem.SaveSetting(setting);
        Setting = setting;
    }
    #endregion

    
}
