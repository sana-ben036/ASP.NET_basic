using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ManageStudent1.Models.Repositories
{
    public class FiliereRepository : ICompanyRepository<Filiere>
    {
        public List<Filiere> FiliereList;
        public IConfiguration _Configuration { get; }
        

        public List<Filiere> Get()
        {

            FiliereList = new List<Filiere>();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = _Configuration.GetConnectionString("CnxSqlServer");
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT *  FROM Filiere ";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        FiliereList.Add(new Filiere
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Titre = rd["Titre"].ToString()
                            

                        });
                    }
                }
                con.Close();
            }



            return FiliereList;
        }

        public void Add(Filiere entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
