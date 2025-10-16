/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class MenuCar : MonoBehaviour
{
    public GameObject Model;
    
    public string CarName;
    
    [TextArea(1, 5)] public string CarRuInfo;
    [TextArea(1, 5)] public string CarEnInfo;

    public int Price;
}
