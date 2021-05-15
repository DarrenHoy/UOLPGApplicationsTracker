using PGProgrammeApplications.DataContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGProgrammeApplications.Models
{
    public class ApplicationsListViewModel : IEnumerable<ApplicationViewModel>
    {
        private IEnumerable<ApplicationViewModel> _applications;

        public Student Student { get; private set; }

        public ApplicationsListViewModel(Student student, IEnumerable<ApplicationViewModel> applications)
        {
            _applications = applications;
            Student = student;
        }

        public IEnumerator<ApplicationViewModel> GetEnumerator()
        {
            return _applications.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _applications.GetEnumerator();
        }
    }
}