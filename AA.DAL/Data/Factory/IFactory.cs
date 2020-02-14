using System;
using System.Collections.Generic;
using System.Text;

namespace AA.DAL.Data.Factory
{
    public interface IFactory
    {
        AA_BBDDContext GetMainContext();
        AA_BBDDContext GetNewContext();
        string GetConnString();
    }
}
