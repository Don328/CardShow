using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite
{
    public static class ParamBuilder
    {
        public static void Build(
            DbCommand cmd,
            string name,
            object value)
        {
            var p = cmd.CreateParameter();
            if (value == null)
            {
                throw new ArgumentNullException("Parameter value cannot be null");
            }

            var type = value.GetType();
            if (type == typeof(int))
                p.DbType = DbType.Int32;
            else if (type == typeof(string))
                p.DbType = DbType.String;
            else if (type == typeof(DateTime))
                p.DbType = DbType.DateTime;
            else
                throw new ArgumentException(
                    $"Unrecognized Type: {type}");

            p.Direction = ParameterDirection.Input;
            p.ParameterName = name;
            p.Value = value;
            cmd.Parameters.Add(p);
        }
    }
}
