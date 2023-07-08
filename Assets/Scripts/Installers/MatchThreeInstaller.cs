using UnityEngine;
using Zenject;

public class MatchThreeInstaller : MonoInstaller
{
    [Header("Managers Prefabs")]
    [SerializeField] private GameObject inputHandlerPrefab;
    [SerializeField] private GameObject uIManagerPrefab;
    [SerializeField] private GameObject levelManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject backgroundManagerPrefab;

    [Header("Match Three Grid Prefabs")]
    [SerializeField] private GameObject matchThreeGridPrefab;
    [SerializeField] private GameObject gridItemsManagerPrefab;
    [SerializeField] private GameObject gridTileSwapperPrefab;
    [SerializeField] private GameObject gridMatchCalculatorPrefab;

    public override void InstallBindings()
    {
        InstallManagers();
        InstallMatchThreeGrid();
    }

    private void InstallManagers()
    {
        Container.Bind<IInputHandler>().To<InputHandler>().FromComponentInNewPrefab(inputHandlerPrefab).AsSingle().NonLazy();
        Container.Bind<AudioManager>().FromComponentInNewPrefab(audioManagerPrefab).AsSingle().NonLazy();
        Container.Bind<UiManager>().FromComponentInNewPrefab(uIManagerPrefab).AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromComponentInNewPrefab(levelManagerPrefab).AsSingle().NonLazy();
        Container.Bind<ScoreManager>().FromComponentInNewPrefab(scoreManagerPrefab).AsSingle().NonLazy();
        Container.Bind<BackgroundManager>().FromComponentInNewPrefab(backgroundManagerPrefab).AsSingle().NonLazy();
    }

    private void InstallMatchThreeGrid()
    {
        Container.Bind<MatchThreeGrid>().FromComponentInNewPrefab(matchThreeGridPrefab).AsSingle().NonLazy();
        Container.Bind<TileFactory>().AsSingle().NonLazy();
        Container.Bind<GridItemsManager>().FromComponentInNewPrefab(gridItemsManagerPrefab).AsSingle().NonLazy();
        Container.Bind<GridTileSwapper>().FromComponentInNewPrefab(gridTileSwapperPrefab).AsSingle().NonLazy();
        Container.Bind<GridMatchCalculator>().FromComponentInNewPrefab(gridMatchCalculatorPrefab).AsSingle().NonLazy();
    }
}
