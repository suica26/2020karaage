using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerGimmickBase : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private GameObject _effect;
    [SerializeField, Range(0, 3)] private int _usageEvo;
    [SerializeField] private float _force;
    [SerializeField] private float _time;
    [SerializeField, Tooltip("オブジェクトの高さを設定(地面からエフェクトを出す場合)")] private float _ySize;
    [SerializeField] private float _ySizeAdditional;
    [SerializeField] private float _ySizeAdditionalObj;

    [Header("判定サイズ設定")]
    [SerializeField] private float _xScale;
    [SerializeField] private float _yScale;
    [SerializeField] private float _zScale;

    //オブジェクト(当たり判定)を生成
    protected GameObject InstanceObject()
    {
        Vector3 pos = transform.position + transform.up * (_yScale - _ySize + _ySizeAdditionalObj) / 2.0f;
        GameObject obj = Instantiate(_obj, pos, Quaternion.identity);
        obj.transform.localScale = new Vector3(_xScale, _yScale, _zScale);

        if (obj.GetComponent<BlowWind_R>())
        {
            obj.GetComponent<BlowWind_R>().force = _force;
            obj.GetComponent<BlowWind_R>().usageEvo = _usageEvo;
        }
        else
        {
            obj.GetComponent<Explode_R>().force = _force;
            obj.GetComponent<Explode_R>().usageEvo = _usageEvo;
        }

        Destroy(obj, _time);

        return obj;
    }

    //エフェクトを生成
    protected void InstanceEffect()
    {
        Vector3 pos = transform.position - new Vector3(0, _ySize / 2.0f, 0) + transform.up * _ySizeAdditional;
        GameObject effect = Instantiate(_effect, pos, Quaternion.identity);

        if(effect.GetComponent<ParticleSystem>() != null)
            effect.GetComponent<ParticleSystem>().Play();

        //子オブジェクトのパーティクルを再生
        foreach (var childObj in effect.GetComponentsInChildren<ParticleSystem>())
        {
            childObj.Play();
        }
        Destroy(effect, _time);
    }
}
