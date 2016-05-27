using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Assets.Scripts;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
  public float LineWidth = .5f;
  // Lower value makes the line more round, but consumes more resources
  [Tooltip("Lower value makes the line more round, but consumes more resources")] public float LineRoundness = .3f;

  public GameObject DrawPlane;
  public Camera PlayerCamera;

  private Member SelectedMember;
  private List<Vector3> Positions;

  private Vector3 LastPosition
  {
    get { return (Positions.Count > 0) ? Positions[Positions.Count - 1] : Vector3.zero; }
  }

  private List<GameObject> LineRenderers;

  void Start()
  {
    this.Positions = new List<Vector3>();
    this.LineRenderers = new List<GameObject>();
  }

  public void ClearLine()
  {
    Positions.Clear();
    // Destroys all Linerenderer objects
    LineRenderers.ForEach(Destroy);
  }

  void Update()
  {
    if (IsMemberSelected)
    {
      if (Input.GetMouseButton(0))
      {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
          Vector3 hitpos = hit.point;
          // Check distance so lines aren't drawn when user holds mouse still
          if (Vector2.Distance(hitpos, LastPosition) <= LineRoundness)
          {
            Debug.Log("Returned");
            return;
          }
          CreateLine(LastPosition, hitpos);
          Positions.Add(hitpos);
        }
      }
      else
      {
        SetAction();
        SelectedMember = null;
      }
    }
  }

  public bool IsMemberSelected
  {
    get { return SelectedMember != null; }
  }

  private void CreateLine(Vector2 begin, Vector2 end)
  {
    GameObject gameObject = new GameObject();
    gameObject.AddComponent<LineRenderer>();

    LineRenderer line = gameObject.GetComponent<LineRenderer>();
    line.SetVertexCount(2);
    line.SetWidth(0.08f, 0.08f);
    line.useWorldSpace = true;
    line.SetPosition(0, begin);
    line.SetPosition(1, end);
    line.transform.SetParent(transform);
    line.transform.position += new Vector3(0, -0.1f, 0f);

    LineRenderers.Add(gameObject);
  }

  public void SetAction()
  {
    List<Vector2> vector2s = new List<Vector2>(Positions.Count);
    Positions.ForEach(o =>
    {
      vector2s.Add((Vector2) o);
    });
    SelectedMember.SetAction(vector2s);
  }

  public void SetMember(Member member)
  {
    this.SelectedMember = member;
    Positions.Add(member.transform.position);
  }
}