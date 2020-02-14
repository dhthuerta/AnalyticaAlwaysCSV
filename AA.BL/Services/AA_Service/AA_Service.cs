using AA.CrossDomain;
using AA.DAL.Data.Repositories.InventoryRepository;
using AA.ExternalServices.AzureFileDownload;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AA.BL.Mappers;
using AA.DAL.Data.UnitofWork;
using AA.DAL.Data.Models;
using System.Data;

namespace AA.BL.Services.AA_Service
{
    public class AA_Service: IAA_Service
    {
        private readonly IInventoryRepo inventoryRepo;
        private readonly IAzureFileDownload azDownload;
        public readonly IUnitOfWork uow;

        public AA_Service(IInventoryRepo _inventoryRepo, IAzureFileDownload _azDownload, IUnitOfWork _uow)
        {
            inventoryRepo = _inventoryRepo;
            azDownload = _azDownload;
            uow = _uow;
        }

        public bool AA_Process()
        {
            Console.WriteLine("Inicio Proceso: " + DateTime.Now);

            Console.WriteLine("Inicio descarga fichero: " + DateTime.Now);
            var dto = azDownload.GetFile();
            Console.WriteLine("Fin descarga fichero: " + DateTime.Now);

            Console.WriteLine("Inicio conversión a datatable: " + DateTime.Now);
            var dataTable = Helper.ToDataTable(dto.FileString);           
            Console.WriteLine("fin conversión a datatable: " + DateTime.Now);
           
            if (inventoryRepo.GetCount() > 0)
            {
                Console.WriteLine("Inicio borrado de carga anterior: " + DateTime.Now);
                uow.MassiveDelete(Constants.MAIN_TABLE);
                Console.WriteLine("Fin borrado de carga anterior: " + DateTime.Now);
            }             

            Console.WriteLine("Inicio guardado masivo: " + DateTime.Now);
            uow.MassiveBulkSave(dataTable);
            Console.WriteLine("Fin guardado masivo: " + DateTime.Now);

            return true;
        }

    }
}
