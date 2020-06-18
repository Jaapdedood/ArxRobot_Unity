using UnityEngine;
using System.Collections;

public class DragUI : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    public GameObject TrashCan;

    void Start()
    {
         TrashCan = GameObject.FindGameObjectWithTag("TrashCan");
         TrashCan.transform.localScale = new Vector3(0, 0, 0); //Cool trick to hide without disabling
    }

    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
        TrashCan.transform.localScale = new Vector3(1, 1, 1); //Show
    }

    public void OnDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }

    public void OnEndDrag()
    {
        float Distance = Vector3.Distance(transform.position, TrashCan.transform.position);
        if(Distance < 60 )
        {
            Destroy(gameObject);
        }

        TrashCan.transform.localScale = new Vector3(0, 0, 0); //Hide
    }
    
    public void OnDrop()
    {
        
    }
}