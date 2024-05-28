using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{

    [SerializeField] private float m_ClampAngles = 90f;
    [SerializeField] private float m_VerticalSpeed = 10f;
    [SerializeField] private float m_HorizontalSpeed = 10f;

    private InputManager m_InputManager;
    private Vector3 m_StartingRotation;

    protected override void Awake()
    {
        m_InputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (m_StartingRotation == null)
                {
                    m_StartingRotation = transform.localRotation.eulerAngles;
                    Vector2 deltaInput = m_InputManager.GetMouseDelta();
                    m_StartingRotation.x += deltaInput.x * m_HorizontalSpeed *  Time.deltaTime;
                    m_StartingRotation.y += deltaInput.y * m_VerticalSpeed * Time.deltaTime;
                    m_StartingRotation.y = Mathf.Clamp(m_StartingRotation.y, -m_ClampAngles, m_ClampAngles);
                    state.RawOrientation = Quaternion.Euler(m_StartingRotation.y, m_StartingRotation.x, 0f);

                }
            }
        }
    }
    
}
