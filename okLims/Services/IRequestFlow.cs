using okLims.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Services
{
    public interface IRequestFlow
    {

        Task SendEmailOnCreation();
        Task SendEmailOnCompletion();
    }
}
