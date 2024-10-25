using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerActionCtrl triggerActionCtrl;
    /*public Collider2D Collider2DTrigger;
	public Collider2D Collider2DParent;*/
	/*private void Start()
	{
		Collider2DTrigger = GetComponent<BoxCollider2D>();
		//Collider2Dparent = GetComponent<BoxCollider2D>();

		if (Collider2DTrigger != null)
		{
			Vector3 currentPos = Collider2DTrigger.transform.position;
			//Vector3 objectPos = Collider2Dparent.transform.position;
			// Cộng thêm giá trị x + 0.5 và y + 0.5
			Vector3 newPos = new Vector3(currentPos.x + 0.5f, currentPos.y + 0.5f, currentPos.z);
			Collider2DTrigger.transform.position = newPos;
		}
		else
		{
			Debug.LogError("Không tìm thấy BoxCollider2D.");
		}
	}*/

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerActionCtrl.action.Act();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerActionCtrl.action.CancelAct();
    }
}
