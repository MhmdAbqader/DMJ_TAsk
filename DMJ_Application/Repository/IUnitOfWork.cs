﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMJ_Application.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository  { get; }
        public IDepartmentRepository DepartmentRepository { get; }
       





        void SaveChanges();
    }

}
