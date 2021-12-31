using Agate.MVC.Base;
using Jaddwal.GameplaySequence.Message;
using Jaddwal.GameplaySequence.RoundManager;
using Jaddwal.SchedulerPiece.Selector;
using Jaddwal.SchedulerPiece.TeamManager.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.GameplaySequence.Selector.Connector
{
    public class SelectionConnector : BaseConnector
    {
        private SchedulerSelectionController _selection;
        private RoundManagementController _roundManager;

        protected override void Connect()
        {
            Subscribe<SchedulerTeamChangeMessage>(OnTeamChange);
            Subscribe<GameplayDoneActionMessage>(OnTurnEnd);
        }

        protected override void Disconnect()
        {
            Unsubscribe<SchedulerTeamChangeMessage>(OnTeamChange);
            Unsubscribe<GameplayDoneActionMessage>(OnTurnEnd);

        }

        private void OnTeamChange(SchedulerTeamChangeMessage message)
        {
            _selection.ChangeHighlightColor(message.CurrentTeamIndex);
        }

        private void OnTurnEnd(GameplayDoneActionMessage message)
        {
            _roundManager.EndTurn(message.Turn);
        }
    }

}
