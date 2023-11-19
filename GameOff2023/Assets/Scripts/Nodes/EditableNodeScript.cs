using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditableNodeScript : NodeScript
{
    [SerializeField]
    private Slider _valueSlider;

    public int GetSliderValue()
    {
        return (int)_valueSlider.value;
    }
}
