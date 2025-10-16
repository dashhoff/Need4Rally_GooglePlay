/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.Rendering.Universal;

public class Settings : MonoBehaviour
{
    [Header("Audio")]
    //[SerializeField] private AudioMixer _audioMixer;
    
    private Bus _masterBus;
    private Bus _carBus;
    private Bus _enviromentBus;
    private Bus _uiBus;
    private Bus _sfxBus;    
    private Bus _musicBus;

    [SerializeField] private Slider _generalSlider;
    [SerializeField] private Slider _carSlider;
    [SerializeField] private Slider _enviromentSlider;
    [SerializeField] private Slider _uiSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    
    [Header("Graphics")]
    [SerializeField] private Slider _generalGraphicSlider;
    [SerializeField] private UniversalRenderPipelineAsset[] _universalRenderPipelineAssets;
    
    [Header("Controls")]
    [SerializeField] private Toggle _automaticGearToggle;
    [SerializeField] private Slider _turningSpeedSlider;

    public void Init()
    {
        LoadSlidersValue();
        
        _masterBus = RuntimeManager.GetBus("bus:/");
        _carBus = RuntimeManager.GetBus("bus:/Car");
        _enviromentBus = RuntimeManager.GetBus("bus:/Enviroment");
        _uiBus = RuntimeManager.GetBus("bus:/UI"); 
        _sfxBus = RuntimeManager.GetBus("bus:/SFX");    
        _musicBus = RuntimeManager.GetBus("bus:/Music");

        _generalGraphicSlider.maxValue = _universalRenderPipelineAssets.Length - 1;
    }

    public void OnGeneralSliderChanged()
    {
        SettingsSaves.GeneralVolume = _generalSlider.value;
        SettingsSaves.MainSave();
        
        //_audioMixer.SetFloat("Master", ToDecibel(_generalSlider.value));
        _masterBus.setVolume(_generalSlider.value);
    }

    public void OnEnviromentSliderChanged()
    {
        SettingsSaves.EnviromentVolume = _enviromentSlider.value;
        SettingsSaves.MainSave();
        
        //_audioMixer.SetFloat("Enviroment", ToDecibel(_enviromentSlider.value));
        _enviromentBus.setVolume(_enviromentSlider.value);
    }

    public void OnCarSliderChanged()
    {
        SettingsSaves.CarVolume = _carSlider.value;
        SettingsSaves.MainSave();
        
        //_audioMixer.SetFloat("Car", ToDecibel(_carSlider.value));
        _carBus.setVolume(_carSlider.value);
    }

    public void OnUISliderChanged()
    {
        SettingsSaves.UIVolume = _uiSlider.value;
        SettingsSaves.MainSave();
        
        //_audioMixer.SetFloat("UI", ToDecibel(_uiSlider.value));
        _uiBus.setVolume(_uiSlider.value);
    }
    
    public void OnSFXSliderChanged()
    {
        SettingsSaves.UIVolume = _sfxSlider.value;
        SettingsSaves.MainSave();
        
        //_audioMixer.SetFloat("UI", ToDecibel(_uiSlider.value));
        _sfxBus.setVolume(_sfxSlider.value);
    }

    public void OnMusicSliderChanged()
    {
        SettingsSaves.MusicVolume = _musicSlider.value;
        SettingsSaves.MainSave();

        //_audioMixer.SetFloat("Music", ToDecibel(_musicSlider.value));
        _musicBus.setVolume(_musicSlider.value);
    }
    
    public void OnGeneralGraphicSliderChanged()
    {
        int presetIndex =  Mathf.RoundToInt(_universalRenderPipelineAssets.Length / 2);

        SettingsSaves.GraphicsPreset = presetIndex;
        SettingsSaves.MainSave();

        UniversalRenderPipelineAsset selectedAsset = _universalRenderPipelineAssets[presetIndex];
        QualitySettings.renderPipeline = selectedAsset;
        
        //QualitySettings.SetQualityLevel(SettingsSaves.GraphicsPreset);
        
        Debug.Log("Пресет графики: " + _universalRenderPipelineAssets[presetIndex]);
    }
    
    public void OnAutomaticGearToggleChanged()
    {
        SettingsSaves.AutomaticGear = _automaticGearToggle.isOn;
        SettingsSaves.MainSave();
        
        EventBus.InvokeGearboxChanget();
    }
    
    public void OnTurningSpeedSliderChanged()
    {
        SettingsSaves.TurningSpeed = _turningSpeedSlider.value;
        SettingsSaves.MainSave();
    }
    
    public void LoadSlidersValue()
    {
        _generalSlider.value = SettingsSaves.GeneralVolume;
        _enviromentSlider.value = SettingsSaves.EnviromentVolume;
        _carSlider.value = SettingsSaves.CarVolume;
        _uiSlider.value = SettingsSaves.UIVolume;
        _musicSlider.value = SettingsSaves.MusicVolume;

        _generalGraphicSlider.value = SettingsSaves.GraphicsPreset;

        _automaticGearToggle.isOn = SettingsSaves.AutomaticGear;
        _turningSpeedSlider.value = SettingsSaves.TurningSpeed;
    }
    

    private float ToDecibel(float value) => Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
}