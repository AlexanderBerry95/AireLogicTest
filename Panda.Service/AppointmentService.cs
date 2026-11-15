using Panda.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Service
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IPandaContext patientContext;

        public AppointmentService(IPandaContext patientContext)
        {
            this.patientContext = patientContext;
        }

    }
}
