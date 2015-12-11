using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	Vector2 dragOffset;
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;
    
    GameObject placeholder = null;
	
	//public enum Slot {WEAPON, HEAD, CHEST, LEGS, FEET, INVENTORY};
	//public Slot typeofItem = Slot.WEAPON;
	

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log ("OnBeginDrag");
        
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        
		
		dragOffset.x = this.transform.position.x - eventData.position.x;
		dragOffset.y = this.transform.position.y - eventData.position.y;
		
        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        transform.SetParent(transform.parent.parent);
        
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
        /*
        DropZone[] zone = GameObject.FindObjectOfType<DropZone>();
        foreach(DropZone myZone in zone)
        {
        
        }
        */
        
	} 
	
	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log ("OnDrag");
		
		this.transform.position = eventData.position + dragOffset;
		
		int newSiblingIndex = placeholderParent.childCount;
		
		if(placeholder.transform.parent != placeholderParent)
		{
			placeholder.transform.SetParent(placeholderParent);
		}
		
		for (int i=0; i < placeholderParent.childCount; i++)
		{
			if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
			{
				newSiblingIndex = i;
				
				if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
				{
					newSiblingIndex--;
				}
				break;
			}
		}
		
		placeholder.transform.SetSiblingIndex(newSiblingIndex);
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log ("OnEndDrag");
		transform.SetParent(parentToReturnTo);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		
		Destroy(placeholder);
		
		//EventSystem.current.RaycastAll(eventData);   // raycasts all objects under card
	}
}
