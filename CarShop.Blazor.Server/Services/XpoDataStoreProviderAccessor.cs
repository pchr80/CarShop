using System;
using DevExpress.ExpressApp.Xpo;

namespace CarShop.Blazor.Server.Services {
    public class XpoDataStoreProviderAccessor {
        public IXpoDataStoreProvider DataStoreProvider { get; set; }
    }
}
