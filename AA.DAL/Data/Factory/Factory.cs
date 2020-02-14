using AA.CrossDomain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.DAL.Data.Factory
{
    public class Factory: IFactory, IDisposable
    {
        private AA_BBDDContext _mainContext;
        private readonly IConfiguration config;
        public Factory(IConfiguration _config)
        {
            config = _config;
        }

        public AA_BBDDContext GetMainContext()
        {
            return _mainContext ?? (_mainContext = new AA_BBDDContext(config[Constants.CONN_STRING_KEY]));
        }

        public AA_BBDDContext GetNewContext()
        {
            return new AA_BBDDContext(config[Constants.CONN_STRING_KEY]);
        }

        public string GetConnString()
        {
            return config[Constants.CONN_STRING_KEY];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this._mainContext == null)
            {
                return;
            }

            this._mainContext.Dispose();
            this._mainContext = null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
