using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy2 : MonoBehaviour
{
    [SerializeField] private GameObject Bill;
    [SerializeField] public Text scoreText;
    [SerializeField] float count, counter, n, add, gain, timer;
    [SerializeField] public Transform trf;
    private RectTransform txtTrf;
    private Vector3 offset = new Vector3(0.5f, 0.8f, 0f);

    void Start()
    {
        scoreText.color = new Color(0, 0, 0, 1);
        txtTrf = scoreText.GetComponent<RectTransform>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jama"))
        {
            Bill.SetActive(false);
            txtTrf.position = RectTransformUtility.WorldToScreenPoint
                 (Camera.main, trf.position + offset);
            scoreText.text = "+" + n;
            Color();
        }
    }
    void Color()
    {
        gain += Time.deltaTime;
        scoreText.color = new Color(0, 0, 0, 3.5f - gain);
        if (gain >= 3f)
        {
            timer = 1;
            gain = 3f;
            scoreText.text = "+" + 0;
        }
    }
}
