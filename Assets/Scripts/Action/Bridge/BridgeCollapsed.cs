public class BridgeCollapsed : Action
{
    public float disableDelay = 0.3f;
    public float respawnDelay = 5f;

    public override void Act()
    {
        Invoke("Disable", disableDelay);
        Invoke("Respawn", respawnDelay);
    }

    private void Disable()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }
    void Respawn()
    {
        transform.parent.parent.gameObject.SetActive(true);
    }
}
