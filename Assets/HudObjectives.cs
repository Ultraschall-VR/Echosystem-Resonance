using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudObjectives : MonoBehaviour
{
    public List<HudObjective> HudObjectivesList;
    [SerializeField] private TextMeshProUGUI _textBox;
    [SerializeField] private Canvas _canvas;
    private int _index = 0;

    private bool _initialized = false;

    private void Awake()
    {
        _canvas.enabled = false;
        _textBox.text = null;
    }

    public void Initialize()
    {
        _initialized = true;
    }

    private void Update()
    {
        if(!_initialized) 
            return;
        
        _canvas.enabled = true;
        _textBox.text = HudObjectivesList[_index].ObjectiveText;
    }

    public void NextObjective()
    {
        if(HudObjectivesList.Count < _index)
            return;
        
        _index = _index + 1;
    }
}

[Serializable]
public class HudObjective
{
    public string ObjectiveName;
    public string ObjectiveText;
}
