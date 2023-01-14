using JKFrame;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        ConfigManager.Instance.Init();
        UISystem.Show<UI_StartGame>();
    }
}
