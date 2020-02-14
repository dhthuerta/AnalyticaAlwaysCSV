using AA.DAL.Data.Factory;
using AA.DAL.Data.Models;
using AA.DAL.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.DAL.Data.Repositories.InventoryRepository
{
    public class InventoryRepo: RepositoryBase<AA_Inventory>, IInventoryRepo
    {
        public InventoryRepo(IFactory dataFactory)
                    : base(dataFactory)
        {
        }
    }
}
