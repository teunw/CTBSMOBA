using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public static class TeamCollector {

    /// <summary>
    /// The first team.
    /// </summary>
    public static List<Member> team1;

    /// <summary>
    /// The second team.
    /// </summary>
    public static List<Member> team2;

    public static void SetTeam(List<Member> team, int i)
    {
        if (i == 1)
        {
            team1 = team;
            Debug.Log("Set team 1, it has " + team.Count + " players");
        }

        else if (i == 2)
        {
            team2 = team;
            Debug.Log("Set team 2, it has " + team.Count + " players");
        }

        else
        {
            Debug.Log("The team: " + i + " was not found, could not set teams");
        }
    }
    
}
