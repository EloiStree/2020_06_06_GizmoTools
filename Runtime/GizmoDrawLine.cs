using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawLine : MonoBehaviour
{

    [SerializeField] GizmoDrawingDefault drawingParameters= new GizmoDrawingDefault();
    [Header("Lag Drawing")]
    public bool m_useLagDrawing;
    public float m_lagDrawingTime = 2f;
    public float m_lagDrawingTimeFrame = 0.1f;
    private float m_drawCountDown;
    void Update()
    {
        if (m_useLagDrawing)
        { 
            m_drawCountDown -= Time.deltaTime;
            if (m_drawCountDown < 0)
            {
                m_drawCountDown = m_lagDrawingTimeFrame;
                GizmoDrawer.DrawAxis(drawingParameters, m_lagDrawingTime);

            }
        }

        if (drawingParameters.m_axisDirection == null)
            return;
        GizmoDrawer.DrawAxis(drawingParameters , Time.deltaTime);
    }

    

    private void Reset()
    {
        drawingParameters.m_axisDirection = transform;
    }
}


[System.Serializable]
public class GizmoDrawingDefault
{
    public Transform m_axisDirection;
    public float m_rayDistance = 1f;
    public bool m_scaleAffectDistance = true;
    public bool m_useAxeX = true;
    public bool m_useAxeY = true;
    public bool m_useAxeZ = true;
}

public class GizmoDrawer {

    public static void DrawAxis(GizmoDrawingDefault drawingInfo, float timeDisplay)
    {
        float scale = 1f;
        if (drawingInfo.m_useAxeX) { 
            if (drawingInfo.m_scaleAffectDistance)
                scale = drawingInfo.m_axisDirection.lossyScale.x * drawingInfo.m_rayDistance;
            else scale = drawingInfo.m_rayDistance;
            Debug.DrawLine(drawingInfo.m_axisDirection.position, drawingInfo.m_axisDirection.position + drawingInfo.m_axisDirection.right * scale, Color.red, timeDisplay);
        }
        if (drawingInfo.m_useAxeY)
        { 
            if (drawingInfo.m_scaleAffectDistance) 
                scale = drawingInfo.m_axisDirection.lossyScale.y * drawingInfo.m_rayDistance;
            else scale = drawingInfo.m_rayDistance;
            Debug.DrawLine(drawingInfo.m_axisDirection.position, drawingInfo.m_axisDirection.position + drawingInfo.m_axisDirection.up * scale, Color.green, timeDisplay);
        }
        if (drawingInfo.m_useAxeZ) 
        { 
            if (drawingInfo.m_scaleAffectDistance)
                scale = drawingInfo.m_axisDirection.lossyScale.z * drawingInfo.m_rayDistance;
            else scale = drawingInfo.m_rayDistance;
            Debug.DrawLine(drawingInfo.m_axisDirection.position, drawingInfo.m_axisDirection.position + drawingInfo.m_axisDirection.forward * scale, Color.blue, timeDisplay);
        }
    }
}
