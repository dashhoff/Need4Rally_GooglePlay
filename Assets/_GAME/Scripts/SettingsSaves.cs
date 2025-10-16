/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public static class SettingsSaves
{
    [Header("Audio")]
    public static float GeneralVolume;
    public static float EnviromentVolume;
    public static float CarVolume;
    public static float UIVolume;
    public static float SFXVolume;
    public static float MusicVolume;
    
    [Header("Graphics")]
    public static int GraphicsPreset;
    
    [Header("Control")]
    public static bool AutomaticGear;
    public static float TurningSpeed;

    public static bool IsPlayerPrefs = true;
    public static bool IsYandexGame = false;
    public static bool IsCrazyGames = false;
    
    public static void Init()
    {
        MainLoad();
    }

    public static void MainLoad()
    {
        if (IsPlayerPrefs)
            PlayerPrefsLoad();
        
        if (IsYandexGame)
            return;
        
        if (IsCrazyGames)
            YandexGameLoad();
    }

    public static void MainSave()
    {
        if (IsPlayerPrefs)
            PlayerPrefsSave();
        
        if (IsYandexGame)
            YandexGameSave();
        
        if (IsCrazyGames)
            return;
    }
    
    public static void MainReset()
    {
        if (IsPlayerPrefs)
            PlayerPrefsReset();
        
        if (IsYandexGame)
            YandexGameReset();
        
        if (IsCrazyGames)
            return;
    }
    
    private static void YandexGameLoad()
    {
        /*GeneralVolume = YandexGame.savesData.GeneralVolume;
        EnviromentVolume = YandexGame.savesData.EnviromentVolume;
        CarVolume = YandexGame.savesData.CarVolume;
        UIVolume = YandexGame.savesData.UIVolume;
        SFXVolume = YandexGame.savesData.SFXVolume;
        MusicVolume = YandexGame.savesData.MusicVolume;

        GraphicsPreset = YandexGame.savesData.GraphicsPreset;
        
        AutomaticGear = YandexGame.savesData.AutomaticGear;
        TurningSpeed = YandexGame.savesData.TurningSpeed;*/
    }
    
    private static void YandexGameSave()
    {
        /*YandexGame.savesData.GeneralVolume = GeneralVolume;
        YandexGame.savesData.EnviromentVolume = EnviromentVolume;
        YandexGame.savesData.CarVolume = CarVolume;
        YandexGame.savesData.UIVolume = UIVolume;
        YandexGame.savesData.SFXVolume = SFXVolume;
        YandexGame.savesData.MusicVolume = MusicVolume;
        
        YandexGame.savesData.GraphicsPreset = GraphicsPreset;
        
        YandexGame.savesData.AutomaticGear = AutomaticGear;
        YandexGame.savesData.TurningSpeed = TurningSpeed;
        
        YandexGame.SaveProgress();*/
    }

    private static void YandexGameReset()
    {
        /*YandexGame.savesData.GeneralVolume = 1;
        YandexGame.savesData.EnviromentVolume = 1;
        YandexGame.savesData.CarVolume = 1;
        YandexGame.savesData.UIVolume = 1;
        YandexGame.savesData.SFXVolume = 1;
        YandexGame.savesData.MusicVolume = 1;
        
        YandexGame.savesData.GraphicsPreset = 3;
        
        YandexGame.savesData.AutomaticGear = false;
        YandexGame.savesData.TurningSpeed = 0.5f;
        
        YandexGame.SaveProgress();*/
    }

    private static void PlayerPrefsLoad()
    {
        GeneralVolume = PlayerPrefs.GetFloat("GeneralVolume", 1);
        EnviromentVolume = PlayerPrefs.GetFloat("EnviromentVolume", 1);
        CarVolume = PlayerPrefs.GetFloat("CarVolume", 1);
        UIVolume = PlayerPrefs.GetFloat("UIVolume", 1);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        
        GraphicsPreset = PlayerPrefs.GetInt("GraphicsPreset", 1);
        
        AutomaticGear = PlayerPrefs.GetInt("AutomaticGear", 0) == 1;
        TurningSpeed = PlayerPrefs.GetFloat("TurningSpeed", 0.5f);
    }
    
    private static void PlayerPrefsSave()
    {
        PlayerPrefs.SetFloat("GeneralVolume", GeneralVolume);
        PlayerPrefs.SetFloat("EnviromentVolume", EnviromentVolume);
        PlayerPrefs.SetFloat("CarVolume", CarVolume);
        PlayerPrefs.SetFloat("UIVolume", UIVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        
        PlayerPrefs.SetInt("GraphicsPreset", GraphicsPreset);
        
        PlayerPrefs.SetInt("AutomaticGear", AutomaticGear ? 1 : 0);
        PlayerPrefs.SetFloat("TurningSpeed", TurningSpeed);
        
        PlayerPrefs.Save();
    }
    
    private static void PlayerPrefsReset()
    {
        PlayerPrefs.DeleteKey("GeneralVolume");
        PlayerPrefs.DeleteKey("EnviromentVolume");
        PlayerPrefs.DeleteKey("CarVolume");
        PlayerPrefs.DeleteKey("UIVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        
        PlayerPrefs.DeleteKey("GraphicsPreset");
        
        PlayerPrefs.DeleteKey("AutomaticGear");
        PlayerPrefs.DeleteKey("TurningSpeed");
        
        PlayerPrefs.Save();
        
        MainLoad();
    }
}
