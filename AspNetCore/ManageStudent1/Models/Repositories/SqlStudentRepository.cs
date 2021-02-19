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

        public SqlStudentRepository(AppDbContext _context)
        {
            context = _context;
        }
        public void Add(Student entity)
        {
           
            context.Students.Add(entity);
            context.SaveChanges();
            //return entity if  we want return student after add
        }

        public Student Delete(string cin)
        {
            var student = Get(cin);
            if(student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }
        
        public Student Get(string cin)
        {
            var student = context.Students.SingleOrDefault(s => s.CIN == cin);
            return student;
        }

        public IEnumerable<Student> GetList()
        {
            return context.Students;
        }

        public Student Edit(Student entityChanges)
        {
            var student = context.Students.Attach(entityChanges);
            student.State = EntityState.Modified;
            context.SaveChanges();
            return entityChanges;

        }
    }
}
