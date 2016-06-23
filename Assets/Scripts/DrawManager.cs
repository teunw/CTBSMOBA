#region

using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Skills;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

public class DrawManager : MonoBehaviour
{
    public GameObject DrawPlane;
    public GameScript GameScript;
    public GameObject SelectedMemberGameObject;
    public SkillIndicator SelectedMemberIndicator;
    public Material whiteLine;
    // Lower value makes the line more round, but consumes more resources
    [Tooltip("Lower value makes the line more round, but consumes more resources")]
    public float LineRoundness = .3f;

    public float LineWidth = .08f;
    private List<MemberLine> MemberLines = new List<MemberLine>();
    public Camera PlayerCamera;

    private Member SelectedMember;
    public float StaminaModifier = .1f;
    private int drawLayer;
    private int characterLayer;

    private bool noLongerOnCharacter = false;
    private bool lineCompleted = true;

    void Start()
    {
        drawLayer = 1 << 10;
        characterLayer = 1 << 11;
        if (SelectedMemberIndicator == null) Debug.LogWarning("No indicator for member selected");
    }

    private MemberLine CurrentMemberLine
    {
        get { return MemberLines.Find(o => o.Member == SelectedMember); }
    }

    /// <summary>
    ///     Checks if a member is selected
    /// </summary>
    public bool IsMemberSelected
    {
        get { return SelectedMember != null; }
    }


    /// <summary>
    ///     Checks if the current member has enough stamina to complete this line
    /// </summary>
    public bool HasEnoughStamina
    {
        get { return CalculateLineDistance() <= SelectedMember.Stamina; }
    }

    public float GetStaminaPercent
    {
        get
        {
            float calcLineDistance = CalculateLineDistance();
            float stamina = (SelectedMember == null) ? 1 : SelectedMember.Stamina;
            try
            {
                return 100 - (calcLineDistance / stamina) * 100;
            }
            catch (DivideByZeroException e)
            {
                return 0;
            }
        }
    }

    private void Awake()
    {
        GameScript.ProgressBar.gameObject.SetActive(IsMemberSelected);
        GameScript.kickButton.gameObject.SetActive(IsMemberSelected);
        GameScript.besteGameButton.gameObject.SetActive(IsMemberSelected);
    }

    private void Update()
    {
        if (GameScript.teamStatus == Assets.TeamStatus.Executing)
        {
            return;
        }

        if (SelectedMemberIndicator != null)
        {
            if (SelectedMember != null)
            {
                if (CurrentMemberLine.LineRenderers.Count > 0)
                {
                    SelectedMemberIndicator.SetActive(
                        CurrentMemberLine.LastPosition,
                        CurrentMemberLine.GetAlmostLastPosition,
                        SelectedMember.GetSkill()
                        );
                }
            }
            else
            {
                SelectedMemberIndicator.SetInactive();
            }
        }

        SelectedMemberGameObject.SetActive(IsMemberSelected);
        if (IsMemberSelected)
        {
            SelectedMemberGameObject.transform.position = SelectedMember.transform.position;
        }

        if (IsMemberSelected)
        {
            GameScript.ProgressBar.Value = GetStaminaPercent;

            if (Input.GetMouseButton(0) && !lineCompleted)
            {
                /* Casting a ray from the camera to the mouse position
                 * Because camera is orthographic depth doesnt matter
                 */
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)) return;
                Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, drawLayer))
                {
                    Vector3 hitpos = hit.point;
                    Vector2 hitpos2D = hitpos;

                    Collider2D c2d = SelectedMember.GetComponent<Collider2D>();
                    if (c2d.OverlapPoint(hitpos2D)) return;

                    // Check distance so lines aren't drawn when user holds mouse still
                    if (Vector2.Distance(hitpos, CurrentMemberLine.LastPosition) <= LineRoundness) return;
                    if (!HasEnoughStamina)
                    {
                        CompleteLine();
                        return;
                    }

                    // Checks if the line is too big, to prevent drawing to quick or out of screen
                    float distance = Vector3.Distance(CurrentMemberLine.LastPosition, hitpos);
                    if (distance > LineRoundness * 33)
                    {
                        Debug.LogWarning("Drawn out of screen!");
                        CompleteLine();
                        return;
                    }

                    CreateLine(CurrentMemberLine.LastPosition, hitpos);
                }
                //if raycast hits doesnt hit complete the line
                else
                {
                    CompleteLine();
                }
            }
            else
            {
                CompleteLine();
            }
        }
    }

    /// <summary>
    /// Destroys any linerenderer objects currently in memory
    /// </summary>
    public void DestroyLines()
    {
        // Destroys all Linerenderer objects
        MemberLines.ForEach(DestroyLines);
        MemberLines.Clear();
    }

    /// <summary>
    /// Clears the lines from this member, and removing the instance from the register
    /// </summary>
    /// <param name="m"></param>
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
    /// </summary>
    /// <param name="ml"></param>
    private void DestroyLines(MemberLine ml)
    {
        ml.LineRenderers.ForEach(o => { Destroy(o.gameObject); });
    }

    /// <summary>
    ///     Completes the current line
    /// </summary>
    public void CompleteLine()
    {
        SetSelectedMemberAction();
        //SelectedMember = null;
        noLongerOnCharacter = false;
        lineCompleted = true;
    }

    /// <summary>
    ///     Creates a line using the Unity linerenderer
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
        line.material = whiteLine;
        line.useWorldSpace = true;
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
    public void SetSelectedMemberAction()
    {
        if (CurrentMemberLine == null) return;
        List<Vector2> vector2s = new List<Vector2>(CurrentMemberLine.Positions.Count);
        for (int i = 0; i < CurrentMemberLine.Positions.Count; i++)
        {
            vector2s.Add(CurrentMemberLine.Positions[i]);
        }
        GameObject gm = SelectedMember.gameObject;

        WalkAction wa = gm.GetComponent<WalkAction>();
        if (wa != null) Destroy(wa);

        WalkAction walkAction = gm.AddComponent<WalkAction>();
        walkAction.Positions = vector2s;
    }

    /// <summary>
    ///     Sets the current member for the line
    /// </summary>
    /// <param name="member">Member to select</param>
    public void SetMember(Member member)
    {
        Debug.Log((member == null ? "des" : "S") + "elected member");
        SelectedMember = member;
        lineCompleted = false;
        MemberLine ml = MemberLines.Find(o => o.Member == SelectedMember);
        if (ml != null)
        {
            ml.LineRenderers.ForEach(o => { Destroy(o.gameObject); });
            ml.Reset(member.transform.position);
        }
        else
        {
            if (SelectedMember != null)
            {
                MemberLines.Add(new MemberLine(member).Reset(member.transform.position));
            }
        }
        GameScript.ProgressBar.gameObject.SetActive(IsMemberSelected);
        GameScript.kickButton.gameObject.SetActive(IsMemberSelected);
        GameScript.besteGameButton.gameObject.SetActive(IsMemberSelected);
    }

    public void ActionPressed(Type action)
    {
        if (SelectedMember == null)
        {
            Debug.LogError("No member selected");
            return;
        }
        SelectedMember.ActionPressed(action);
    }

    public void KickPressed()
    {
        ActionPressed(typeof(KickAction));
    }

    public void TiedtogetherPressed()
    {
        ActionPressed(typeof(TiedTogetherAction));
    }


    /// <summary>
    ///     Calculates the stamina needed for the current line
    /// </summary>
    /// <returns>Stamina need for the line, rounds up!</returns>
    public int CalculateLineDistance()
    {
        float distance = 0;
        if (CurrentMemberLine == null) return 0;
        List<Vector3> positions = CurrentMemberLine.Positions;
        // Calculate total distance for the line
        for (int i = 1; i < positions.Count; i++)
        {
            Vector2 v1 = positions[i - 1];
            Vector2 v2 = positions[i];
            distance += Vector2.Distance(v1, v2);
        }
        distance *= StaminaModifier;
        // Round up stamina needed
        return (int)Math.Ceiling(distance);
    }
}