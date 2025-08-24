using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Element goblinElement;
    public Renderer myRenderer;
    
    void Start()
    {
        UpdateGoblinElement();
    }

    void UpdateGoblinElement()
    {
        Color color = goblinElement.ToColor();
        myRenderer.material.color = color;
        Debug.Log(color);
    }
}
