using UnityEngine;
using UnityEngine.UI;

public class CircleHealthBar : MonoBehaviour
{
    public Image _bar;
    public float _healthValue = 0;

    void Update()
    {
        HealthChange(_healthValue);
    }

    void HealthChange(float healthValue)
    {
        float amount = (healthValue / 100.0f) * 360.0f / 360;
        _bar.fillAmount = amount;
    }
}