using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RequestQueueSystem>().AsSingle();
        Container.Bind<WeatherModel>().AsSingle();
        Container.Bind<DogBreedsModel>().AsSingle();
        Container.Bind<WeatherView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DogBreedsView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<NavigationView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
    }
}