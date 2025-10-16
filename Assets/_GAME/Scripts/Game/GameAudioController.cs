/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class GameAudioController : MonoBehaviour
{
    public static GameAudioController Instance;
    
    public EventInstance WindInstance;
    
    public EventInstance EngineInstance;
    
    private Bus _masterBus;
    private Bus _carBus;
    private Bus _enviromentBus;
    private Bus _uiBus;
    private Bus _sfxBus;    
    private Bus _musicBus;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventBus.OnStartPause += OnStartPause;
        EventBus.OnStopPause += OnStopPause;
        
        EventBus.OnDeath += OnDefeat;
        EventBus.OnFinish += OnVictory;
    }

    private void OnDisable()
    {
        EventBus.OnStartPause -= OnStartPause;
        EventBus.OnStopPause -= OnStopPause;
        
        EventBus.OnDeath -= OnDefeat;
        EventBus.OnFinish -= OnVictory;
    }

    public void Init()
    {
        _masterBus = RuntimeManager.GetBus("bus:/");
        _carBus = RuntimeManager.GetBus("bus:/Car");
        _enviromentBus = RuntimeManager.GetBus("bus:/Enviroment");
        _uiBus = RuntimeManager.GetBus("bus:/UI"); 
        _sfxBus = RuntimeManager.GetBus("bus:/SFX");    
        _musicBus = RuntimeManager.GetBus("bus:/Music");
        
        WindInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Wind);
        WindInstance.start();
        
        //EngineInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Engine);
        //EngineInstance.start();
    }

    public void OnStartPause()
    {
        _carBus.setMute(true);
        _enviromentBus.setMute(true);
        _sfxBus.setMute(true);
        _musicBus.setMute(true);
    }

    public void OnStopPause()
    {
        _carBus.setMute(false);
        _enviromentBus.setMute(false);
        _sfxBus.setMute(false);
        _musicBus.setMute(false);
    }

    public void OnDefeat()
    {
        _carBus.setMute(true);
        _enviromentBus.setMute(true);
        _sfxBus.setMute(true);
        _musicBus.setMute(true);
    }
    
    public void OnVictory()
    {
        _carBus.setMute(true);
        _enviromentBus.setMute(true);
        _sfxBus.setMute(true);
        _musicBus.setMute(true);
    }
}
