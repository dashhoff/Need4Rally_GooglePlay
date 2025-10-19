/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;
    
    [Header("UI")]
    public EventReference ButtonHover;
    public EventReference ButtonClick;
    public EventReference Woosh;
    
    [Header("SFX")]
    public EventReference Logo;
    public EventReference RaceStart;
    public EventReference Death;
    public EventReference Finish;
    
    [Header("Enviroment")]
    public EventReference Wind;
    
    [Header("Car")]
    public EventReference Engine_1;
    public EventReference Engine_2;
    public EventReference ShiftGearbox;
    public EventReference ExhaustShot;
    
    [Header("Music")]
    public EventReference Music;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
