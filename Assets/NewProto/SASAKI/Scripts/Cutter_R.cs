using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_R : MonoBehaviour
{
    [SerializeField] private GameObject preCutter = null;

    [SerializeField] private Transform[] cutterTransform;
    [SerializeField] private float[] cutterSize;

    public bool throwingCutter = false;

    GameObject cutter;
    EvolutionChicken_R scrEvo;

    // Start is called before the first frame update
    void Start()
    {
        scrEvo = GetComponent<EvolutionChicken_R>();
        throwingCutter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !throwingCutter)
        {
            throwingCutter = true;
            cutter = Instantiate(preCutter, cutterTransform[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -90)));
            cutter.transform.localScale = cutter.transform.localScale * cutterSize[scrEvo.EvolutionNum];
            cutter.GetComponent<CutterMove1_R>().evoSpeed = cutterSize[scrEvo.EvolutionNum];
            cutter.GetComponent<CutterMove1_R>().backArea = cutterTransform[scrEvo.EvolutionNum];
        }
    }

    public void CutterAttack()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == cutter)
        {
            Destroy(other.gameObject);
            throwingCutter = false;
        }
    }
}
