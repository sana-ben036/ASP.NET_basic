using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageStudent1.Models.Repositories
{
    public class SqlStudentRepository : ICompanyRepository<Student>
    {
        private readonly AppDbContext context;

        public SqlStudentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Student entity)
        {
            this.context.Students.Add(entity);
            context.SaveChanges();
            //return entity if add return student
        }

        public Student Delete(string cin)
        {
            var student = Get(cin);
            if(student != null)
            {
                this.context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        public Student Get(string cin)
        {
            var student = this.context.Students.SingleOrDefault(s => s.CIN == cin);
            return student;
        }

        public IEnumerable<Student> GetList()
        {
            return this.context.Students;
        }

        public Student Update(Student entityChanges)
        {
            var student = this.context.Students.Attach(entityChanges);
            student.State = EntityState.Modified;
            context.SaveChanges();
            return entityChanges;

        }
    }
}
