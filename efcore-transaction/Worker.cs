using efcore_transaction.Entities;
using efcore_transaction.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace efcore_transaction
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerContext _context;

        public Worker(ILogger<Worker> logger, WorkerContext context)
        {
            _logger = logger;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var trans = _context.Database.BeginTransaction();

                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    Customer customer = _context.Customers.Where(v => v.Id == 925).FirstOrDefault();

                    var user = new User { Name = "AHM", CustomerId = customer.Id };
                    _context.SaveChanges();

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
