using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudObjectives : MonoBehaviour
{
    public List<HudObjective> HudObjectivesList;
    [SerializeField] private TextMeshProUGUI _textBox;
    private int _index = 0;
    
    private void Start()
    {
        _textBox.text = HudObjectivesList[_index].ObjectiveText;
    }

    public void NextObjective()
    {
        _index = _index + 1;
    }
}

[Serializable]
public class HudObjective
{
    public string ObjectiveName;
    public string ObjectiveText;
}
