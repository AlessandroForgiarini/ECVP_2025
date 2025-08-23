using UnityEngine;

namespace ECVP_2025_Unity_Workshop
{
    [CreateAssetMenu()]
    public class BallDataSO : ScriptableObject
    {
        public float explosionRadius;
        public int damage;
        public AudioClip throwEffect;
        public AudioClip explodeEffect;
    }
}