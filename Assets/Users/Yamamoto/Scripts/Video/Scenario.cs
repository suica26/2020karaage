using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Scenario : MonoBehaviour
{
    public TextAsset scenarioText;
    public Text canvasText;
    [SerializeField] private Image canvasImage;
    private string[] lines;
    private int lineNumber;
    private int wordNumber;
    private int spriteNumber;
    private string displayedText = "";
    private float textTimer;
    private float spriteTimer;
    private int spriteAorB;     //1ならA、-1ならB
    [SerializeField] private float textSpeed;
    [SerializeField] private float spriteSpeed;
    [SerializeField] private Sprite[] spriteA;
    [SerializeField] private Sprite[] spriteB;
    // Start is called before the first frame update
    private void Start()
    {
        //行ごとのテキストを読み取り
        lines = scenarioText.text.Split('\n');
        canvasImage.sprite = spriteA[spriteNumber];
        spriteAorB = 1;
    }
    private void Update()
    {
        //タイマー処理
        textTimer += Time.deltaTime;
        spriteTimer += Time.deltaTime;

        //画像を次に移すかどうかの判定
        if (lines[lineNumber].Contains("#"))
        {
            lineNumber++;
            spriteNumber++;

            spriteAorB = 1;
            canvasImage.sprite = spriteA[spriteNumber];
            spriteTimer = 0f;
        }

        //文字を進ませる処理
        if (textTimer >= textSpeed && wordNumber < lines[lineNumber].Length)
        {
            AddNextWord();
            textTimer = 0f;
        }

        //画像の入れ替わり処理
        if (spriteTimer >= spriteSpeed)
        {
            spriteAorB *= -1;
            if (spriteAorB > 0) canvasImage.sprite = spriteA[spriteNumber];
            else canvasImage.sprite = spriteB[spriteNumber];
            spriteTimer = 0f;
        }

        canvasText.text = displayedText;

        if (Input.GetMouseButtonDown(0))
        {
            CheckStatus();
        }
    }

    private void AddNextWord()
    {
        displayedText += lines[lineNumber][wordNumber];
        wordNumber++;
    }

    private void CheckStatus()
    {
        if (wordNumber < lines[lineNumber].Length)
        {
            while (wordNumber < lines[lineNumber].Length)
            {
                AddNextWord();
            }
        }
        else
        {
            displayedText = "";
            wordNumber = 0;
            lineNumber++;
            //次の行に移る処理
            if (lineNumber >= lines.Length)
            {
                lineNumber = 0;
                spriteNumber = 0;
            }
        }
    }
}
