using System;

[Serializable]
public class SettingConfig
{
    public bool MusicIsMute;
    public float MusicVolume = 1;
    public bool SoundIsMute;
    public float SoundVolume = 1;
    public bool UISoundIsMute;
    public float UISoundVolume = 1;
    public int Quality = 2;
    public bool Tutorial = true;
    public int Language;
    public bool GamepadVibration = true;

    public SettingConfig() { }

    public SettingConfig(SettingConfig config)
    {
        MusicIsMute = config.MusicIsMute;
        MusicVolume = config.MusicVolume;
        SoundIsMute = config.SoundIsMute;
        SoundVolume = config.SoundVolume;
        UISoundIsMute = config.UISoundIsMute;
        UISoundVolume = config.UISoundVolume;
        Quality = config.Quality;
        Tutorial = config.Tutorial;
        Language = config.Language;
        GamepadVibration = config.GamepadVibration;
    }
}