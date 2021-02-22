using UnityEngine;

public class FoodMaker_R : MonoBehaviour
{
    [SerializeField] private GameObject objFood;
    [SerializeField] private int minFood;
    [SerializeField] private int maxFood;

    public void DropFood()
    {
        int count = Random.Range(minFood, maxFood);
        float range = 3f;

        for(int i = 0; i < count; i++)
        {
            //ヤマモト加筆
            var genPos = transform.position;
            genPos.x += Random.Range(-range, range);
            genPos.y += Random.Range(-range, range);
            genPos.z += Random.Range(-range, range);
            //生成場所をtransfotm.positionからgenPosに
            var food = Instantiate(objFood, genPos, new Quaternion(Random.Range(0,360) * Mathf.Deg2Rad, 0f, Random.Range(0, 360) * Mathf.Deg2Rad, 1));

            Destroy(food, 20f);
        }
    }
}