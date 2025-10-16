/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using TMPro;

public class TranslateText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [TextArea(1, 5)] [SerializeField] private string _en;
    [TextArea(1, 5)] [SerializeField] private string _ru;

    public void Start()
    {
        if (_text == null)
            _text = GetComponent<TMP_Text>();
        
        Translate();
    }

    public void Translate()
    {
        /*if (YandexGame.lang == "ru")
            _text.text = _ru;
        else
            _text.text = _en;*/
    }
}
