/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class MenuTracks : MonoBehaviour
{
    [SerializeField] private MenuCameraController _menuCameraController;
    
    [SerializeField] private UITrack[] _tracks;

    [SerializeField] private UIPanel _selectedTrackPanel;
    [SerializeField] private UIPanel _selectCarPanel;

    public void Init()
    {
        for (int i = 0; i < _tracks.Length; i++)
        {
            if (GameSaves.OpenTracks[i] == 0)
                _tracks[i].Locked = true;
            else
                _tracks[i].Locked = false;
            
            _tracks[i].Init();
        }
    }

    public void OnTrackClick(int id)
    {
        if (id == -1)
        {
            GameData.CurrentTrack = id;
            return;
        }

        if (!_tracks[id].Locked)
        {
            GameData.CurrentTrack = id;
            
            _menuCameraController.SetActiveCamera(2);
            
            _selectCarPanel.Open();
            _selectedTrackPanel.Close();
        }
    }
}
