using UnityEngine;

public class Bone : MonoBehaviour
{
    protected Vector3 PanelOffset = new Vector3(0f, 0f, 0.6f);

    public GameObject MainObject => this.mainObject;

    [SerializeField]
    private GameObject mainObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdatePosition()
    {
        UnifiedCamera unifiedCamera = UnifiedCamera.Instance();

        Vector3 forwardOffset = (unifiedCamera.m_Camera.transform.right * this.PanelOffset.x) +
                                    (unifiedCamera.m_Camera.transform.up * this.PanelOffset.y) +
                                    (unifiedCamera.GetCameraForward() * this.PanelOffset.z);

        this.transform.position = unifiedCamera.GetCameraPosition() + forwardOffset;
    }
}