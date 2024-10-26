using UnityEngine;

public class Trigger : MonoBehaviour
{
	public TriggerActionCtrl triggerActionCtrl;

	public virtual void Awake()
	{
		if (transform.parent.parent.gameObject.CompareTag("TerrainTrap"))
		{
			triggerActionCtrl = transform.parent.parent.GetComponent<TriggerActionCtrl>();
		}
		else
		{
			triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
		}
	}
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		foreach (Action action in triggerActionCtrl.actions)
		{
			action.Act();
		}
	}
	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		foreach (Action action in triggerActionCtrl.actions)
		{
			action.CancelAct();
		}
	}
}