using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Models.Repositories
{
    public interface ICompanyRepository<TEntity>
    {


        List<TEntity>  Get();
        void Add(TEntity entity);


    }
}
