using Agate.MVC.Base;
using Agate.MVC.Core;
using Jaddwal.Core;
using Jaddwal.Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaddwal.Cell.System;
using Jaddwal.PersonPiece.System;
using Jaddwal.SchedulerPiece.Instantiator;
using Jaddwal.SchedulerPiece.TeamManager;
using Jaddwal.SchedulerPiece.Container;
using Jaddwal.GameplaySequence.Selector;
using Jaddwal.GameplaySequence.Selector.Connector;
using Jaddwal.SchedulerPiece.Selector;
using Jaddwal.PersonPiece.Selector;
using Jaddwal.GameplaySequence.RoundManager;
using Jaddwal.SchedulerPiece.RoundManager;
using Jaddwal.SchedulerPiece.ScoreCollector;
using Jaddwal.SchedulerPiece.BoardFilter;
using Jaddwal.PersonPiece.AI;
using Jaddwal.PersonPiece.ScorePopup;

namespace Jaddwal.Scene.Main
{
    public class MainSceneController : BaseLauncher<MainSceneController, MainSceneView>
    {

        [SerializeField] private MainSceneView _mainView;

        private BoardController _board;
        private CellInstantiatorController _cellInstantiator;

        private PersonPieceAIController _personPieceAI;
        private PersonPieceSystemController _personPieceSystem;
        private PersonPieceSelectionController _personSelector;
        private PersonPieceScorePopupInstantiateController _personPieceScorePopupInstantiator;

        private SelectionController _selector;
        private SchedulerInstantiatorController _schedulerInstantiator;
        private SchedulerTeamController _schedulerTeam;
        private SchedulerSelectionController _schedulerSelector;
        private SchedulerRoundManagementController _schedulerRoundManager;
        private SchedulerPieceScoreCollectorController _schedulerScoring;
        private SchedulerBoardFilterController _schedulerBoardFilter;

        private RoundManagementController _roundManager;

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[]
            {
                new SelectionConnector()
            };
        }

        protected override IController[] GetSceneDependencies()
        {
            return new IController[]
            {
                new BoardController(),
                new CellInstantiatorController(),

                new PersonPieceSystemController(),
                new PersonPieceSelectionController(),
                new PersonPieceAIController(),
                new PersonPieceScorePopupInstantiateController(),

                new SchedulerInstantiatorController(),
                new SchedulerTeamController(),
                new SchedulerContainerController(),
                new SchedulerSelectionController(),
                new SchedulerRoundManagementController(),
                new SchedulerPieceScoreCollectorController(),
                new SchedulerBoardFilterController(),

                new RoundManagementController(),
            };
        }

        protected override IEnumerator InitSceneObject()
        {
            yield return _board.OnInitSceneObject(_view.Board);
            yield return _cellInstantiator.OnInitSceneObject(_view.CellSystem);

            yield return _personPieceAI.OnInitSceneObject(_view.PersonPieceAI);
            yield return _personPieceSystem.OnInitSceneObject(_view.PersonPieceSystem);
            yield return _personSelector.OnInitSceneObject(_view.PersonSelector);
            yield return _personPieceScorePopupInstantiator.OnInitSceneObject(_view.PersonScorePopupInstatiator);

            yield return _schedulerInstantiator.OnInitSceneObject(_view.SchedulerInstantiator);
            //yield return _selector.OnInitSceneObject(_view.Selector);
            yield return _schedulerTeam.OnInitSceneObject(_view.SchedulerTeam);
            yield return _schedulerSelector.OnInitSceneObject(_view.SchedulerSelector);
            yield return _roundManager.OnInitSceneObject(_view.RoundManager);
            yield return _schedulerRoundManager.OnInitSceneObject(_view.SchedulerRoundManager);
            yield return _schedulerScoring.OnInitSceneObject(_view.SchedulerScoring);
            yield return _schedulerBoardFilter.OnInitSceneObject(_view.SchedulerFilter);
        }

        protected override IEnumerator LaunchScene()
        {
            yield return _board.OnLaunchScene();
            _board.SetIsActivateEvent(true);
            yield return _personPieceSystem.OnLaunchScene();
            //yield return _schedulerInstantiator.OnLaunchScene();
            _schedulerInstantiator.IsActive = true;
            yield return _schedulerTeam.OnLaunchScene();
            yield return _schedulerSelector.OnLaunchScene();
            yield return _schedulerScoring.OnLaunchScene();
            yield return _personSelector.OnLaunchScene();
            yield return _schedulerRoundManager.OnLaunchScene();

            yield return _schedulerBoardFilter.OnLaunchScene();

            yield return _roundManager.OnLaunchScene();
        }

        protected override ILoad GetLoader() => SceneLoader.Instance;
        protected override IMain GetMain() => SceneLauncher.Instance;
        protected override string GetSceneName() => "Main";
        protected override MainSceneView GetSceneView() => _mainView;
    }
}
