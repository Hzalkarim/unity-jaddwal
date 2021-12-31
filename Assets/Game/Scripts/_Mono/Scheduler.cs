using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scheduler : MonoBehaviour
{
    private int value;
    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            currentVal = value;
            text.text = value.ToString();
            maxValText.text = value.ToString();
            CalculateFillAmount();
        }
    }

    public Team team;
    public Image image;
    public Text text;
    public Text maxValText;

    public int currentVal = 1;

    public void TimeProgress()
    {
        currentVal--;
        DestroyIfCountdownOver();
        CalculateFillAmount();
    }

    private void DestroyIfCountdownOver()
    {
        if (currentVal < 0)
        {
            Destroy(gameObject);
        }
    }

    public void TimeRegress()
    {
        currentVal++;
        CalculateFillAmount();

    }

    public int GetFinalValue()
    {
        if (currentVal == 0)
        {
            return value * 2;
        }
        else
        {
            return value - currentVal;
        }
    }

    public void DecreaseCurrentValue(int value)
    {
        currentVal += value;
        CalculateFillAmount();
        DestroyIfCountdownOver();
    }

    private void CalculateFillAmount()
    {
        text.text = currentVal.ToString();
        image.fillAmount = (float)currentVal / value;
    }
}
