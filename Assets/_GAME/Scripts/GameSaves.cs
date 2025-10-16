/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GameSaves
{
    public static int Money = 1000;
    public static Action OnMoneyChanget;
    
    public static int[] OpenTracks;
    public static int[] OpenCars;
    public static int CurrentCar = 0;
    
    public static float[] TrackBestTimes;
    
    public static bool IsPlayerPrefs = true;
    public static bool IsYandexGame = false;
    public static bool IsCrazyGames = false;

    public static void Init()
    {
        MainLoad();
    }

    public static void AddMoney(int value)
    {
        Money += value;
        
        OnMoneyChanget?.Invoke();
        
        MainSave();
    }
    
    public static void SubMoney(int value)
    {
        Money -= value;
        
        OnMoneyChanget?.Invoke();
        
        MainSave();
    }
    
    public static void MainLoad()
    {
        if (IsPlayerPrefs)
            PlayerPrefsLoad();

        if (IsYandexGame)
            YandexGameLoad();
        
        if (IsCrazyGames)
            return;
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
        /*Money = YandexGame.savesData.Money;
        OpenTracks = YandexGame.savesData.OpenTracks;
        OpenCars = YandexGame.savesData.OpenCars;
        CurrentCar = YandexGame.savesData.CurrentCar;

        TrackBestTimes = new float[YandexGame.savesData.TrackBestTimes.Length];

        for (int i = 0; i < TrackBestTimes.Length; i++)
        {
            TrackBestTimes[i] = YandexGame.savesData.TrackBestTimes[i];
        }*/
    }
    
    private static void YandexGameSave()
    {
        /*YandexGame.savesData.Money = Money;
        YandexGame.savesData.OpenTracks = OpenTracks;
        YandexGame.savesData.OpenCars = OpenCars;

        YandexGame.savesData.CurrentCar = CurrentCar;

        for (int i = 0; i < TrackBestTimes.Length; i++)
        {
            YandexGame.savesData.TrackBestTimes[i] = TrackBestTimes[i];
        }

        YandexGame.SaveProgress();*/
    }
    
    private static void YandexGameReset()
    {
        /*YandexGame.savesData.Money = 1000;

        int[] openTracks = {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        YandexGame.savesData.OpenTracks = openTracks;

        int[] openCars = {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        YandexGame.savesData.OpenCars = openCars;

        YandexGame.savesData.CurrentCar = 2;

        for (int i = 0; i < TrackBestTimes.Length; i++)
        {
            YandexGame.savesData.TrackBestTimes[i] = 0;
        }

        YandexGame.SaveProgress();*/
    }

    private static void PlayerPrefsLoad()
    {
        Money = PlayerPrefs.GetInt("Money", 1000);
        
        if (PlayerPrefs.HasKey("OpenTracks"))
        {
            string openTracksData = PlayerPrefs.GetString("OpenTracks");
            string[] parts = openTracksData.Split(',');
            OpenTracks = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                OpenTracks[i] = int.Parse(parts[i]);
        }
        else
        {
            OpenTracks = new int[64];
            OpenTracks[0] = 1;
        }

        if (PlayerPrefs.HasKey("OpenCars"))
        {
            string openCarsData = PlayerPrefs.GetString("OpenCars");
            string[] parts = openCarsData.Split(',');
            OpenCars = new int[parts.Length];
            for(int i = 0; i < parts.Length; i++)
                OpenCars[i] = int.Parse(parts[i]);
        }
        else
        {
            OpenCars = new int[64];
            OpenCars[0] = 1;
        }
        
        CurrentCar = PlayerPrefs.GetInt("CurrentCar", 0);

        if (PlayerPrefs.HasKey("TrackBestTimes"))
        {
            string trackBestTimesData = PlayerPrefs.GetString("TrackBestTimes");
            string[] parts = trackBestTimesData.Split(',');
            TrackBestTimes = new float[parts.Length];
            for(int i = 0; i < parts.Length; i++)
                TrackBestTimes[i] = int.Parse(parts[i]);
        }
        else
            TrackBestTimes = new float[64];
    }
    
    private static void PlayerPrefsSave()
    {
        string openTracksData = string.Join(",", OpenTracks);
        PlayerPrefs.SetString("OpenTracks", openTracksData);
        
        string openCarsData = string.Join(",", OpenCars);
        PlayerPrefs.SetString("OpenCars", openCarsData);
        
        PlayerPrefs.SetInt("CurrentCar", CurrentCar);
        
        string openBestTimesData = string.Join(",", TrackBestTimes);
        PlayerPrefs.SetString("TrackBestTimes", openBestTimesData);
        
        PlayerPrefs.Save();
    }
    
    private static void PlayerPrefsReset()
    {
        PlayerPrefs.DeleteKey("OpenTracks");
        PlayerPrefs.DeleteKey("OpenCars");
        PlayerPrefs.DeleteKey("CurrentCar");
        PlayerPrefs.DeleteKey("TrackBestTimes");
        
        PlayerPrefs.Save();
        
        MainLoad();
    }
}
