using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_R : MonoBehaviour
{
    [SerializeField] private GameObject preCutter = null;
    [SerializeField] private GameObject preCutterFA;
    [SerializeField] private GameObject setGround;

    [SerializeField] private Transform[] cutterTransform;
    [SerializeField] private float[] cutterSize;

    public bool throwingCutter = false;

    GameObject cutter;
    GameObject cutterFA;
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
        cutterFA = Instantiate(preCutterFA, cutterTransform[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 0)));
        cutterFA.transform.localScale = cutter.transform.localScale * cutterSize[scrEvo.EvolutionNum];
        cutterFA.GetComponent<CutterMoveFA_R>().ground = setGround;
        cutterFA.GetComponent<CutterMoveFA_R>().evoSpeed = cutterSize[scrEvo.EvolutionNum];
        cutterFA.GetComponent<CutterMoveFA_R>().backArea = cutterTransform[scrEvo.EvolutionNum];
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == cutter || other.gameObject == cutterFA)
        {
            Destroy(other.gameObject);
            throwingCutter = false;
        }
    }
}
