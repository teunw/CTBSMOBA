using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public static class TeamCollector {

    /// <summary>
    /// The first team.
    /// </summary>
    private static List<MemberData> team1 = new List<MemberData>();

    /// <summary>
    /// The second team.
    /// </summary>
    private static List<MemberData> team2 = new List<MemberData>();

    public static bool hasTeams
    {
        get
        {
            if (team1 == null || team2 == null)
            {
                return false;
            }
            return (team1.Count == 3 && team2.Count == 3);
        }
    }

    /// <summary>
    /// Fills the team data
    /// </summary>
    /// <param name="team"></param>
    /// <param name="i"></param>
    public static void FillTeam(List<MemberData> team, int i)
    {
        if (i == 1)
        {
            team1 = team;
        }
        else if (i == 2)
        {
            team2 = team;
        }
    }

    /// <summary>
    /// Sets the given team with the inserte values 
    /// </summary>
    /// <param name="team"></param>
    /// <param name="i"></param>
    public static void SetTeam(List<Member> team, int i)
    {
        if (i == 1)
        {
            int j = 0;
            foreach (Member m in team)
            {
                m.SetFieldsFromFile(team1[j++]);
            }
            Debug.Log("Set team 1, it has " + team.Count + " players");
        }

        else if (i == 2)
        {
            int j = 0;
            foreach (Member m in team)
            {
                m.SetFieldsFromFile(team2[j++]);
            }
            Debug.Log("Set team 2, it has " + team.Count + " players");
        }
        else
        {
            Debug.Log("The team: " + i + " was not found, could not set teams");
        }
    }
    
}
