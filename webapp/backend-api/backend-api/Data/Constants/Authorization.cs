using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Data.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Teamlead,
            Member,
        }

        //comment or remove for production
        public const string default_username = "admin@achievements.com";
        public const string default_firstName = "Admin";
        public const string default_lastName = "Admin";
        public const string default_email = "admin@achievements.com";
        public const string default_password = "Test!234";
        public const Roles default_registration_role = Roles.Administrator;
        public const Roles default_organization_role = Roles.Administrator;
    }
}
