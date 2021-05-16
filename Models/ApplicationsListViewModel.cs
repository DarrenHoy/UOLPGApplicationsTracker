using PGProgrammeApplications.DataContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGProgrammeApplications.Models
{
    public class ApplicationsListViewModel : IEnumerable<EditApplicationViewModel>
    {
        private IEnumerable<EditApplicationViewModel> _applications;

        public Student Student { get; private set; }

        public ApplicationsListViewModel(Student student, IEnumerable<EditApplicationViewModel> applications)
        {
            _applications = applications;
            Student = student;
        }

        public IEnumerator<EditApplicationViewModel> GetEnumerator()
        {
            return _applications.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _applications.GetEnumerator();
        }
    }
}