using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace HL7_DB_EXPORT
{
    class DBUtil
    {
        public class DBResult : List< List<string> >
        {

        }

        public class MSSqlDBConnection
        {
            private MSSqlDBConnection()
            {
            }

            private SqlConnection connection = null;

            public DBResult query(string sql)
            {
                var result = new DBResult();
                var cmd = new SqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if(reader.IsDBNull(i))
                        {
                            row.Add("");
                        } else if(reader.GetDataTypeName(i)=="bit")
                        {
                            row.Add(reader.GetBoolean(i).ToString());
                        } else if (reader.GetDataTypeName(i) == "int")
                        {
                            row.Add(reader.GetInt32(i).ToString());
                        } else if (reader.GetDataTypeName(i) == "datetime")
                        {
                            row.Add(reader.GetDateTime(i).ToString());
                        }
                        else if (reader.GetDataTypeName(i) == "char" || reader.GetDataTypeName(i) == "varchar")
                        {
                            row.Add(reader.GetString(i));
                        }
                    }
                        
                    result.Add(row);
                }
                return result;
            }

            private static MSSqlDBConnection _instance = null;
            public static MSSqlDBConnection Instance()
            {
                if (_instance == null)
                    _instance = new MSSqlDBConnection();
                return _instance;
            }

            public bool connect(string host, int port, string dbName, string user, string password)
            {
                bool result = true;
                if (connection == null)
                {
                    string connstring = "";
                    if (host != "")
                        connstring = string.Format("Data Source={0},{1};", host, port);
                    if (dbName != "")
                    {
                        connstring = string.Format("{0}database={1};", connstring, dbName);
                    }
                    if (user != "")
                    {
                        connstring = string.Format("{0}UID={1};", connstring, user);
                    }
                    if (user != "")
                    {
                        connstring = string.Format("{0}password={1};", connstring, password);
                    }
                    connstring += "Integrated Security=True;";
                    connection = new SqlConnection(connstring);
                    connection.Open();
                    result = true;
                }

                return result;
            }

            public void Close()
            {
                connection.Close();
                connection = null;
            }

        }

        public class MysqlDBConnection
        {
            private MysqlDBConnection()
            {
            }

            private MySqlConnection connection = null;

            public DBResult query(string sql)
            {
                var result = new DBResult();
                var cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.IsDBNull(i))
                        {
                            row.Add("");
                        }
                        else if (reader.GetDataTypeName(i) == "bit")
                        {
                            row.Add(reader.GetBoolean(i).ToString());
                        }
                        else if (reader.GetDataTypeName(i) == "int")
                        {
                            row.Add(reader.GetInt32(i).ToString());
                        }
                        else if (reader.GetDataTypeName(i) == "datetime")
                        {
                            row.Add(reader.GetDateTime(i).ToString());
                        }
                        else if (reader.GetDataTypeName(i) == "char" || reader.GetDataTypeName(i) == "varchar")
                        {
                            row.Add(reader.GetString(i));
                        }
                    }
                    result.Add(row);
                }
                return result;
            }

            private static MysqlDBConnection _instance = null;
            public static MysqlDBConnection Instance()
            {
                if (_instance == null)
                    _instance = new MysqlDBConnection();
                return _instance;
            }

            public bool connect(string host, int port, string dbName, string user, string password)
            {
                bool result = true;
                if (connection == null)
                {
                    string connstring = "";
                    if (password == "")
                        connstring = string.Format("Server={0};database={1};UID={2};port={3}", host, dbName, user, port);
                    else
                        connstring = string.Format("Server={0};database={1};UID={2};password={3};port={4}", host, dbName, user, password, port);
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                    result = true;
                }

                return result;
            }

            public bool connectMSServer(string host, int port, string dbName, string user, string password)
            {
                bool result = true;
                if (connection == null)
                {
                    string connstring = "";
                    if (password == "")
                        connstring = string.Format("Server={0};database={1};UID={2};port={3}", host, dbName, user, port);
                    else
                        connstring = string.Format("Server={0};database={1};UID={2};password={3};port={4}", host, dbName, user, password, port);
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                    result = true;
                }

                return result;
            }

            public void Close()
            {
                connection.Close();
            }
        }
    }
}
