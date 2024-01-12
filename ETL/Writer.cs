using ETL.ContextDB;
using ETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    internal static class Writer
    {
        public static void Save(List<Institute> input) 
        {
            using (var dbContext = new InstituteContext())
            {
                foreach (var institute in input)
                {
                    dbContext.Institute.Add(institute);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
