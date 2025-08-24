using UnityEngine;

public class Rock : MonoBehaviour
{
    public Element rockElement;
    public Renderer myRenderer;

    void Start()
    {
        UpdateRockElement();
    }

    void UpdateRockElement()
    {
        Color color = rockElement.ToColor();
        myRenderer.material.color = color;
        Debug.Log(color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("Goblin"))
        {
            Goblin goblin = collidedGameObject.GetComponent<Goblin>();
            Element goblinElement = goblin.goblinElement;
            if (rockElement == goblinElement)
            {
                Destroy(collidedGameObject);
            }
        }
    }
}
