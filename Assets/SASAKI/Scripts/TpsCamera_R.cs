using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TpsCamera_R : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float spinSpeed = 1.0f;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;

    public List<Renderer> rendererHitsList = new List<Renderer>();
    public Renderer[] rendererHitsPrevs;
    public int layer = 1 << 8;

    public List<Renderer> materialHitsList = new List<Renderer>();
    public Renderer[] materialHitsPrevs;

    // Use this for initialization
    void Start()
    {
        // Canera get Start Position from Player
        nowPos = transform.position;

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Debug.Log("player didn't setting. Auto search 'Player' tag.");
        }

        mouse.y = 0.5f; // start mouse y pos ,0.5f is half
    }

    // Update is called once per frame
    void Update()
    {

        // Get MouseMove
        mouse -= new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        // Clamp mouseY move
        mouse.y = Mathf.Clamp(mouse.y, 0.5f, 0.95f);

        // sphere coordinates
        pos.x = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
        pos.y = Mathf.Cos(mouse.y * Mathf.PI);
        pos.z = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);
        // r and upper
        pos *= nowPos.z;

        pos.y += nowPos.y;
        //pos.x += nowPos.x; // if u need a formula,pls remove comment tag.

        transform.position = pos + target.position;
        transform.LookAt(target.position);

        TransparentObject();
    }

    void TransparentObject()
    {
        Vector3 distance = target.transform.position - this.transform.position;
        Ray ray = new Ray(this.transform.position, distance);

        RaycastHit[] hits = Physics.RaycastAll(ray, distance.magnitude);

        rendererHitsPrevs = rendererHitsList.ToArray();
        rendererHitsList.Clear();

        materialHitsPrevs = materialHitsList.ToArray();
        materialHitsList.Clear();

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.gameObject == target.gameObject)
            {
                continue;
            }

            //Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
            Renderer material = hit.collider.gameObject.GetComponent<Renderer>();
            /*if(renderer != null)
            {
                rendererHitsList.Add(renderer);
                renderer.enabled = false;
            }*/

            if (material != null)
            {
                materialHitsList.Add(material);
                Color color = new Color(material.material.color.r, material.material.color.g, material.material.color.b, 0.5f);
                material.material.color = color;
            }
        }

        /*foreach(Renderer renderer in rendererHitsPrevs.Except<Renderer>(rendererHitsList))
        {
            if(renderer != null)
            {
                renderer.enabled = true;
            }
        }*/

        foreach (Renderer material in materialHitsPrevs.Except<Renderer>(materialHitsList))
        {
            if (material != null)
            {
                material.material.color = new Color(material.material.color.r, material.material.color.g, material.material.color.b, 1.0f);
            }
        }
    }
}
