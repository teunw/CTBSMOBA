using System;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public readonly Color DefaultColor = new Color(0f, 0f, 0f);

    public float LineWidth = .5f;
    public float LineDistance = .5f;

    public GameObject DrawPlane;
    public GameObject RendererParent;
    public Camera PlayerCamera;
    public Member SelectedMember;

    private List<LineRenderer> LineRenderers; 
    private List<Vector2> Positions;
    private Vector2 PreviousMouseLocation;

    void Start()
    {
        this.Positions = new List<Vector2>();
        this.LineRenderers = new List<LineRenderer>();
    }

    void Update()
    {
        if (IsMemberSelected)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = PlayerCamera.ScreenPointToRay(DrawPlane.GetComponent<Transform>().position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 currentMouseLocation = hit.point;
                    if (Vector2.Distance(currentMouseLocation, PreviousMouseLocation) < LineDistance) return;
                    Positions.Add(currentMouseLocation);

                    GameObject gameObject = new GameObject();
                    gameObject.GetComponent<Transform>().parent = RendererParent.transform; 
                    gameObject.AddComponent<LineRenderer>();
                    LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
                    lineRenderer.SetPositions(new Vector3[]
                    {
                        PreviousMouseLocation,
                        currentMouseLocation 
                    });
                    LineRenderers.Add(lineRenderer);

                    PreviousMouseLocation = currentMouseLocation;
                }
            }
            else
            {
                SelectedMember = null;
                SetAction();
                ClearLine();
            }
        }
    }

    /// <summary>
    /// Checks if member is selected, return true if selected
    /// </summary>
    public bool IsMemberSelected
    {
        get
        {
            return SelectedMember != null;
        }
    }

    /// <summary>
    /// Clears the line on screen and in memory
    /// </summary>
    private void ClearLine()
    {
        this.Positions = new List<Vector2>();
        LineRenderers.ForEach(o =>
        {
            Destroy(o.gameObject);
        });
        LineRenderers.Clear();
    }

    /// <summary>
    /// Sets action 
    /// </summary>
	public void SetAction() {
        SelectedMember.SetAction(this.Positions);
	}
}