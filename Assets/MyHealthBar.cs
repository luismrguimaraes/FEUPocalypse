using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;

    void Start()
    {
        low = Color.red;
        high = Color.green;

        SetHealth(1.0f, 1.0f);
    }

    public void SetHealth(float currHealth, float maxHealth)
    {
        slider.value = currHealth;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

}