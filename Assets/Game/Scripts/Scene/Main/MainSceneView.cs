using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Cell.System;
using Jaddwal.PersonPiece.System;
using Jaddwal.SchedulerPiece.TeamManager;
using Jaddwal.SchedulerPiece.Instantiator;
using Jaddwal.GameplaySequence.Selector;
using Jaddwal.SchedulerPiece.Selector;
using Jaddwal.PersonPiece.Selector;
using Jaddwal.GameplaySequence.RoundManager;
using Jaddwal.SchedulerPiece.RoundManager;
using Jaddwal.SchedulerPiece.ScoreCollector;
using Jaddwal.SchedulerPiece.BoardFilter;
using Jaddwal.PersonPiece.ScorePopup;
using Jaddwal.PersonPiece.AI;

namespace Jaddwal.Scene.Main
{
    public class MainSceneView : BaseSceneView
    {
        public BoardView Board;
        public CellInstantiatorView CellSystem;

        public PersonPieceAIView PersonPieceAI;
        public PersonPieceSystemView PersonPieceSystem;
        public PersonPieceSelectionView PersonSelector;
        public PersonPieceScorePopupInstantiateView PersonScorePopupInstatiator;

        public SelectionView Selector;

        public SchedulerInstantiatorView SchedulerInstantiator;
        public SchedulerTeamView SchedulerTeam;
        public SchedulerSelectionView SchedulerSelector;
        public SchedulerRoundManagementView SchedulerRoundManager;
        public SchedulerPieceScoreCollectorView SchedulerScoring;
        public SchedulerBoardFilterView SchedulerFilter;

        public RoundManagementView RoundManager;
    }

}
