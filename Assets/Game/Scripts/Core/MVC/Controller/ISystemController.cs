using System.Collections;

namespace Jaddwal.Core.MVC
{
    public interface ISystemController<TView> : IInitializableOnScene<TView>, ILaunchableOnScene
    {
        
    }

    public interface IInitializableOnScene<TView>
    {
        IEnumerator OnInitSceneObject(TView view);
    }

    public interface ILaunchableOnScene
    {
        IEnumerator OnLaunchScene();
    }
}
