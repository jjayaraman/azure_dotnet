using Snowflake.Data.Client;
using System.Data;
using System.Data.Common;

public class SnowflakeUtils
{
    private readonly string _connString;

    public SnowflakeUtils()
    {
        _connString = Environment.GetEnvironmentVariable("SNOWFLAKE_CONN");
    }

    public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string sql, object[] bindParams = null)
    {
        var results = new List<Dictionary<string, object>>();

        using (var conn = new SnowflakeDbConnection())
        {
            conn.ConnectionString = _connString;
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters((SnowflakeDbCommand)cmd, bindParams);

            using SnowflakeDbDataReader reader = (SnowflakeDbDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }
        }

        return results;
    }

    public async Task<int> ExecuteNonQueryAsync(string sql, object[] bindParams = null)
    {
        using var conn = new SnowflakeDbConnection();
        conn.ConnectionString = _connString;
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        AddParameters((SnowflakeDbCommand)cmd, bindParams);

        return await cmd.ExecuteNonQueryAsync();
    }

    private static void AddParameters(SnowflakeDbCommand cmd, object[] bindParams)
    {
        if (bindParams == null) return;

        for (int i = 0; i < bindParams.Length; i++)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = $"p{i + 1}";   // Snowflake uses positional params
            p.Value = bindParams[i] ?? DBNull.Value;
            cmd.Parameters.Add(p);
        }
    }
}
