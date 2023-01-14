using DG.Tweening;
using JKFrame;
using UnityEngine;
using UnityEngine.UI;

[UIWindowData(typeof(UI_StartGame), false, "UI_StartGame", 0)]
public class UI_StartGame : UI_WindowBase
{
    const string DataBag = "UI_StartGame";
    [SerializeField] SelectableGroup m_SGroup;
    [SerializeField] CanvasGroup m_CGroupMainMenu;
    [SerializeField] CanvasGroup m_CGroupBg;
    [SerializeField] Text m_TextTitle;
    [SerializeField] Text m_TextStartGame;
    [SerializeField] Text m_TextContinueGame;
    [SerializeField] Text m_TextSetting;
    [SerializeField] Text m_TextTeamGroup;
    [SerializeField] Text m_TextQuit;

    public override void Init()
    {
        base.Init();
        m_CGroupMainMenu.alpha = 0;
        SelectableSystem.AddSelectable(m_SGroup);
    }

    public override void OnShow()
    {
        base.OnShow();
        DoMainMenuCGroup(true);
    }

    void DoMainMenuCGroup(bool isActive)
    {
        if (isActive)
        {
            m_CGroupMainMenu.DOFade(1, 1);
            return;
        }
        m_CGroupMainMenu.DOFade(0, 0.5f);
    }

    public void OnClickStartGame()
    {
        m_CGroupMainMenu.DOFade(0, 0.5f).OnComplete(delegate
        {
            SceneSystem.LoadSceneAsync("DEMOHome", delegate
            {
                m_CGroupBg.DOFade(0, 0.5f).OnComplete(Close);
            });
        });
    }

    public void OnClickContinueGame()
    {

    }

    public void OnClickSetting()
    {
        DoMainMenuCGroup(false);
        UISystem.Show<UI_Setting>();
    }

    public void OnClickGroup()
    {

    }

    public void OnClickQuit()
    {
        DoMainMenuCGroup(false);
        UISystem.Show<UI_Tip>().Show("QuitGame", TipType.Warning, delegate
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }, delegate
        {
            DoMainMenuCGroup(true);
        }, "Quit");
    }

    protected override void OnUpdateLanguage()
    {
        base.OnUpdateLanguage();
        m_TextTitle.text = LocalizationSystem.GetContentText(DataBag, "Title");
        m_TextStartGame.text = LocalizationSystem.GetContentText(DataBag, "StartGame"); ;
        m_TextContinueGame.text = LocalizationSystem.GetContentText(DataBag, "ContinueGame"); ;
        m_TextSetting.text = LocalizationSystem.GetContentText(DataBag, "Setting"); ;
        m_TextTeamGroup.text = LocalizationSystem.GetContentText(DataBag, "TeamGroup"); ;
        m_TextQuit.text = LocalizationSystem.GetContentText(DataBag, "QuitGame");
    }
}
