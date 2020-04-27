using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SqlServerDbService : IProjectsDbService
    {
        private static string connectionString;

        static SqlServerDbService()
        {
            connectionString = "Data Source=db-mssql;Initial Catalog=s18731;Integrated Security=True";
        }

        public bool DatabaseContainsTaskType(int id, string name)
        {
            using (var client = new SqlConnection(connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "SELECT * FROM TaskType WHERE IdTaskType = @id AND Name = @name";
                com.Parameters.AddWithValue("id", id);
                com.Parameters.AddWithValue("name", name);


                client.Open();
                var dr = com.ExecuteReader();
                dr.Read();

                if (!dr.HasRows)
                    return false;
                else
                    return true;
            }
        }

        public List<TaskGet> GetTaskInfo(int projectId)
        {
            var taskInfoList = new List<TaskGet>();

            using (var client = new SqlConnection(connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "SELECT PROJECT.Name AS 'Project_Name', TASK.IdTask, TASK.Name, TASK.Description, TASK.Deadline FROM TASK INNER JOIN PROJECT ON PROJECT.IdProject = TASK.IdProject WHERE TASK.IdProject = @projectId ORDER BY Task.Deadline;";
                com.Parameters.AddWithValue("projectId", projectId);

                client.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var taskInfo = new TaskGet();
                    taskInfo.ProjectName = dr["Project_Name"].ToString();
                    taskInfo.IdTask = Int32.Parse(dr["IdTask"].ToString());
                    taskInfo.Name = dr["Name"].ToString();
                    taskInfo.Description = dr["Description"].ToString();
                    taskInfo.Deadline = DateTime.Parse(dr["Deadline"].ToString());

                    taskInfoList.Add(taskInfo);
                }

                if (!dr.HasRows)
                    return null;

                return taskInfoList;
            }
        }

        public bool InsertNewTask(TaskNew taskNew)
        {
            using (var client = new SqlConnection(connectionString))
            using (var com = new SqlCommand())
            {
                SqlTransaction transaction = client.BeginTransaction("Transaction");
                com.Connection = client;
                com.CommandText = "INSERT INTO Task (IdTask, Name, Description, Deadline, IdTeam, IdAssignedTo, IdCreator) VALUES ((SELECT MAX(IdTask) FROM TASK) + 1, '@name', '@description', @deadline, @IdTeam, @IdAssignedTo, @IdCreator)";
                com.Parameters.AddWithValue("name", taskNew.Name);
                com.Parameters.AddWithValue("description", taskNew.Description);
                com.Parameters.AddWithValue("deadline", taskNew.Deadline);
                com.Parameters.AddWithValue("IdTeam", taskNew.IdTeam);
                com.Parameters.AddWithValue("IdAssignedTo", taskNew.IdAssignedTo);
                com.Parameters.AddWithValue("IdCreator", taskNew.IdCreator);

                client.Open();
                try
                {
                    var nonq = com.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    transaction.Rollback();
                    return false;
                }

                client.Close();
            }

            return true;
        }
    }
}