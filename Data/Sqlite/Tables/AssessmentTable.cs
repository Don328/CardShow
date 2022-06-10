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
            var assessments = new List<_Assessment>();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = ReadTable.CardAssessments;
                ParamBuilder.Build(cmd, "@cardId", cardId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assessments.Add(new _Assessment(
                            id:         reader.GetInt32(0),
                            cardId:     reader.GetInt32(1),
                            date:       reader.GetDateTime(2),
                            highGrade:  reader.GetInt32(3),
                            lowGrade:   reader.GetInt32(4),
                            text:       reader.GetString(5),
                            corners:    reader.GetString(6),
                            edges:      reader.GetString(7),
                            centering:  reader.GetString(8),
                            surface:    reader.GetString(9)));
                    }
                }
            }

            return assessments;
        }

        internal static async Task<int> Create(
            SqliteConnection conn,
            _Assessment assessment)
        {
            int createdId;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Assessment;
                ParamBuilder.Build(cmd, "@cardId", assessment.CardId);
                ParamBuilder.Build(cmd, "@date", assessment.Date);
                ParamBuilder.Build(cmd, "@highGrade", assessment.HighGrade);
                ParamBuilder.Build(cmd, "@lowGrade", assessment.LowGrade);
                ParamBuilder.Build(cmd, "@text", assessment.Text);
                ParamBuilder.Build(cmd, "@corners", assessment.Corners);
                ParamBuilder.Build(cmd, "@edges", assessment.Edges);
                ParamBuilder.Build(cmd, "@centering", assessment.Centering);
                ParamBuilder.Build(cmd, "@surface", assessment.Surface);

                long rowid = (long)cmd.ExecuteScalar();
                createdId = (int)rowid;
            }

            return await Task.FromResult(createdId);
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

        internal static async Task<bool> Any(
            SqliteConnection conn,
            int cardId)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = ReadTable.CardAssessments;
            ParamBuilder.Build(cmd, "@cardId", cardId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        internal static async Task<bool> Exists(
            SqliteConnection conn,
            int id)
        {
            using var cmd = conn.CreateCommand();
            ParamBuilder.Build(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
