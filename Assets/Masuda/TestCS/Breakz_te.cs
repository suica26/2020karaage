using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breakz_te : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] public Text scoreText;
    [SerializeField] float count, counter, n, add, gain, onoff, timer;
    [SerializeField] public Transform trf;
    private RectTransform txtTrf;
    private Vector3 offset = new Vector3(0.5f, 0.8f, 0f);

    void Start()
    {
        count = 0;
        add = 0;
        scoreText.color = new Color(0, 0, 0, 1);
        txtTrf = scoreText.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            count++;
            counter = count;
            add += counter;
        }

        if (counter == n)
        {
            timer += Time.deltaTime;
            Color();
            Destroy(obj,0.1f);
            txtTrf.position = RectTransformUtility.WorldToScreenPoint
                 (Camera.main, trf.position + offset);
            if (onoff == 1)
            {
                scoreText.text = "+" + 0;
                add = 0;
            }
            else if (onoff == 0)
            {
                scoreText.text = "+" + add;
            }
        }
    }

    void Color()
    {
        gain += Time.deltaTime;
        scoreText.color = new Color(0, 0, 0, 3f - gain);
        if (gain >= 3f)
        {
            gain = 3f;
            scoreText.text = "+" + 0;
        }
        if (timer >= 3.0f)
        {
            timer = 3.0f;
            onoff = 1;
        }
    }
}
