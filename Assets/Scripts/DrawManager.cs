using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawManager : MonoBehaviour
{
    public readonly Color DefaultColor = new Color(0f, 0f, 0f);

    public float LineWidth = .5f;

    public GameObject DrawPlane;
    public Camera PlayerCamera;
    public GameObject SelectedMember;

    public LineRenderer LineRenderer
    {
        get; private set;
    }

	private int Current;
    private List<Vector2> Positions;

    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (IsMemberSelected)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Input.mousePosition;

                Positions.Add(mousePosition);
                LineRenderer.SetPosition(Current, mousePosition);

                Current++;
            }
            else
            {
                SelectedMember = null;
                SetAction();
            }
        }
    }

    public bool IsMemberSelected
    {
        get
        {
            return SelectedMember != null;
        }
    }

	public void SetAction() {
        WalkAction walkAction = new WalkAction(this.Positions);
	}
}