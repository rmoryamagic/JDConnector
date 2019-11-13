using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ODBC
{
    public class QueryExecutor
    {
        public static readonly DataTable EmptyDataTable = GetEmptyDataTable();

        private static DataTable GetEmptyDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("There are no results...");
            return table;
        }

        private readonly SqlConnection connection;

        public QueryExecutor(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public TimeSpan Timeout { get; set; }

        public async Task<DataTable> ExecuteQueryAsync(string commandText, CancellationToken token)
        {
            //await Task.Delay(TimeSpan.FromSeconds(5), token);
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(token);
            }
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = (int)Timeout.TotalSeconds;
                command.CommandText = commandText;

                using (var reader = await command.ExecuteReaderAsync(token))
                {
                    DataTable table = new DataTable();
                    await Task.Run(() =>
                    {
                        table.Load(reader);
                        //foreach (var dataColumn in table.Columns.Cast<DataColumn>())
                        //{
                        //    dataColumn.ColumnName = $"{dataColumn.ColumnName} ({dataColumn.DataType.Name})";
                        //}
                    }, token);
                    return table;
                }
            }
        }
        public async Task<Boolean> ExecuteNonQueryAsync(string commandText, CancellationToken token)
        {
            bool InsertedStatus = false;
            //await Task.Delay(TimeSpan.FromSeconds(5), token);
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(token);
            }
            using (var command = connection.CreateCommand())
            {
                command.CommandTimeout = (int)Timeout.TotalSeconds;
                command.CommandText = commandText;
                await command.ExecuteNonQueryAsync();
                InsertedStatus = true;
            }
            return InsertedStatus;
        }
    }

}
