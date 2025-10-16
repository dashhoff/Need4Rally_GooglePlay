/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using FMOD.Studio;

public class CarAudio : MonoBehaviour
{
   [SerializeField] private Car _car;

   [SerializeField] private EventInstance _engineInstance;

   private void OnEnable()
   {
      EventBus.OnStartGame += Init;
   }

   private void OnDisable()
   {
      EventBus.OnStartGame -= Init;
   }

   private void Update()
   {
      if (!_car.Active || GameController.Instance.GameOver || GameController.Instance.PausedGame) return;
      
      UpdateEngineSound();
      
      UpdateWindSound();
   }

   public void Init()
   {
      if (_car._engine.GetEngineLayout() == EngineLayout.Inline4 ||
          _car._engine.GetEngineLayout() == EngineLayout.Inline5 ||
          _car._engine.GetEngineLayout() == EngineLayout.Inline6)
      {
         _engineInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Engine_1);
         _engineInstance.start();
         
      }
      else
      {
         _engineInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Engine_2);
         _engineInstance.start();
      }
   }

   public void UpdateWindSound()
   {
      float windIntensity = Mathf.InverseLerp(0, 150, _car.GetCurrentSpeed() * 3.6f);
      
      AudioManager.Instance.SetParametrs(GameAudioController.Instance.WindInstance, "Wind_intensity", windIntensity);
   }
   
   public void UpdateEngineSound()
   {
      float engineRPMNormalized = Mathf.InverseLerp(0, _car._engine.GetMaxRPM(), _car._engine.GetRPM());
      
      if (_car._engine.GetEngineLayout() == EngineLayout.Inline4  ||
          _car._engine.GetEngineLayout() == EngineLayout.Inline5  ||
          _car._engine.GetEngineLayout() == EngineLayout.Inline6)
      {
         AudioManager.Instance.SetParametrs(_engineInstance, "RPM", engineRPMNormalized);
      }
      else
      {
         AudioManager.Instance.SetParametrs(_engineInstance, "RPM", engineRPMNormalized);
      }
   }
}
