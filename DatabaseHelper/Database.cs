using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DatabaseHelper {
	public class Database {
		#region Private Members

		#endregion

		#region Private Methods

		#endregion

		#region Constructors

		public Database() {
			ConnectionString = ConfigurationManager.ConnectionStrings[this.GetType().Name].ConnectionString;
		}

		public Database(string connectionString) {
			ConnectionString = connectionString;
		}

		#endregion

		#region Events

		public event Action<string, Exception> OnError;

		#endregion

		#region Public Properties

		public string ConnectionString { get; private set; }

		#endregion

		#region Public Methods

		public SqlConnection GetConnection(string connectionString) {
			var connection = new SqlConnection(connectionString);
			return connection;
		}

		public SqlConnection GetConnection() {
			return GetConnection(ConnectionString);
		}

		public SqlCommand GetCommand(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters) {
			var command = new SqlCommand() {
				Connection = GetConnection(connectionString),
				CommandText = commandText,
				CommandType = commandType
			};
			command.Parameters.AddRange(parameters);

			return command;
		}

		public SqlCommand GetCommand(string commandText, CommandType commandType, params SqlParameter[] parameters) {
			return GetCommand(ConnectionString, commandText, commandType, parameters);
		}

		public SqlCommand GetCommand(string commandText, params SqlParameter[] parameters) {
			return GetCommand(ConnectionString, commandText, CommandType.Text, parameters);
		}

		// ExecuteReader

		public SqlDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters) {
			var command = GetCommand(connectionString, commandText, commandType, parameters);
			command.Connection.Open();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}

		public SqlDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType) {
			return ExecuteReader(connectionString, commandText, commandType);
		}

		public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters) {
			return ExecuteReader(ConnectionString, commandText, CommandType.Text, parameters);
		}

		// ExecuteNonQuery

		public int ExecuteNonQuery(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters) {
			var command = GetCommand(connectionString, commandText, commandType, parameters);
			int result;
			try {
				command.Connection.Open();
				result = command.ExecuteNonQuery();
			} catch (Exception ex) {
				OnError?.Invoke("ExecuteNonQuery", ex);
				throw;
			} finally {
				command.Connection.Close();
			}
			return result;
		}
        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(ConnectionString, commandText, commandType, parameters);
        }
		public int ExecuteNonQuery(string connectionString, string commandText, params SqlParameter[] parameters) {
			return ExecuteNonQuery(connectionString, commandText, CommandType.Text, parameters);
		}

		public int ExecuteNonQuery(string commandText, params SqlParameter[] parameters) {
			return ExecuteNonQuery(ConnectionString, commandText, parameters);
		}

		// ExecuteScalar

		public object ExecuteScalar(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters) {
			var command = GetCommand(connectionString, commandText, commandType, parameters);
			object result;
			try {
				command.Connection.Open();
				result = command.ExecuteScalar();
			} catch (Exception ex) {
				OnError?.Invoke("ExecuteScalar", ex);
				throw;
			} finally {
				command.Connection.Close();
			}
			return result;
		}

		public object ExecuteScalar(string connectionString, string commandText, params SqlParameter[] parameters) {
			return ExecuteScalar(connectionString, commandText, CommandType.Text, parameters);
		}

		public object ExecuteScalar(string commandText, params SqlParameter[] parameters) {
			return ExecuteScalar(ConnectionString, commandText, parameters);
		}

		#endregion

		//todo: get commandis msgavsad gadaaketet overloadebi danarcheni brdzanebebistvis
	}
}
