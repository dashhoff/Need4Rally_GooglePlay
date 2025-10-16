/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalScrollOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    private bool _hovered;

    private void Update()
    {
        if (_hovered)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _scrollRect.horizontalNormalizedPosition += scroll * _scrollRect.scrollSensitivity;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) => _hovered = true;
    public void OnPointerExit(PointerEventData eventData) => _hovered = false;
}