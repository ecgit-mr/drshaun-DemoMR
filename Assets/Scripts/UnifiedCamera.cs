using UnityEngine;

public class UnifiedCamera : UnitySingleton<UnifiedCamera>
{
    public enum CameraType
    {
        None,
        Hololens,
        OculusRift,
        Tango,
        Vive,
    }

    public CameraType m_CameraType = CameraType.Hololens;
    public Camera m_Camera;
    public Camera m_UIRenderCamera;

    protected Vector3 lastPosition = Vector3.zero;
    protected Quaternion lastRotation = Quaternion.identity;
    protected bool m_bIsDesktopApp = false;

    public GameObject GetCameraGameObject()
    {
        return m_Camera.gameObject;
    }

    public Vector3 GetCameraPosition()
    {
        return m_Camera.transform.position;
    }

    public Quaternion GetCameraRotation()
    {
        return m_Camera.transform.rotation;
    }

    public Vector3 GetCameraEulers()
    {
        return m_Camera.transform.eulerAngles;
    }

    public Vector3 GetCameraForward()
    {
        return m_Camera.transform.forward;
    }

    public Transform GetCenterTransform()
    {
        return m_Camera.transform;
    }

    public float GetFieldOfView()
    {
        return m_Camera.GetComponent<Camera>().fieldOfView;
    }

    public bool IsStereo()
    {
        return UnityEngine.XR.XRDevice.isPresent;
    }

    public Vector2 WorldToScreen(Vector3 oPosition)
    {
        return Camera.main.WorldToScreenPoint(oPosition);
    }

    /// <summary>
    /// To update the camera's background color (with alpha) value
    /// </summary>
    /// <param name="inColor">Incoming color value that needs to be set</param>
    public void SetCameraColor(Color inColor)
    {
        m_Camera.backgroundColor = inColor;
    }
}
