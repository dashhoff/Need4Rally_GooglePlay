/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    //[SerializeField] private SettingsSaves _settingsSaves;
    //[SerializeField] private GameSaves _gameSaves;
    
    [SerializeField] private Settings _settings;
    
    [SerializeField] private MenuTracks _menuTracks;
    [SerializeField] private MenuCarManager _menuCarManager;
    
    [SerializeField] private MenuUIController _menuUICotnroller; 
    
    [SerializeField] private MenuAudioController _menuAudioController;
    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (ScreenFader.Instance != null)
            ScreenFader.Instance.FadeOut();
        
        SettingsSaves.Init();
        GameSaves.Init();
        
        _settings.Init();
        
        _menuTracks.Init();
        _menuCarManager.Init();

        _menuUICotnroller.Init();
        
        _menuAudioController.Init();

        LoadingManager.Instance.NextScene = "Game";
        LoadingManager.Instance.ReadyToLoading = false;
        LoadingManager.Instance.StartLoading();
    }
}
