using Agate.MVC.Base;
using System.Collections;

namespace Jaddwal.Core.MVC
{
    public abstract class GameObjectController<TController, TModel, TInterfaceModel, TView> : ObjectController<TController, TModel, TInterfaceModel, TView>
        where TController : GameObjectController<TController, TModel, TInterfaceModel, TView>
        where TModel : BaseModel, TInterfaceModel, new()
        where TInterfaceModel : IBaseModel
        where TView : ObjectView<TInterfaceModel>
    {
        public abstract IEnumerator InitObject(TView view);
    }
}
