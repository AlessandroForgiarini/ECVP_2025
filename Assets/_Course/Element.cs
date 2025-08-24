using UnityEngine;

public enum Element
{
    ICE,
    FIRE,
    GRASS
}

static class ElementMethods
{
    public static Color ToColor(this Element element)
    {
        switch (element)
        {
            case Element.ICE:
                return new Color(0, 0.1f, 1f);
            case Element.FIRE:
                return new Color(1, 0.1f, 0);
            case Element.GRASS:
                return new Color(0.1f, 1f, 0);
            default:
                return Color.white;
        }
    }
}