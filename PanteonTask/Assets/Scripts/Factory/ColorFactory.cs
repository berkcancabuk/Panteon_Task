using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public enum ColorType
    {
        Selected,
        NotSelected,
        OnCollision
    }
    public static class ColorFactory
    {
        public static Color GetColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Selected:
                    return new Color(16f / 255f, 255f / 255f, 72f / 255f, 87f / 255);
                case ColorType.NotSelected:
                    return new Color(0 / 255f, 0 / 255f, 0 / 255f, 87f / 255);
                case ColorType.OnCollision:
                    return new Color(212f / 255f, 49f / 255f, 49f / 255f, 120f / 255f);
                default:
                    Debug.Log("Color type is invalid : " + colorType);
                    return new Color(16f / 255f, 255f / 255f, 72f / 255f, 87f / 255);
            }
        }
        
    }
}