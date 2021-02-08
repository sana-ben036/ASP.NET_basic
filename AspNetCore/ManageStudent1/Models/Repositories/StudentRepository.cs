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

        public List<Student> Get()
        {
            StudentList = new List<Student>();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = _Configuration.GetConnectionString("CnxSqlServer");
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT CIN,IsActive,Prenom,Nom,Adresse,Titre as Filiere FROM Student INNER JOIN Filiere ON student.Filiere_Id = Filiere.Id;";
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
                            Filiere = rd["Filiere"].ToString()

                        }) ;
                    }
                }
                con.Close();
            }

            return StudentList;
        }

        public void Add(Student Model) {

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = _Configuration.GetConnectionString("CnxSqlServer");
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO Student (CIN,Prenom,Nom,Adresse,Filiere_Id,IsActive) VALUES (@CIN,@Prenom,@Nom,@Adresse,@Filiere,@IsActive)";
                    cmd.Parameters.AddWithValue("@CIN", Model.CIN);
                    cmd.Parameters.AddWithValue("@Prenom", Model.Prenom);
                    cmd.Parameters.AddWithValue("@Nom", Model.Nom);
                    cmd.Parameters.AddWithValue("@Adresse", Model.Adresse);
                    cmd.Parameters.AddWithValue("@Filiere", Model.Filiere);
                    cmd.Parameters.AddWithValue("@IsActive", Model.IsActive);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }


        }

        
    }
}
