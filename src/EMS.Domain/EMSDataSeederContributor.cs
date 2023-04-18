using EMS.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace EMS
{
    public class EMSDataSeederContributor
    : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Expense, Guid> _expenseRepository;

        public EMSDataSeederContributor(IRepository<Expense, Guid> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _expenseRepository.GetCountAsync() <= 0)
            {
                await _expenseRepository.InsertAsync(
                    new Expense
                    {
                        expense_title = "ABC",
                        expense_description = "XYZ",
                        split_as = "PQR",
                        currency = "Rupees",
                        expense_amount = 2000,

                    },
                    autoSave: true
                ); ;

                await _expenseRepository.InsertAsync(
                    new Expense
                    {
                        expense_title = "ABCQPR",
                        expense_description = "XYZPQR",
                        split_as = "PQRPQR",
                        currency = "RupeesPQR",
                        expense_amount = 2000,

                    },
                    autoSave: true
                );
            }
        }
    }
}
