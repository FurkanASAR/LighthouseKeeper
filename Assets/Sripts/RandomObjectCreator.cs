using UnityEngine;

public class RandomObjectCreator : MonoBehaviour
{
    [SerializeField] float radius = 1f;
    [SerializeField] GameObject wood;


    public int maxObjectNumber = 40;
    private int currentObjectNumber = 0;
    private void Awake()
    {
        transform.position = Vector3.zero;
        
        while(currentObjectNumber <= maxObjectNumber)
        {
            CreateRandomObject();
            currentObjectNumber++;
        }        
    }

    private void CreateRandomObject()
    {
        float randomAngle = Random.Range(0f, 360f);

        Vector3 randomDirection = new Vector3(Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0f, Mathf.Cos(randomAngle * Mathf.Deg2Rad));


        GameObject randomObject = Instantiate(wood);

        randomObject.transform.forward = randomDirection;
        randomObject.transform.position = randomObject.transform.forward * radius;


    }

}
