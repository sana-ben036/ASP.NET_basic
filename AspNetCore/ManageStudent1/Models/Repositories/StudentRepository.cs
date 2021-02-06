using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ManageStudent1.Models.Repositories
{
    public class StudentRepository : ICompanyRepository<Student>
    {
        public List<Student> StudentList;
        public IConfiguration _Configuration { get; }
        public StudentRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
            //StudentList = new List<Student>();
            //StudentList.Add(new Student() {CIN=1,IsActive=true,Prenom="sana",Nom="ben",Adresse="Youssoufia",Filiere_Id=1});
            //StudentList.Add(new Student() { CIN =2, IsActive = true, Prenom = "swino", Nom = "beng", Adresse = "Safi", Filiere_Id = 2 });
           
        }
        public Student GetStudent(string cin)
        {
            return StudentList.Find(x => x.CIN == cin);
        }

        public List<Student> GetAll()
        {
            StudentList = new List<Student>();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = _Configuration.GetConnectionString("CnxSqlServer");
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from Student";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        StudentList.Add(new Student
                        {
                            CIN = rd["CIN"].ToString(),
                            IsActive = Convert.ToBoolean(rd["IsActive"]),
                            Prenom = rd["Prenom"].ToString(),
                            Nom = rd["Nom"].ToString(),
                            Adresse = rd["Adresse"].ToString(),
                            Filiere_Id = Convert.ToInt32(rd["Filiére"])

                        });
                    }
                }
                con.Close();
            }

            return StudentList;
        }
    }
}
