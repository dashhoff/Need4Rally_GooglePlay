/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
   public static GameController Instance;
   
   public bool PausedGame = false;
   
   public bool GameOver = true;
   
   [SerializeField] private TMP_Text _startTimerText;
   
   private void Awake()
   {
      if (Instance == null)
         Instance = this;
      else
         Destroy(gameObject);
   }

   private void Start()
   {
      
   }

   public void StartGame()
   {
      Time.timeScale = 1;
      
      StartCoroutine(StartRaceCor());
   }
   
   private void OnEnable()
   {
      EventBus.OnStartPause += GamePaused;
      EventBus.OnStopPause += GameUnPaused;

      EventBus.OnDeath += OnDefeat;
      EventBus.OnFinish += OnVictory;
   }

   private void OnDisable()
   {
      EventBus.OnStartPause -= GamePaused;
      EventBus.OnStopPause -= GameUnPaused;
      
      EventBus.OnDeath -= OnDefeat;
      EventBus.OnFinish -= OnVictory;
   }

   public void StartPause()
   {
      if (PausedGame) return;
      
      EventBus.InvokeStartPause();
   }
   
   public void StopPause()
   {
      if (!PausedGame) return;
      
      EventBus.InvokeStopPause();
   }
   
   public void GamePaused()
   {
      PausedGame = true;
      Time.timeScale = 0;
   }
   
   public void GameUnPaused()
   {
      PausedGame = false;
      Time.timeScale = 1;
   }

   public void OnDefeat()
   {
      GameOver = true;
      
      GameSaves.Money += 50;
      GameSaves.MainSave();
   }
   
   public void OnVictory()
   {
      GameOver = true;
      
      GameSaves.Money += 250;
      
      if (GameData.CurrentTrack + 1 < GameSaves.OpenTracks.Length)
      {
         GameSaves.OpenTracks[GameData.CurrentTrack + 1] = 1;
         GameSaves.MainSave();
      }
   }

   public IEnumerator StartRaceCor()
   {
      if (GameData.CurrentTrack == -1)
      {
         GameOver = false;
         EventBus.InvokeStartGame();
         
         _startTimerText.gameObject.SetActive(false);
         
         yield break;
      }
      
      GameOver = true;
      
      yield return new WaitForSecondsRealtime(1f);
      
      AudioManager.Instance.PlayOneShot(FMODEvents.Instance.RaceStart, transform.position);

      _startTimerText.text = 3.ToString();
      yield return new WaitForSecondsRealtime(1f);
      _startTimerText.text = 2.ToString();
      yield return new WaitForSecondsRealtime(1f);
      _startTimerText.text = 1.ToString();
      yield return new WaitForSecondsRealtime(1f);
      _startTimerText.gameObject.SetActive(false);
      
      EventBus.InvokeStartGame();
      GameOver = false;
   }
}
