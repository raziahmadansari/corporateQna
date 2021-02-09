using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public static class DbService
    {
        public static Database Db;

        static DbService()
        {
            Db = new Database("server=WOLVERINE;database=CorporateQna;Trusted_Connection=True;MultipleActiveResultSets=true", "System.Data.SqlClient");
        }
    }
}
