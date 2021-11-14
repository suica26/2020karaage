using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePanel_M : MonoBehaviour
{
    public Image damage;

    void Start()
    {
        
    }

    void Update()
    {
        damage.color = Color.Lerp(damage.color, Color.clear, Time.deltaTime * 2);
    }

    public void DamageEffect()
    {
        damage.color = new Color(0.6f, 0f, 0f, 0.6f);
    }
}
