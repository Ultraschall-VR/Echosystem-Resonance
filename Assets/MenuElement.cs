using System.Collections;
using TMPro;
using UnityEngine;

public class MenuElement : MonoBehaviour
{ 
    private TextMeshProUGUI _text;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectColor;
    
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.faceColor = _defaultColor;
    }

    public void Highlight()
    {
        _text.faceColor = _highlightColor;
        //StartCoroutine(ChangeState(_defaultColor, _highlightColor));
    }

    public void DeHighlight()
    {
        _text.faceColor = _defaultColor;
        //StartCoroutine(ChangeState(_highlightColor, _defaultColor));
    }

    public void Select()
    {
        _text.faceColor = _selectColor;
        //StartCoroutine(ChangeState(_highlightColor, _selectColor));
    }

    private IEnumerator ChangeState(Color from, Color to)
    {
        float t = 0;
        float timer = 0.5f;

        while (t <= timer)
        {
            t += Time.deltaTime;
            _text.faceColor = Color.Lerp(from, to, t);
            yield return null;
        }
    }
}
