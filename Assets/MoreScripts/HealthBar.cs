using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    public int activeSec = 2;
    private float timer = 0;

    void Start()
    {
        slider.gameObject.SetActive(false);

        low = Color.red;
        high = Color.yellow;
}

    public void SetHealth(int currHealth, int maxHealth)
    {

        if (currHealth < maxHealth)
        {
            if (currHealth > 0)
            {
                slider.gameObject.SetActive(true);
                timer = 0;
            }
            else
            {
                slider.gameObject.SetActive(false);
            }
        }
        slider.value = currHealth;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > activeSec)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            timer += Time.deltaTime;
        }

        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    }
}
