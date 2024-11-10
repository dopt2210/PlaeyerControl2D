using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera[] _cameras;

    [Header("Control Y damping while jump/fall")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDampingThreshold = -15f;

    public bool _isLearpingYDamping {  get; private set; }
    public bool _isLearpFromPlayerFalling { get; set; }

    public CinemachineVirtualCamera _currentCamera;
    public CinemachineFramingTransposer _framingTranposer;

    private float _normYPanAmount = 2f;

    private Coroutine LearpYPanCoroutine;
    private Coroutine PanCameraCoroutine;

    private Vector2 _startTrackedObjectOffset;
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject);  return; }
        Instance = this;

        for(int i = 0; i < _cameras.Length; i++)
        {
            if (_cameras[i].enabled)
            {
                _currentCamera = _cameras[i];

                _framingTranposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        _normYPanAmount = _framingTranposer.m_YDamping;

        _startTrackedObjectOffset = _framingTranposer.m_TrackedObjectOffset;
        
        
    }
    #region Learp Y Damping
    public void LearpYDamping(bool isPlayerFalling)
    {
        LearpYPanCoroutine = StartCoroutine(LearpYAction(isPlayerFalling));
    }
    private IEnumerator LearpYAction(bool isPlayerFalling)
    {
        _isLearpingYDamping = true;
        float startDamping = _framingTranposer.m_YDamping;
        float endDamping = 0f;

        if (isPlayerFalling)
        {
            endDamping = _fallPanAmount;
            _isLearpFromPlayerFalling = true;
        }
        else
        {
            endDamping = _normYPanAmount;
        }

        float elapsedTime = 0f;
        while (elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            float learpPanAmount = Mathf.Lerp(startDamping, endDamping, (elapsedTime/_fallYPanTime));
            _framingTranposer.m_YDamping = learpPanAmount;
            yield return null;
        }

        _isLearpingYDamping = false;
    }
    #endregion
    #region PanCamera
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool isPanToStartPos)
    {
        PanCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, isPanToStartPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool isPanToStartPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startPos = Vector2.zero;

        if (!isPanToStartPos)
        {
            switch (panDirection)
            {
                case PanDirection.Right:
                    endPos = Vector2.left;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                default:
                    break;
            }
            endPos *= panDistance;
            startPos = _startTrackedObjectOffset;
            endPos += startPos;
        }
        else
        {
            startPos = _framingTranposer.m_TrackedObjectOffset;
            endPos = _startTrackedObjectOffset;

        }
        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 panLearp = Vector3.Lerp(startPos, endPos, (elapsedTime/panTime));
            _framingTranposer.m_TrackedObjectOffset = panLearp;
            Debug.Log("pan1" + _framingTranposer.m_TrackedObjectOffset);
            Debug.Log("panlearp" + panLearp);
            yield return null; 
        }
    }
    #endregion
    #region Swap Cameras
    public void SwapCamera(CinemachineVirtualCamera leftCamera, CinemachineVirtualCamera rightCamera, Vector2 triggerExit)
    {
        if (_currentCamera == leftCamera && triggerExit.x > 0f)
        {
            rightCamera.enabled = true;
            leftCamera.enabled = false;

            _currentCamera = rightCamera;
            _framingTranposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else if (_currentCamera == rightCamera && triggerExit.x < 0f)
        {
            leftCamera.enabled = true;
            rightCamera.enabled = false;

            _currentCamera = leftCamera;
            _framingTranposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }
    #endregion
}
