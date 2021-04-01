using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breakz_te : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] public Text scoreText;
    [SerializeField] public float count, counter, n, add, gain;
    [SerializeField] public Transform trf;
    private RectTransform txtTrf;
    private Vector3 offset = new Vector3(0f, 0.5f, 0f);

    void Start()
    {
        count = 0;
        add = 0;
        scoreText.color = new Color(0, 0, 0, 0);
        txtTrf = scoreText.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            count++;
            counter = count;
            add += count;
        }

        if (counter == n)
        {
            //gain = 0;
            Color();
            txtTrf.position = RectTransformUtility.WorldToScreenPoint
            (Camera.main, trf.position + offset);
            scoreText.text = "+" + add;
            obj.SetActive(false);
        }

        if (gain >= 1.5f)
        {
            gain = 1.5f;
            scoreText.text = "+" + 0;
            add = 0;
        }
    }

    void Color()
    {
        gain += Time.deltaTime / 2;
        scoreText.color = new Color(0, 0, 0, 1.5f - gain);
    }
}
