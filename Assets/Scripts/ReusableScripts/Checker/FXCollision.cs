using UnityEngine;

public class FXCollision : MonoBehaviour
{
    #region Collision Particle
    private void OnParticleCollision(GameObject other)
    {

        if (other.CompareTag("DangerObject"))
        {
            KillPlayer.Instance.KillByBoss(CameraCtrl.Instance.GetMainVirtual());
            NoticeCtrl.Instance.SetTextWhenDie("Skill of Boss");
        }
    }

    #endregion
}
