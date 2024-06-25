using EnglishCourses.BusinessLogic.Interface;
using EnglishCourses.BusinessLogic.MainBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCourses.BusinessLogic
{
    public class BussinesLogical
    {

        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IAdministration GetAdministrationBL()
        {
            return new AdministrationBL();
        }

        public IService GetServiceBL()
        {
            return new ServiceBL();
        }
    }
}