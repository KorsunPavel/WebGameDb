using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using WebGame.Data.DAL;
using WebGame.Infostracture;

namespace WebUI.Infostracture {
    //public class NinjectDependencyResolver : IDependencyResolver {
    //    private IKernel kernel;

    //    public NinjectDependencyResolver(IKernel kernelParam) { kernel = kernelParam; AddBindings(); }

    //    public object GetService(Type serviceType) { return kernel.TryGet(serviceType); }

    //    public IEnumerable<object> GetServices(Type serviceType) { return kernel.GetAll(serviceType); }

    //    private void AddBindings() {
    //        //kernel.Bind<IRepository>().To<Repository>().WithConstructorArgument("settings", emailSettings);

    //    }
    //}
    
    // This class is the resolver, but it is also the global scope
        // so we derive from NinjectScope.
        public class NinjectDependencyResolver : NinjectDependencyScope, System.Web.Http.Dependencies.IDependencyResolver
    {
            IKernel kernel;

            public NinjectDependencyResolver(IKernel kernel) : base(kernel)
            {
                this.kernel = kernel;
            }

            public IDependencyScope BeginScope()
            {
                return new NinjectDependencyScope(kernel.BeginBlock());
            }
        }
}