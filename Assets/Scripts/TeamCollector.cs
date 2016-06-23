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
        }

        else if (i == 2)
        {
            team2 = team;
        }

        else
        {
            Debug.Log("The team: " + i + " was not found, could not set teams");
        }
    }
    
}
