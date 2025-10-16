using UnityEngine;

public class MenuController : MonoBehaviour
{
    

    public void AllDataReset()
    {
        GameSaves.MainReset();
        SettingsSaves.MainReset();
    }

    public void GameSavesReset()
    {
        GameSaves.MainReset();
    }

    public void SettingsReset()
    {
        SettingsSaves.MainReset();
    }
}
