using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
    private const int MaxAttemptsToGenerateCrystal = 100;
    [SerializeField] private float crystalRadius = .2f;
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private Renderer planeRenderer;
    
    public void GenerateCrystals(int totalCrystals)
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 extents = planeRenderer.bounds.extents;
        Vector3 colPosition = planeRenderer.transform.position;
        for (int i = 0; i < totalCrystals; i++)
        {
            int attempts = 0;
            Vector3 randomPosition;
            do
            {
                randomPosition = colPosition + new Vector3(
                    Random.Range(-extents.x, extents.x),
                    0,
                    Random.Range(-extents.z, extents.z)
                    );
                attempts++;
            } while (positions.Any(pos => Vector3.Distance(pos, randomPosition) < crystalRadius && attempts < MaxAttemptsToGenerateCrystal));
            positions.Add(randomPosition);
        }

        foreach (Vector3 position in positions)
        {
            float randomRotY = Random.rotation.eulerAngles.y;
            Instantiate(crystalPrefab, position, Quaternion.Euler(0, randomRotY, 0), transform);
        }
    }
}