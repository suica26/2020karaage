using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attention_Field_M : MonoBehaviour
{
    [SerializeField] GameObject wall1, wall2, wall3, wall4, wall5, player, attention;
    [SerializeField] public float safety;
    void Start()
    {
        attention.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 wallPos1 = wall1.transform.position;
        Vector3 wallPos2 = wall2.transform.position;
        Vector3 wallPos3 = wall3.transform.position;
        Vector3 wallPos4 = wall4.transform.position;
        Vector3 wallPos5 = wall5.transform.position;
        float dist1 = Vector3.Distance(playerPos, wallPos1);
        float dist2 = Vector3.Distance(playerPos, wallPos2);
        float dist3 = Vector3.Distance(playerPos, wallPos3);
        float dist4 = Vector3.Distance(playerPos, wallPos4);
        float dist5 = Vector3.Distance(playerPos, wallPos5);

        if (dist1 <= safety)
        {
            attention.SetActive(true);
        }
        else if (dist2 <= safety)
        {
            attention.SetActive(true);
        }
        else if (dist3 <= safety)
        {
            attention.SetActive(true);
        }
        else if (dist3 <= safety)
        {
            attention.SetActive(true);
        }
        else if (dist3 <= safety)
        {
            attention.SetActive(true);
        }

        else
        {
            attention.SetActive(false);
        }
    }
}
