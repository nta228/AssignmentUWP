using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2009M1HelloUWP.Entities;

namespace T2009M1HelloUWP.Models
{
    public class NoteModel
    {
        private static string _insertStatementTemplate = "INSERT INTO notes (Id, Title, Detail, CreatedAt)" +
            " values (@id, @title, @detail, @created_at)";
        private static string _selectStatementTemplate = "SELECT * FROM notes";
        private static string _selectStatementWithConditionTemplate = "SELECT * FROM notes WHERE Title like @keyword";
        //using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
        //{
        //    db.Open(); // mở kết nối
        //    // tạo chuỗi câu lệnh.
        //    string tableCommand = "Select * from DemoTable";
        //    // tạo đối tượng câu lệnh SqliteCommand để đẩy vào trong db.
        //    SqliteCommand createTable = new SqliteCommand(tableCommand, db);
        //    // trả về ResultSet
        //    SqliteDataReader sqliteDataReader = createTable.ExecuteReader();
        //    // tạo ra biến cờ để check xem còn dữ liệu trả về ko
        //    while (sqliteDataReader.Read()) {                   
        //        int id = sqliteDataReader.GetInt32(0);
        //        string text = sqliteDataReader.GetString(1);
        //    }               
        //}

        public bool Save(Note note)
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand(_insertStatementTemplate, db);
                    insertCommand.Parameters.AddWithValue("@id", note.Id);
                    insertCommand.Parameters.AddWithValue("@title", note.Title);
                    insertCommand.Parameters.AddWithValue("@detail", note.Detail);
                    insertCommand.Parameters.AddWithValue("@created_at", note.CreatedAt);
                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public bool ClearData()
        {
            try
            {
                using (SqliteConnection db = new SqliteConnection($"Filename={DatabaseMigration._databasePath}"))
                {
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand("DELETE FROM notes", db);                 
                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public List<Note> FindAll()
        {
            List<Note> notes = new List<Note>();
            try
            {
                // mở kết nối.
                using (SqliteConnection cnn = new SqliteConnection($"FileName={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    // tạo câu lệnh.
                    SqliteCommand cmd = new SqliteCommand(_selectStatementTemplate, cnn);
                    // bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = Convert.ToString(reader["Id"]);
                        var title = Convert.ToString(reader["Title"]);
                        var detail = Convert.ToString(reader["Detail"]);
                        var createdAt = Convert.ToDateTime(reader["CreatedAt"]);
                        var note = new Note()
                        {
                            Title = title,
                            Id = id,
                            Detail = detail,
                            CreatedAt = createdAt
                        };
                        notes.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return notes;
        }

        public List<Note> SearchByKeyword(string keyword)
        {
            List<Note> notes = new List<Note>();
            try
            {
                // mở kết nối.
                using (SqliteConnection cnn = new SqliteConnection($"FileName={DatabaseMigration._databasePath}"))
                {
                    cnn.Open();
                    // tạo câu lệnh.
                    //var select = $"select * from notes where Title like '%{keyword}%'";
                    //SqliteCommand cmd = new SqliteCommand(select, cnn);
                    SqliteCommand cmd = new SqliteCommand(_selectStatementWithConditionTemplate, cnn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    Debug.WriteLine(cmd.CommandText);
                    // bắn lệnh vào và lấy dữ liệu.
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = Convert.ToString(reader["Id"]);
                        var title = Convert.ToString(reader["Title"]);
                        var detail = Convert.ToString(reader["Detail"]);
                        var createdAt = Convert.ToDateTime(reader["CreatedAt"]);
                        var note = new Note()
                        {
                            Title = title,
                            Id = id,
                            Detail = detail,
                            CreatedAt = createdAt
                        };
                        notes.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return notes;
        }

        public void FindById()
        {
        }

        public void Update()
        {
        }

        public void Delete()
        {
        }
    }
}
