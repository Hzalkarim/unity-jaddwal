using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.TeamManager.Message
{
    public struct SchedulerTeamChangeMessage
    {
        public int CurrentTeamIndex;

        public SchedulerTeamChangeMessage(int current)
        {
            CurrentTeamIndex = current;
        }
    }
}
