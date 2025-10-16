/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System;
using UnityEngine;

public class CarEventBus : MonoBehaviour
{
    public static Action OnExhaust;
    public static void InvokeExhaust() => OnExhaust?.Invoke();
    
    
    
}
