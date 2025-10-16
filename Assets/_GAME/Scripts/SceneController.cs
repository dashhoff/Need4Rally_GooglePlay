/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadLevel(int id)
    {
        //ScreenFader.Instance.FadeIn(() => { SceneManager.LoadScene(id); });
        SceneManager.LoadScene(id);
    }

    public void LoadLevel(string name)
    {
        //ScreenFader.Instance.FadeIn(() => { SceneManager.LoadScene(name); });
        SceneManager.LoadScene(name);
    }

    public void RestartLevel()
    {
        //ScreenFader.Instance.FadeIn(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
