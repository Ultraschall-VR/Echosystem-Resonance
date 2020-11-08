using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowToolTipp : MonoBehaviour
{
    [SerializeField] private ToolTipps.Tooltip _tooltip;
    private ToolTipps _toolTipps;

    private void OnEnable()
    {
        _toolTipps = FindObjectOfType<ToolTipps>();
        _toolTipps.LoadToolTip(_tooltip);
    }
}
