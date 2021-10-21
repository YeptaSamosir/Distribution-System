using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Repository.Interface
{
    interface IDashboardRepository
    {
        Task<DashboardVM> GetDataCount();
    }
}
