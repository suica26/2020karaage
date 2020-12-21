using UnityEngine;

public class FoodMaker_R : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Parameters_R scrParameters;
    [SerializeField] private GameObject objFood;
    [SerializeField] private int minFood;
    [SerializeField] private int maxFood;

    public void DropFood()
    {
        int count = Random.Range(minFood, maxFood);

        for(int i = 0; i < count; i++)
        {
            var food = Instantiate(objFood, transform.position, new Quaternion(Random.Range(0,360) * Mathf.Deg2Rad, 0f, Random.Range(0, 360) * Mathf.Deg2Rad, 1));

            food.GetComponent<Food_R>().Player = Player;
            food.GetComponent<Food_R>().scrEP = scrParameters;

            Destroy(food, 20f);
        }
    }
}