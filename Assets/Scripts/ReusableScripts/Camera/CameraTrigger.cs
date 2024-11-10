using Cinemachine;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera leftCamera;
    [HideInInspector] public CinemachineVirtualCamera rightCamera;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public enum PanDirection
{
    Right,
    Left,
    Up,
    Down
}
public class CameraTrigger : MonoBehaviour
{
    [SerializeField] public CustomInspectorObjects customInspectorObjects;
    private Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraCtrl.Instance.PanCameraOnContact(customInspectorObjects.panDistance,
                    customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - _collider.bounds.center).normalized;
            Debug.Log(""+ exitDirection);
            if (customInspectorObjects.swapCameras && customInspectorObjects.leftCamera != null && customInspectorObjects.rightCamera != null)
            {
                CameraCtrl.Instance.SwapCamera(customInspectorObjects.leftCamera, customInspectorObjects.rightCamera, exitDirection);
            }

            
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraCtrl.Instance.PanCameraOnContact(customInspectorObjects.panDistance,
                    customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
                
            }
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(CameraTrigger))]
public class MyScriptEditor : Editor
{
    CameraTrigger cameraTrigger;

    private void OnEnable()
    {
        cameraTrigger = (CameraTrigger)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (cameraTrigger.customInspectorObjects.swapCameras)
        {
            cameraTrigger.customInspectorObjects.leftCamera = EditorGUILayout.ObjectField("Left Camera", cameraTrigger.customInspectorObjects.leftCamera,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraTrigger.customInspectorObjects.rightCamera = EditorGUILayout.ObjectField("Right Camera", cameraTrigger.customInspectorObjects.rightCamera,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }
        if (cameraTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction", cameraTrigger.customInspectorObjects.panDirection);
            cameraTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraTrigger.customInspectorObjects.panDistance);
            cameraTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", cameraTrigger.customInspectorObjects.panTime);
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraTrigger);
        }
    }
}
#endif
