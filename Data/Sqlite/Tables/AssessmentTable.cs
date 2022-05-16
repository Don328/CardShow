using CardShow.Data.Models;
using CardShow.Data.Sqlite.Schema;
using CardShow.Data.SqliteSchema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Tables
{
    internal static class AssessmentTable
    {
        internal static IEnumerable<_Assessment> GetCardAssesments(
            SqliteConnection conn, int cardId)
        {
            var assesments = new List<_Assessment>();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = ReadTable.CardAssessments;
                ParamBuilder.Build(cmd, "@cardId", cardId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assesments.Add(new _Assessment()
                        {
                            Id = reader.GetInt32(0),
                            CardId = reader.GetInt32(1),
                            Date = reader.GetDateTime(2),
                            HighGrade = reader.GetInt32(3),
                            LowGrade = reader.GetInt32(4),
                            Text = reader.GetString(5),
                            Corners = reader.GetString(6),
                            Edges = reader.GetString(7),
                            Centering = reader.GetString(8),
                            Surface = reader.GetString(9)
                        });
                    }
                }
            }

            return assesments;
        }

        internal static async Task<int> Create(
            SqliteConnection conn,
            _Assessment assesment)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Assessment;
                ParamBuilder.Build(cmd, "@cardId", assesment.CardId);
                ParamBuilder.Build(cmd, "@date", assesment.Date);
                ParamBuilder.Build(cmd, "@highGrade", assesment.HighGrade);
                ParamBuilder.Build(cmd, "@lowGrade", assesment.LowGrade);
                ParamBuilder.Build(cmd, "@text", assesment.Text);
                ParamBuilder.Build(cmd, "@corners", assesment.Corners);
                ParamBuilder.Build(cmd, "@edges", assesment.Edges);
                ParamBuilder.Build(cmd, "@centering", assesment.Centering);
                ParamBuilder.Build(cmd, "@surface", assesment.Surface);

                long rowid = (long)cmd.ExecuteScalar();
                assesment.Id = (int)rowid;
            }

            return await Task.FromResult(assesment.Id);
        }

        internal static async Task Delete(
            SqliteConnection conn, int id)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = DeleteRow.Assessment;
            ParamBuilder.Build(cmd, "@id", id);
            cmd.ExecuteNonQuery();

            await Task.CompletedTask;
        }

        internal static bool Any(
            SqliteConnection conn,
            int cardId)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = ReadTable.CardAssessments;
            ParamBuilder.Build(cmd, "@cardId", cardId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return true;
            }

            return false;
        }

        internal static bool Exists(
            SqliteConnection conn,
            int id)
        {
            using var cmd = conn.CreateCommand();
            ParamBuilder.Build(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return true;
            }

            return false;
        }
    }
}
