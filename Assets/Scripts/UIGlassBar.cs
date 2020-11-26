using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIGlassBar : MonoBehaviour
{
    public UIGlassBar()
    {
        instance = this;
    }

    private static UIGlassBar instance;

    public static UIGlassBar Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIGlassBar();
            }
            return instance;
        }
    }

    [SerializeField] private Image glassMask;
    private float originalSize;
    
    public void Initialized()
    {
        originalSize = glassMask.rectTransform.rect.height;
    }
    
    public void SetValue(int value)
    {
        glassMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize + value);
    }
}
