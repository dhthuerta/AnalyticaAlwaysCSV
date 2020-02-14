using AA.BL.Services.AA_Service;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticaAlwaysCSV
{
    public class AA_Manager: IHostedService
    {
        private readonly IAA_Service aaService;
        public AA_Manager(IAA_Service _aaService)
        {
            aaService = _aaService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<bool>(aaService.AA_Process());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
