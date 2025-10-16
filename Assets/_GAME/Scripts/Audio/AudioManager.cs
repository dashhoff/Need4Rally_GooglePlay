/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private Bus _masterBus;
    [SerializeField] private Bus _carBus;
    [SerializeField] private Bus _enviromentBus;
    [SerializeField] private Bus _uiBus;
    [SerializeField] private Bus _sfxBus;    
    [SerializeField] private Bus _musicBus;
    
    [SerializeField] private List<EventInstance> _eventInstances = new List<EventInstance>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
        _masterBus = RuntimeManager.GetBus("bus:/");
        _carBus = RuntimeManager.GetBus("bus:/Car");
        _enviromentBus = RuntimeManager.GetBus("bus:/Enviroment");
        _uiBus = RuntimeManager.GetBus("bus:/UI"); 
        _sfxBus = RuntimeManager.GetBus("bus:/SFX");    
        _musicBus = RuntimeManager.GetBus("bus:/Music");
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            MuteAll();
        
        if (Time.timeScale == 1f)
            UnMuteAll();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        
        _eventInstances.Add(eventInstance);
        
        return eventInstance;
    }

    public void SetParametrs(EventInstance eventInstance, string parametrName, float parametrValue)
    {
        eventInstance.setParameterByName(parametrName, parametrValue);
    }

    public void ClearInstances()
    {
        foreach (var instance in _eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }

        _eventInstances.Clear();
    }

    private void OnDestroy()
    {
        ClearInstances();
    }
    
    public void MuteAll()
    {
        _masterBus.setMute(true);
        _carBus.setMute(true);
        _enviromentBus.setMute(true);
        _uiBus.setMute(true);
        _sfxBus.setMute(true);
        _musicBus.setMute(true);
    }
    
    public void UnMuteAll()
    {
        _masterBus.setMute(false);
        _carBus.setMute(false);
        _enviromentBus.setMute(false);
        _uiBus.setMute(false);
        _sfxBus.setMute(false);
        _musicBus.setMute(false);
    }
    
    void OnApplicationPause(bool pause)
    {
        FMODUnity.RuntimeManager.MuteAllEvents(pause);
    }
}
