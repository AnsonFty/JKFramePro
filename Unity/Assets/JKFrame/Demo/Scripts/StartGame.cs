using JKFrame;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        LocalizationSystem.LoadAllTable();
        ConfigManager.Instance.Init();
        UISystem.Show<UI_StartGame>();
    }
}
