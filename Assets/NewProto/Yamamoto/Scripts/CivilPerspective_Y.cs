using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilPerspective_Y : MonoBehaviour
{
    public Civil_Y civilScr;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "WayPoint")
        {
            civilScr.avoidFlg = true;
            civilScr.Avoid(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "WayPoint")
        {
            civilScr.avoidFlg = false;
            civilScr.ResetForward();
        }
    }
}
