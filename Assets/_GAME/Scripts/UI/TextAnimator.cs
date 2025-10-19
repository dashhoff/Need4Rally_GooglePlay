/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;

    private Coroutine _currentCoroutine;
    
    public void TextAnim(string newText, float duration)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(AnimCor(newText, duration));
    }

    private IEnumerator AnimCor(string newText, float duration)
    {
        string oldText = _textField.text;
        float eraseSpeed = duration / Mathf.Max(oldText.Length, 1f);
        float typeSpeed = duration / Mathf.Max(newText.Length, 1f);

        for (int i = oldText.Length; i >= 0; i--)
        {
            _textField.text = oldText.Substring(0, i);
            yield return new WaitForSeconds(eraseSpeed / 10);
        }

        //yield return new WaitForSeconds(0.1f);
        
        for (int i = 0; i <= newText.Length; i++)
        {
            _textField.text = newText.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed / 10);
        }

        _currentCoroutine = null;
    }
}