/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System;

public static class EventBus
{
    public static event Action OnSwitchCamera;
    public static void InvokeSwitchCamera() => OnSwitchCamera?.Invoke();
    
    
    public static event Action OnStartGame;
    public static void InvokeStartGame() => OnStartGame?.Invoke();
    
    
    public static event Action OnStartPause;
    public static void InvokeStartPause() => OnStartPause?.Invoke();
    
    
    public static event Action OnStopPause;
    public static void InvokeStopPause() => OnStopPause?.Invoke();
    
    
    public static event Action OnFinish;
    public static void InvokeFinish() => OnFinish?.Invoke();
    
    
    public static event Action OnDeath;
    public static void InvokeDeath() => OnDeath?.Invoke();
    
    
    public static event Action OnGearboxChanget;
    public static void InvokeGearboxChanget() => OnGearboxChanget?.Invoke();
}
