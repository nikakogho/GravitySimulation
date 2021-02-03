using UnityEngine;

public class CelestialSpawner : MonoBehaviour
{
    public CelestialObject[] celestials;
    public int amount = 100;

    public float pointFactor;

    Vector3 minPoint, maxPoint;

    Vector3 RandomPoint
    {
        get
        {
            return new Vector3(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y), Random.Range(minPoint.z, maxPoint.z));
        }
    }

    void Awake()
    {
        float total = 0;

        foreach (CelestialObject cel in celestials) total += cel.chance;

        minPoint = Vector3.one * -pointFactor;
        maxPoint = Vector3.one * pointFactor;

        for(int i = 0; i < amount; i++)
        {
            float rand = Random.Range(0, total);
            float sum = 0;

            CelestialObject toPick = null;

            foreach (CelestialObject cel in celestials)
            {
                sum += cel.chance;

                if (sum >= rand)
                {
                    toPick = cel;
                    break;
                }
            }

            GameObject obj = Instantiate(toPick.prefab, RandomPoint, Quaternion.Euler(RandomPoint), transform);
            float factor = Random.Range(0f, 1f);

            obj.GetComponent<Rigidbody>().mass = (toPick.minMass + toPick.maxMass * factor) / 2;
            obj.transform.localScale *= (toPick.minScaleFactor + toPick.maxScaleFactor * factor) / 2;
        }
    }

    [System.Serializable]
    public class CelestialObject
    {
        public GameObject prefab;
        public float chance;
        public float minMass, maxMass;
        public float minScaleFactor, maxScaleFactor;
    }
}

