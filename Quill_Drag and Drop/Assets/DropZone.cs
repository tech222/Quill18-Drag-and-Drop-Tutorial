using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
	//public Draggable.Slot typeofItem = Draggable.Slot.INVENTORY;
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log ("OnDropEnter to " + gameObject.name);
		if (eventData.pointerDrag == null)
		{
			return;
		}
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.placeholderParent = this.transform;
		}
	}
		
	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log ("OnDropExit to " + gameObject.name);
		if (eventData.pointerDrag == null)
		{
			return;
		}
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null && d.placeholderParent == this.transform)
		{
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log (eventData.pointerDrag.name + "was dropped on " + gameObject.name);
		
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.parentToReturnTo = this.transform;
		}
			/*
			if(typeofItem == d.typeofItem || typeofItem==Draggable.Slot.INVENTORY)
			{
				d.parentToReturnTo = this.transform;
			}
			*/
	}

}
