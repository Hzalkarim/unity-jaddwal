using Jaddwal.GameplaySequence.RoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.GameplaySequence.Message
{
    public struct GameplayDoneActionMessage
    {
        public Turn Turn;

        public GameplayDoneActionMessage(Turn turn)
        {
            Turn = turn;
        }
    }

}
