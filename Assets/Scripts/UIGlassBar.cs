using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIGlassBar : MonoBehaviour
{
    public static UIGlassBar Instance { get; private set; }

    [SerializeField] private Image glassMask;
    private float originalSize;

    private void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        originalSize = glassMask.rectTransform.rect.height;
    }
    
    public void SetValue(int value)
    {
        glassMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize + value);
    }
}
