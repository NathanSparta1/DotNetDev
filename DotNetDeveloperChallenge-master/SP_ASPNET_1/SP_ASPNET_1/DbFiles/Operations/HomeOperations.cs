using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.ViewModels;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class HomeOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public HomeProductViewModel GetHomeProductViewModel()
        {
            return new HomeProductViewModel(){ProductLines = this._unitOfWork.ProductLineSchoolRepository.Get(null, null, "ProductItems") };
        }
    }
}