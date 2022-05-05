using Numax.DataAccess.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numax.DataAccess.DataBaseModels
{
    //public partial class ContainerEntities
    //{
    //    public static ContainerEntities GetContext()
    //    {
    //        return BaseContext.GetContext<ContainerEntities>();
    //    }

    //    public static void SaveContext()
    //    {
    //        try
    //        {
    //            BaseContext.SaveChanges<ContainerEntities>();

    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Erro");
    //        }
    //    }

    //    public static T SaveContext<T>(Func<T> BeforeSave) where T : class
    //    {
    //        if (GetContext().GetValidationErrors().Count() == 0)
    //        {
    //            var result = BeforeSave();
    //            SaveContext();
    //            return result;
    //        }
    //        else
    //        {
    //            SaveContext();
    //            return null;
    //        }
    //    }
    //}
}
