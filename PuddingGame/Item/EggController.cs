using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField]
    private GameObject egg;

    [SerializeField]
    private Transform dish;

    private GameObject clonedEgg;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("EggSpawn", 5f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (clonedEgg != null && clonedEgg.transform.position.y < dish.position.y)
        {
            Destroy(clonedEgg);
            clonedEgg = null;
        }
    }

    private void EggSpawn()
    {
        clonedEgg = Instantiate(egg, new Vector3(Random.Range(-10f, 10f), 15f,3), Quaternion.identity);
    }
}
