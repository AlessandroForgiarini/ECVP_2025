using UnityEngine;

[CreateAssetMenu()]
public class BallDataSO : ScriptableObject
{
    public float explosionRadius;
    public int damage;
    public AudioClip throwEffect;
    public AudioClip explodeEffect;
}