using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public float StaminaModifier = .1f;
    public float LineWidth = .08f;
    // Lower value makes the line more round, but consumes more resources
    [Tooltip("Lower value makes the line more round, but consumes more resources")]
    public float LineRoundness = .3f;
    public GameObject DrawPlane;
    public Camera PlayerCamera;

    private Member SelectedMember;
    private List<MemberLine> MemberLines; 

    void Start()
    {
        this.MemberLines = new List<MemberLine>();
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
                    if (Vector2.Distance(hitpos, CurrentMemberLine.LastPosition) <= LineRoundness) return;
                    if (HasEnoughStamina)
                    {
                        Debug.Log("Out of stamina");
                        CompleteLine();
                        return;
                    }
                    CreateLine(CurrentMemberLine.LastPosition, hitpos);
                }
            }
            else
            {
                CompleteLine();
            }
        }
    }

    private MemberLine CurrentMemberLine
    {
        get { return MemberLines.Find(o => o.Member == SelectedMember); }
    }

    /// <summary>
    /// Clears all lines that are being drawn
    /// </summary>
    public void DestroyLines()
    {
        // Destroys all Linerenderer objects
        MemberLines.ForEach(ml =>
        {
            DestroyLines(ml);
        });
        MemberLines.Clear();
    }

    public void ClearLine(Member m)
    {
        // Destroys all Linerenderer objects
        MemberLine memberLine = MemberLines.Find(o => o.Member == m);
        if (memberLine != null)
        {
            DestroyLines(memberLine);
        }
        MemberLines.Remove(memberLine);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ml"></param>
    private void DestroyLines(MemberLine ml)
    {
        ml.LineRenderers.ForEach(o =>
        {
            Destroy(o.gameObject);
        });
    }

    /// <summary>
    /// Completes the current line
    /// </summary>
    public void CompleteLine()
    {
        Debug.Log("Completed line, setting action");
        SetAction();
        Debug.Log("Performing action");
        SelectedMember.PerformActions();
        SelectedMember = null;
    }

    /// <summary>
    /// Checks if a member is selected
    /// </summary>
    public bool IsMemberSelected
    {
        get { return SelectedMember != null; }
    }

    /// <summary>
    /// Creates a line using the Unity linerenderer
    /// </summary>
    /// <param name="begin">Begin point the line</param>
    /// <param name="end">End point of the line</param>
    private void CreateLine(Vector2 begin, Vector2 end)
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<LineRenderer>();

        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetWidth(LineWidth, LineWidth);
        line.useWorldSpace = true;
        Debug.Log("Begin: " + begin.x + ", " + begin.y);
        Debug.Log("End: " + end.x + ", " + end.y);
        line.SetPosition(0, begin);
        line.SetPosition(1, end);
        line.transform.SetParent(transform);
        line.transform.position += new Vector3(0, -0.1f, 0f);

        CurrentMemberLine.LineRenderers.Add(line);
        CurrentMemberLine.Positions.Add(end);
    }

    /// <summary>
    /// Sets action to the member for processing
    /// </summary>
    public void SetAction()
    {
        List<Vector2> vector2s = new List<Vector2>(CurrentMemberLine.Positions.Count);
        CurrentMemberLine.Positions.ForEach(o => { vector2s.Add((Vector2) o); });
        WalkAction walkAction = new WalkAction(SelectedMember, vector2s);
        SelectedMember.SetAction(walkAction);
    }

    /// <summary>
    /// Sets the current member for the line
    /// </summary>
    /// <param name="member">Member to select</param>
    public void SetMember(Member member)
    {
        this.SelectedMember = member;
        MemberLine ml = MemberLines.Find(o => o.Member == SelectedMember);
        if (ml != null)
        {
            ml.LineRenderers.ForEach(o =>
            {
                Destroy(o.gameObject);
            });
            ml.Reset(member.transform.position);
        }
        else
        {
            MemberLines.Add(new MemberLine(member).Reset(member.transform.position));
        }
    }


    /// <summary>
    /// Checks if the current member has enough stamina to complete this line
    /// </summary>
    public bool HasEnoughStamina
    {
        get { return CalculateLineDistance() > SelectedMember.Stamina; }
    }

    /// <summary>
    /// Calculates the stamina needed for the current line
    /// </summary>
    /// <returns>Stamina need for the line, rounds up!</returns>
    public int CalculateLineDistance()
    {
        float distance = 0;
        List<Vector3> Positions = CurrentMemberLine.Positions;
        // Calculate total distance for the line
        for (int i = 1; i < Positions.Count; i++)
        {
            Vector2 v1 = Positions[i - 1];
            Vector2 v2 = Positions[i];
            distance += Vector2.Distance(v1, v2);
        }
        distance *= StaminaModifier;
        Debug.Log("Stamina left: " + distance + " of " + SelectedMember.Stamina);
        // Round up stamina needed
        return (int) Math.Ceiling(distance);
    }
}