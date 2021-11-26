using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2009M1HelloUWP.Entities;
using Windows.Storage;

namespace T2009M1HelloUWP.Models
{
    // Update database theo code project.
    public class DatabaseMigration
    {
        public static string _databasePath;
        private static string _databaseName = "mynote.db";
        private static string _createNoteTable = "CREATE TABLE IF NOT EXISTS notes " +
            "(Id NVARCHAR(100) PRIMARY KEY," +
            "Title NVARCHAR(255) NOT NULL," +
            "Detail Text NOT NULL," +
            "CreatedAt DateTime NULL)";       
        public async static void UpdateDabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(_databaseName, CreationCollisionOption.OpenIfExists);
            _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, _databaseName);
            using (SqliteConnection db = new SqliteConnection($"Filename={_databasePath}"))
            {
                db.Open();
                SqliteCommand createTableNote = new SqliteCommand(_createNoteTable, db);
                createTableNote.ExecuteNonQuery();
            }
            await GenerateData();
        }

        public async static Task GenerateData() {
            NoteModel noteModel = new NoteModel();
            noteModel.ClearData();
            noteModel.Save(new Note()
            {
                Id = Guid.NewGuid().ToString("N"),
                Title = "Đưa con đi học",
                Detail = "Dạy sớm đưa đi học",
                CreatedAt = DateTime.Now
            });
            noteModel.Save(new Note()
            {
                Id = Guid.NewGuid().ToString("N"),
                Title = "Tưới cây",
                Detail = "Chăm cây",
                CreatedAt = DateTime.Now
            });
            noteModel.Save(new Note()
            {
                Id = Guid.NewGuid().ToString("N"),
                Title = "Dạy tiếng anh cho con",
                Detail = "Dạy tiếng anh cho con",
                CreatedAt = DateTime.Now
            });            
        }
    }
}
