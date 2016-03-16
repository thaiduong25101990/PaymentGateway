using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Threading;
using System.Transactions;
using System.Data;
using System.Collections;
using System.Data.Odbc;

namespace BR.DataAccess
{
    public class OracleHelper
    {

        #region " Private utility methods & constructors"

        // Since this class provides only static methods, make the default constructor private to prevent
        // instances from being created with "new OracleHelper()".
        private void New()
        {
        }
        // New

        // This method is used to attach array of OracleParameters to a OracleCommand.
        // This method will assign a value of DbNull to any parameter with a direction of
        // InputOutput and a value of null.
        // This behavior will prevent default values from being used, but
        // this will be the less common case than an intended pure output parameter (derived as InputOutput)
        // where the user provided no input value.
        // Parameters:
        // -command - The command to which the parameters will be added
        // -commandParameters - an array of OracleParameters to be added to command
        private static void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {
            if ((command == null))
                throw new ArgumentNullException("command");
            if (((commandParameters != null)))
            {

                foreach (OracleParameter p in commandParameters)
                {
                    if (((p != null)))
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        // AttachParameters

        // This method assigns dataRow column values to an array of OracleParameters.
        // Parameters:
        // -commandParameters: Array of OracleParameters to be assigned values
        // -dataRow: the dataRow used to hold the stored procedure' s parameter values
        private static void AssignParameterValues(OracleParameter[] commandParameters, DataRow dataRow)
        {

            if (commandParameters == null || dataRow == null)
            {
                // Do nothing if we get no data
                return; // TODO: might not be correct. Was : Exit Sub
            }

            // Set the parameters values

            int i = 0;
            foreach (OracleParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if ((commandParameter.ParameterName == null || commandParameter.ParameterName.Length <= 1))
                {
                    throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: ' {1}' .", commandParameter.ParameterName));
                }
                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                {
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                }
                i = i + 1;
            }
        }

        // This method assigns an array of values to an array of OracleParameters.
        // Parameters:
        // -commandParameters - array of OracleParameters to be assigned values
        // -array of objects holding the values to be assigned
        private static void AssignParameterValues(OracleParameter[] commandParameters, object[] parameterValues)
        {

            int i;
            int j;

            if ((commandParameters == null) && (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                //Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            }

            // Value array
            j = commandParameters.Length - 1;
            for (i = 0; i <= j; i++)
            {
                if (i > parameterValues.Length - 1)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    // If the current array value derives from IDbDataParameter, then assign its Value property
                    if (parameterValues[i] is IDbDataParameter)
                    {
                        IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                        if ((paramInstance.Value == null))
                        {
                            parameterValues[i] = DBNull.Value;
                        }
                        else
                        {
                            parameterValues[i] = paramInstance.Value;
                        }
                    }
                    else if ((parameterValues[i] == null))
                    {
                        parameterValues[i] = DBNull.Value;
                    }
                    else
                    {
                        parameterValues[i] = parameterValues[i];
                    }
                }
            }
        }
        // AssignParameterValues

        // This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        // to the provided command.
        // Parameters:
        // -command - the OracleCommand to be prepared
        // -connection - a valid OracleConnection, on which to execute this command
        // -transaction - a valid OracleTransaction, or ' null'
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParameters to be associated with the command or ' null' if no parameters are required
        private static void PrepareCommand(OracleCommand command,
                                           OracleConnection connection,
                                           OracleTransaction transaction,
                                           CommandType commandType,
                                           string commandText,
                                           OracleParameter[] commandParameters,
                                           ref bool mustCloseConnection)
        {

            if ((command == null))
                throw new ArgumentNullException("command");
            if ((commandText == null || commandText.Length == 0))
                throw new ArgumentNullException("commandText");


            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                mustCloseConnection = true;
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;
            //command.CommandTimeout = 0
            // Set the command text (stored procedure name or Oracle statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it.
            if ((transaction != null))
            {
                if (transaction.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                //command.Transaction = transaction
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if ((commandParameters != null))
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        // PrepareCommand

        #endregion

        #region " ExecuteNonQuery"

        // Execute a OracleCommand (that returns no resultset and takes no parameters) against the database specified in
        // the connection string.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders")
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteNonQuery(connectionString, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteNonQuery

        // Execute a OracleCommand (that returns no resultset) against the database specified in the connection string
        // using the provided parameters.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            // Create & open a OracleConnection, and dispose of it after we are done
            OracleConnection connection;

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            connection = new OracleConnection(connectionString);

            try
            {

                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }

            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        // ExecuteNonQuery

        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the database specified in
        // the connection string using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(connString, "PublishOrders", 24, 36)
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: An int representing the number of rows affected by the command

        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)

                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            // Otherwise we can just call the SP without params
            else
            {
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteNonQuery

        // Execute a OracleCommand (that returns no resultset and takes no parameters) against the provided OracleConnection.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders")
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An int representing the number of rows affected by the command



        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteNonQuery(connection, commandType, commandText, (OracleParameter[])null);

        }
        // ExecuteNonQuery

        // Execute a OracleCommand (that returns no resultset) against the specified OracleConnection
        // using the provided parameters.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            int retval;

            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection,
                (OracleTransaction)null,
                commandType,
                commandText,
                commandParameters,
                ref mustCloseConnection);

            // Finally, execute the command
            retval = cmd.ExecuteNonQuery();

            // Detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if ((mustCloseConnection))
                connection.Close();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return retval;
        }

        //private static void PrepareCommand(OracleCommand cmd, OracleConnection connection, OracleTransaction oracleTransaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, bool mustCloseConnection)
        //{
        //    throw new NotImplementedException();
        //}
        // ExecuteNonQuery

        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the specified OracleConnection
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim result As integer = ExecuteNonQuery(conn, "PublishOrders", 24, 36)
        // Parameters:
        // -connection - a valid OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");
            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }

        }
        // ExecuteNonQuery

        // Execute a OracleCommand (that returns no resultset and takes no parameters) against the provided OracleTransaction.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders")
        // Parameters:
        // -transaction - a valid OracleTransaction associated with the connection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteNonQuery(transaction, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteNonQuery

        // Execute a OracleCommand (that returns no resultset) against the specified OracleTransaction
        // using the provided parameters.
        // e.g.:
        // Dim result As Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {

            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            int retval;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);

            // Finally, execute the command
            retval = cmd.ExecuteNonQuery();

            // Detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            return retval;
        }
        // ExecuteNonQuery

        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the specified OracleTransaction
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim result As Integer = OracleHelper.ExecuteNonQuery(trans, "PublishOrders", 24, 36)
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: An int representing the number of rows affected by the command
        public static int ExecuteNonQuery(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteNonQuery

        #endregion

        #region " ExecuteDataset"

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the database specified in
        // the connection string.
        // e.g.:
        // Dim ds As DataSet = OracleHelper.ExecuteDataset("", commandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteDataset(connectionString, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset) against the database specified in the connection string
        // using the provided parameters.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            DataSet ds = new DataSet();
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");

            // Create & open a OracleConnection, and dispose of it after we are done
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                ds = ExecuteDataset(connection, commandType, commandText, commandParameters);
                connection.Dispose();

            }
            catch
            {
                return null;
            }
            return ds;
        }
        // ExecuteDataset

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in
        // the connection string using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim ds As Dataset= ExecuteDataset(connString, "GetOrders", 24, 36)
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");
            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText)
        {

            // Pass through the call providing null for the set of OracleParameters
            return ExecuteDataset(connection, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameters.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((connection == null))
                throw new ArgumentNullException("connection");
            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            OracleDataAdapter dataAdatpter;
            bool mustCloseConnection = true;

            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters,ref  mustCloseConnection);

            try
            {
                // Create the DataAdapter & DataSet
                dataAdatpter = new OracleDataAdapter(cmd);

                // Fill the DataSet using default values for DataTable names, etc
                dataAdatpter.Fill(ds);
                dataAdatpter.Dispose();
            }
            catch (Exception ex)
            {
                return null;
            }
            if ((mustCloseConnection))
                connection.Close();

            if (connection.State == ConnectionState.Open)
            { connection.Close();
            connection.Dispose();
            }
            return ds;
        }

        public static DataTable ExcuteDataTableODBC(string cmdText,OdbcConnection OdbcConn)
        {
            DataTable tblValue = new DataTable();
            OdbcDataAdapter da = new OdbcDataAdapter();            
            OdbcCommand oraCommand = new OdbcCommand(cmdText, OdbcConn);
            oraCommand.CommandType = CommandType.Text;

            try
            {
                da = new OdbcDataAdapter(oraCommand);
                da.Fill(tblValue);
                da.Dispose();
                oraCommand.Dispose();
                oraCommand.Dispose();
            }
            catch
            {
                tblValue = null;
                da.Dispose();
                oraCommand.Dispose();
                oraCommand.Dispose();
            }
            return tblValue;
        }


        // ExecuteDataset

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(conn, "GetOrders", 24, 36)
        // Parameters:
        // -connection - a valid OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(OracleConnection connection, string spName, params object[] parameterValues)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            //If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then

            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of OracleParameters
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            //Else ' Otherwise we can just call the SP without params
            // Return ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            //End If

        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders")
        // Parameters
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteDataset(transaction, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the provided parameters.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            OracleDataAdapter dataAdatpter;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);

            try
            {
                // Create the DataAdapter & DataSet
                dataAdatpter = new OracleDataAdapter(cmd);

                // Fill the DataSet using default values for DataTable names, etc
                dataAdatpter.Fill(ds);

                // Detach the OracleParameters from the command object, so they can be used again
                cmd.Parameters.Clear();


                if (((dataAdatpter != null)))
                    dataAdatpter.Dispose();
            }
            catch
            {
                return null;
            }

            // Return the dataset
            return ds;

        }
        // ExecuteDataset

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified
        // OracleTransaction using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(trans, "GetOrders", 24, 36)
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A dataset containing the resultset generated by the command
        public static DataSet ExecuteDataset(OracleTransaction transaction, string spName, params object[] parameterValues)
        {

            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteDataset

        #endregion

        #region " ExecuteScalar"

        // Execute a OracleCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in
        // the connection string.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount"))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteScalar(connectionString, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteScalar

        // Execute a OracleCommand (that returns a 1x1 resultset) against the database specified in the connection string
        // using the provided parameters.
        // e.g.:
        // Dim orderCount As Integer = Cint(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24)))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            object scalar;
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            // Create & open a OracleConnection, and dispose of it after we are done.
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                scalar = ExecuteScalar(connection, commandType, commandText, commandParameters);
                connection.Dispose();
            }
            catch
            {
                return null;
            }
            return scalar;
        }
        // ExecuteScalar

        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the database specified in
        // the connection string using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(connString, "GetOrderCount", 24, 36))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            // Otherwise we can just call the SP without params
            else
            {
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteScalar

        // Execute a OracleCommand (that returns a 1x1 resultset and takes no parameters) against the provided OracleConnection.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount"))
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteScalar(connection, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteScalar

        // Execute a OracleCommand (that returns a 1x1 resultset) against the specified OracleConnection
        // using the provided parameters.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24)))
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            object retval;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters,ref mustCloseConnection);

            // Execute the command & return the results
            retval = cmd.ExecuteScalar();

            // Detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if ((mustCloseConnection))
                connection.Close();

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return retval;

        }
        // ExecuteScalar

        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the specified OracleConnection
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(conn, "GetOrderCount", 24, 36))
        // Parameters:
        // -connection - a valid OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }

        }
        // ExecuteScalar

        // Execute a OracleCommand (that returns a 1x1 resultset and takes no parameters) against the provided OracleTransaction.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount"))
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteScalar(transaction, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteScalar

        // Execute a OracleCommand (that returns a 1x1 resultset) against the specified OracleTransaction
        // using the provided parameters.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24)))
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            object retval;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);

            // Execute the command & return the results
            retval = cmd.ExecuteScalar();

            // Detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            return retval;
        }
        // ExecuteScalar

        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the specified OracleTransaction
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim orderCount As Integer = CInt(ExecuteScalar(trans, "GetOrderCount", 24, 36))
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: An object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteScalar

        #endregion

        #region "FillDataset"
        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the database specified in
        // the connection string.
        // e.g.:
        // FillDataset (connString, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"})
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");

            // Create & open a OracleConnection, and dispose of it after we are done
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(connectionString);

                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandType, commandText, dataSet, tableNames);
                connection.Dispose();
            }
            catch
            {


            }
        }

        // Execute a OracleCommand (that returns a resultset) against the database specified in the connection string
        // using the provided parameters.
        // e.g.:
        // FillDataset (connString, CommandType.StoredProcedure, "GetOrders", ds, new String() = {"orders"}, new OracleParameter("@prodid", 24))
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -commandParameters: An array of OracleParamters used to execute the command
        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params OracleParameter[] commandParameters)
        {

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");

            // Create & open a OracleConnection, and dispose of it after we are done
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(connectionString);

                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
                connection.Dispose();
            }
            catch
            {

            }
        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in
        // the connection string using the provided parameter values. This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // FillDataset (connString, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"}, 24)
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -spName: the name of the stored procedure
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -parameterValues: An array of objects to be assigned As the input values of the stored procedure
        public static void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");

            // Create & open a OracleConnection, and dispose of it after we are done
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(connectionString);

                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, spName, dataSet, tableNames, parameterValues);
                connection.Dispose();
            }
            catch
            {
            }
        }

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection.
        // e.g.:
        // FillDataset (conn, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"})
        // Parameters:
        // -connection: A valid OracleConnection
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        public static void FillDataset(OracleConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {

            FillDataset(connection, commandType, commandText, dataSet, tableNames, null);

        }

        // Execute a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameters.
        // e.g.:
        // FillDataset (conn, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"}, new OracleParameter("@prodid", 24))
        // Parameters:
        // -connection: A valid OracleConnection
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -commandParameters: An array of OracleParamters used to execute the command
        public static void FillDataset(OracleConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params OracleParameter[] commandParameters)
        {

            FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);

        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameter values. This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // FillDataset (conn, "GetOrders", ds, new string() {"orders"}, 24, 36)
        // Parameters:
        // -connection: A valid OracleConnection
        // -spName: the name of the stored procedure
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -parameterValues: An array of objects to be assigned as the input values of the stored procedure
        public static void FillDataset(OracleConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }

        }

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction.
        // e.g.:
        // FillDataset (trans, CommandType.StoredProcedure, "GetOrders", ds, new string() {"orders"})
        // Parameters:
        // -transaction: A valid OracleTransaction
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        public static void FillDataset(OracleTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {

            FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        // Execute a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the provided parameters.
        // e.g.:
        // FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string() {"orders"}, new OracleParameter("@prodid", 24))
        // Parameters:
        // -transaction: A valid OracleTransaction
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -commandParameters: An array of OracleParamters used to execute the command
        public static void FillDataset(OracleTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params OracleParameter[] commandParameters)
        {

            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);

        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified
        // OracleTransaction using the provided parameter values. This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // FillDataset(trans, "GetOrders", ds, new String(){"orders"}, 24, 36)
        // Parameters:
        // -transaction: A valid OracleTransaction
        // -spName: the name of the stored procedure
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -parameterValues: An array of objects to be assigned as the input values of the stored procedure
        public static void FillDataset(OracleTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {

            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        // Private helper method that execute a OracleCommand (that returns a resultset) against the specified OracleTransaction and OracleConnection
        // using the provided parameters.
        // e.g.:
        // FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"}, new OracleParameter("@prodid", 24))
        // Parameters:
        // -connection: A valid OracleConnection
        // -transaction: A valid OracleTransaction
        // -commandType: the CommandType (stored procedure, text, etc.)
        // -commandText: the stored procedure name or T-Oracle command
        // -dataSet: A dataset wich will contain the resultset generated by the command
        // -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
        // by a user defined name (probably the actual table name)
        // -commandParameters: An array of OracleParamters used to execute the command
        private static void FillDataset(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params OracleParameter[] commandParameters)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");

            // Create a command and prepare it for execution
            OracleCommand command = new OracleCommand();

            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters,ref mustCloseConnection);

            // Create the DataAdapter & DataSet
            OracleDataAdapter dataAdapter = new OracleDataAdapter(command);

            try
            {
                // Add the table mappings specified by the user
                if ((tableNames != null) && tableNames.Length > 0)
                {

                    string tableName = "Table";
                    int index;

                    for (index = 0; index <= tableNames.Length - 1; index++)
                    {
                        if ((tableNames[index] == null || tableNames[index].Length == 0))
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName = tableName + (index + 1).ToString();
                    }
                }

                // Fill the DataSet using default values for DataTable names, etc
                dataAdapter.Fill(dataSet);

                // Detach the OracleParameters from the command object, so they can be used again
                command.Parameters.Clear();
            }
            finally
            {
                if (((dataAdapter != null)))
                    dataAdapter.Dispose();
            }

            if ((mustCloseConnection))
                connection.Close();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }

        }
        #endregion
        #region "UpdateDataset"
        // Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        // e.g.:
        // UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order")
        // Parameters:
        // -insertCommand: A valid transact-Oracle statement or stored procedure to insert new records into the data source
        // -deleteCommand: A valid transact-Oracle statement or stored procedure to delete records from the data source
        // -updateCommand: A valid transact-Oracle statement or stored procedure used to update records in the data source
        // -dataSet: the DataSet used to update the data source
        // -tableName: the DataTable used to update the data source
        public static void UpdateDataset(OracleCommand insertCommand, OracleCommand deleteCommand, OracleCommand updateCommand, DataSet dataSet, string tableName)
        {

            if ((insertCommand == null))
                throw new ArgumentNullException("insertCommand");
            if ((deleteCommand == null))
                throw new ArgumentNullException("deleteCommand");
            if ((updateCommand == null))
                throw new ArgumentNullException("updateCommand");
            if ((dataSet == null))
                throw new ArgumentNullException("dataSet");
            if ((tableName == null || tableName.Length == 0))
                throw new ArgumentNullException("tableName");

            // Create a OracleDataAdapter, and dispose of it after we are done
            OracleDataAdapter dataAdapter = new OracleDataAdapter();
            try
            {
                // Set the data adapter commands
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;

                // Update the dataset changes in the data source
                dataAdapter.Update(dataSet, tableName);

                // Commit all the changes made to the DataSet
                dataSet.AcceptChanges();
            }
            finally
            {
                if (((dataAdapter != null)))
                    dataAdapter.Dispose();
            }
        }
        #endregion

        #region "CreateCommand"
        // Simplify the creation of a Oracle command object by allowing
        // a stored procedure and optional parameters to be provided
        // e.g.:
        // Dim command As OracleCommand = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName")
        // Parameters:
        // -connection: A valid OracleConnection object
        // -spName: the name of the stored procedure
        // -sourceColumns: An array of string to be assigned as the source columns of the stored procedure parameters
        // Returns:
        // a valid OracleCommand object
        public static OracleCommand CreateCommand(OracleConnection connection, string spName, params string[] sourceColumns)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");
            // Create a OracleCommand
            OracleCommand cmd = new OracleCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // If we receive parameter values, we need to figure out where they go
            if ((sourceColumns != null) && sourceColumns.Length > 0)
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided source columns to these parameters based on parameter order
                int index;
                for (index = 0; index <= sourceColumns.Length - 1; index++)
                {
                    commandParameters[index].SourceColumn = sourceColumns[index];
                }

                // Attach the discovered parameters to the OracleCommand object
                AttachParameters(cmd, commandParameters);
            }
            return cmd;

        }
        #endregion

        #region "ExecuteNonQueryTypedParams"
        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the database specified in
        // the connection string using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values
        // Returns:
        // an int representing the number of rows affected by the command
        public static int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            int functionReturnValue = 0;
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the specified OracleConnection
        // using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        // Parameters:
        // -connection:a valid OracleConnection object
        // -spName: the name of the stored procedure
        // -dataRow:The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // an int representing the number of rows affected by the command
        public static int ExecuteNonQueryTypedParams(OracleConnection connection, string spName, DataRow dataRow)
        {
            int functionReturnValue = 0;
            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns no resultset) against the specified
        // OracleTransaction using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        // Parameters:
        // -transaction:a valid OracleTransaction object
        // -spName:the name of the stored procedure
        // -dataRow:The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // an int representing the number of rows affected by the command
        public static int ExecuteNonQueryTypedParams(OracleTransaction transaction, string spName, DataRow dataRow)
        {
            int functionReturnValue = 0;

            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {

                functionReturnValue = OracleHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }
        #endregion

        #region "ExecuteDatasetTypedParams"
        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in
        // the connection string using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // a dataset containing the resultset generated by the command
        public static DataSet ExecuteDatasetTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            DataSet functionReturnValue = null;
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {

                functionReturnValue = OracleHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the dataRow column values as the store procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        // Parameters:
        // -connection: A valid OracleConnection object
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // a dataset containing the resultset generated by the command
        public static DataSet ExecuteDatasetTypedParams(OracleConnection connection, string spName, DataRow dataRow)
        {
            DataSet functionReturnValue = null;

            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {

                functionReturnValue = OracleHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        // Parameters:
        // -transaction: A valid OracleTransaction object
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // a dataset containing the resultset generated by the command
        public static DataSet ExecuteDatasetTypedParams(OracleTransaction transaction, string spName, DataRow dataRow)
        {
            DataSet functionReturnValue = null;
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {

                functionReturnValue = OracleHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }
        #endregion



        #region "ExecuteDataAdarpter"

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the database specified in
        // the connection string.
        // e.g.:
        // Dim ds As DataSet = OracleHelper.ExecuteDataset("", commandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter  ExecuteDaAdarpter(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteDaAdarpter(connectionString, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset) against the database specified in the connection string
        // using the provided parameters.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter ExecuteDaAdarpter(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");

            // Create & open a OracleConnection, and dispose of it after we are done
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                da = ExecuteDaAdarpter(connection, commandType, commandText, commandParameters);
                connection.Dispose();

            }
            catch
            {
                return null;
            }
            return da;
        }
        // ExecuteDataset

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in
        // the connection string using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim ds As Dataset= ExecuteDataset(connString, "GetOrders", 24, 36)
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter  ExecuteDaAdarpter(string connectionString, string spName, params object[] parameterValues)
        {

            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");
            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteDaAdarpter(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDaAdarpter(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter ExecuteDaAdarpter(OracleConnection connection, CommandType commandType, string commandText)
        {

            // Pass through the call providing null for the set of OracleParameters
            return ExecuteDaAdarpter(connection, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameters.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter  ExecuteDaAdarpter(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((connection == null))
                throw new ArgumentNullException("connection");
            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            
            OracleDataAdapter dataAdatpter;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters,ref  mustCloseConnection);

            try
            {
                // Create the DataAdapter & DataSet
                dataAdatpter = new OracleDataAdapter(cmd);

                // Fill the DataSet using default values for DataTable names, etc
              
            }
            catch
            {
                return null;
            }
            if ((mustCloseConnection))
                connection.Close();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            return dataAdatpter;
        }
        // ExecuteDataset

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(conn, "GetOrders", 24, 36)
        // Parameters:
        // -connection - a valid OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter  ExecuteDaAdarpter(OracleConnection connection, string spName, params object[] parameterValues)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            //If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then

            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of OracleParameters
            return ExecuteDaAdarpter(connection, CommandType.StoredProcedure, spName, commandParameters);
            //Else ' Otherwise we can just call the SP without params
            // Return ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            //End If

        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders")
        // Parameters
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter  ExecuteDaAdarpter(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteDaAdarpter(transaction, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteDataset

        // Execute a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the provided parameters.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter  ExecuteDaAdarpter(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            
            OracleDataAdapter dataAdatpter;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);

            try
            {
                // Create the DataAdapter & DataSet
                dataAdatpter = new OracleDataAdapter(cmd);

                // Fill the DataSet using default values for DataTable names, etc
                // Detach the OracleParameters from the command object, so they can be used again
                cmd.Parameters.Clear();


               
            }
            catch
            {
                return null;
            }

            // Return the OracleDataAdarpter
            return dataAdatpter;

        }
        // ExecuteDataset

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified
        // OracleTransaction using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim ds As Dataset = ExecuteDataset(trans, "GetOrders", 24, 36)
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A dataset containing the resultset generated by the command
        public static OracleDataAdapter ExecuteDaAdarpter(OracleTransaction transaction, string spName, params object[] parameterValues)
        {

            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteDaAdarpter(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDaAdarpter(transaction, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteDataset

        #endregion





        private enum OracleConnectionOwnership
        {

            // this enum is used to indicate whether the connection was provided by the caller, or created by OracleHelper, so that
            // we can set the appropriate CommandBehavior when calling ExecuteReader()
            // Connection is owned and managed by OracleHelper
            Internal,
            // Connection is owned and managed by the caller
            External
        }
        // OracleConnectionOwnership

        // Create and prepare a OracleCommand, and call ExecuteReader with the appropriate CommandBehavior.
        // If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        // If the caller provided the connection, we want to leave it to them to manage.
        // Parameters:
        // -connection - a valid OracleConnection, on which to execute this command
        // -transaction - a valid OracleTransaction, or ' null'
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParameters to be associated with the command or ' null' if no parameters are required
        // -connectionOwnership - indicates whether the connection parameter was provided by the caller, or created by OracleHelper
        // Returns: OracleDataReader containing the results of the command

        #region "ExecuteReader"
        private static OracleDataReader ExecuteReader(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, OracleConnectionOwnership connectionOwnership)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            try
            {
                // Create a reader
                OracleDataReader dataReader;

                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters,ref  mustCloseConnection);

                // Call ExecuteReader with the appropriate CommandBehavior
                if (connectionOwnership == OracleConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                // Detach the OracleParameters from the command object, so they can be used again
                bool canClear = true;

                foreach (OracleParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }

                try
                {
                    if ((canClear))
                        cmd.Parameters.Clear();
                }
                catch
                {

                }

                return dataReader;
            }
            catch
            {
                if ((mustCloseConnection))
                    connection.Close();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                throw;
            }
        }
        // ExecuteReader

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the database specified in
        // the connection string.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteReader(connectionString, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteReader

        // Execute a OracleCommand (that returns a resultset) against the database specified in the connection string
        // using the provided parameters.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");

            // Create & open a OracleConnection
            OracleConnection connection = new OracleConnection();
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();
                // Call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, OracleConnectionOwnership.Internal);
            }
            catch
            {
                // If we fail to return the OracleDatReader, we need to close the connection ourselves
                connection.Dispose();
                throw;
            }
        }
        // ExecuteReader

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in
        // the connection string using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(connString, "GetOrders", 24, 36)
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OracleParameters
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            // Otherwise we can just call the SP without params
            else
            {
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteReader

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteReader(connection, commandType, commandText, (OracleParameter[])null);

        }
        // ExecuteReader

        // Execute a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameters.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -connection - a valid OracleConnection
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            // Pass through the call to private overload using a null transaction value
            return ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, OracleConnectionOwnership.External);

        }
        // ExecuteReader

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(conn, "GetOrders", 24, 36)
        // Parameters:
        // -connection - a valid OracleConnection
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;
            // If we receive parameter values, we need to figure out where they go
            //If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            //Else ' Otherwise we can just call the SP without params
            // Return ExecuteReader(connection, CommandType.StoredProcedure, spName)
            //End If

        }
        // ExecuteReader

        // Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders")
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteReader(transaction, commandType, commandText, (OracleParameter[])null);
        }
        // ExecuteReader

        // Execute a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the provided parameters.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -commandType - the CommandType (stored procedure, text, etc.)
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters used to execute the command
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            // Pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, OracleConnectionOwnership.External);
        }
        // ExecuteReader

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the provided parameter values. This method will discover the parameters for the
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure' s return value parameter.
        // e.g.:
        // Dim dr As OracleDataReader = ExecuteReader(trans, "GetOrders", 24, 36)
        // Parameters:
        // -transaction - a valid OracleTransaction
        // -spName - the name of the stored procedure
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        // Returns: A OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReader(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            OracleParameter[] commandParameters;

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && parameterValues.Length > 0)
            {
                commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        // ExecuteReader

        #endregion

        #region "ExecuteReaderTypedParams"
        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in
        // the connection string using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // a OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReaderTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            OracleDataReader functionReturnValue = null;
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // Parameters:
        // -connection: A valid OracleConnection object
        // -spName: The name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // a OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReaderTypedParams(OracleConnection connection, string spName, DataRow dataRow)
        {
            OracleDataReader functionReturnValue = null;
            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // Parameters:
        // -transaction: A valid OracleTransaction object
        // -spName" The name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // a OracleDataReader containing the resultset generated by the command
        public static OracleDataReader ExecuteReaderTypedParams(OracleTransaction transaction, string spName, DataRow dataRow)
        {
            OracleDataReader functionReturnValue = null;
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }
        #endregion

        #region "ExecuteScalarTypedParams"
        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the database specified in
        // the connection string using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // Parameters:
        // -connectionString: A valid connection string for a OracleConnection
        // -spName: The name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            object functionReturnValue = null;
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");
            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the specified OracleConnection
        // using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // Parameters:
        // -connection: A valid OracleConnection object
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalarTypedParams(OracleConnection connection, string spName, DataRow dataRow)
        {
            object functionReturnValue = null;
            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }

        // Execute a stored procedure via a OracleCommand (that returns a 1x1 resultset) against the specified OracleTransaction
        // using the dataRow column values as the stored procedure' s parameters values.
        // This method will query the database to discover the parameters for the
        // stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // Parameters:
        // -transaction: A valid OracleTransaction object
        // -spName: the name of the stored procedure
        // -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // Returns:
        // an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalarTypedParams(OracleTransaction transaction, string spName, DataRow dataRow)
        {
            object functionReturnValue = null;
            if ((transaction == null))
                throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null))
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (((dataRow != null) && dataRow.ItemArray.Length > 0))
            {

                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                functionReturnValue = OracleHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                functionReturnValue = OracleHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
            return functionReturnValue;
        }
        #endregion

        #region "ExecuteXmlReaderTypedParams"
        // ' Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection
        // ' using the dataRow column values as the stored procedure' s parameters values.
        // ' This method will query the database to discover the parameters for the
        // ' stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // ' Parameters:
        // ' -connection: A valid OracleConnection object
        // ' -spName: the name of the stored procedure
        // ' -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // ' Returns:
        // ' an XmlReader containing the resultset generated by the command
        // Public Overloads Shared Function ExecuteXmlReaderTypedParams(ByVal connection As OracleConnection, ByVal spName As String, ByVal dataRow As DataRow) As XmlReader
        // If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        // If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
        // ' If the row has values, the store procedure parameters must be initialized
        // If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then

        // ' Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        // Dim commandParameters() As OracleParameter = OracleHelperParameterCache.GetSpParameterSet(connection, spName)

        // ' Set the parameters values
        // AssignParameterValues(commandParameters, dataRow)

        // ExecuteXmlReaderTypedParams = OracleHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters)
        // Else
        // ExecuteXmlReaderTypedParams = OracleHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName)
        // End If
        // End Function

        // ' Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleTransaction
        // ' using the dataRow column values as the stored procedure' s parameters values.
        // ' This method will query the database to discover the parameters for the
        // ' stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        // ' Parameters:
        // ' -transaction: A valid OracleTransaction object
        // ' -spName: the name of the stored procedure
        // ' -dataRow: The dataRow used to hold the stored procedure' s parameter values.
        // ' Returns:
        // ' an XmlReader containing the resultset generated by the command
        // Public Overloads Shared Function ExecuteXmlReaderTypedParams(ByVal transaction As OracleTransaction, ByVal spName As String, ByVal dataRow As DataRow) As XmlReader
        // If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        // If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
        // If (spName Is Nothing OrElse spName.Length = 0) Then Throw New ArgumentNullException("spName")
        // ' if the row has values, the store procedure parameters must be initialized
        // If (Not dataRow Is Nothing AndAlso dataRow.ItemArray.Length > 0) Then

        // ' Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        // Dim commandParameters() As OracleParameter = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection, spName)

        // ' Set the parameters values
        // AssignParameterValues(commandParameters, dataRow)

        // ExecuteXmlReaderTypedParams = OracleHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
        // Else
        // ExecuteXmlReaderTypedParams = OracleHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName)
        // End If
        // End Function
        #endregion

    }

    // OracleHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    // ability to discover parameters for stored procedures at run-time.
    public sealed class OracleHelperParameterCache
    {

        #region "private methods, variables, and constructors"


        // Since this class provides only static methods, make the default constructor private to prevent
        // instances from being created with "new OracleHelperParameterCache()".
        private OracleHelperParameterCache()
        {
        }
        // New

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        // resolve at run time the appropriate set of OracleParameters for a stored procedure
        // Parameters:
        // - connectionString - a valid connection string for a OracleConnection
        // - spName - the name of the stored procedure
        // - includeReturnValueParameter - whether or not to include their return value parameter>
        // Returns: OracleParameter()
        private static OracleParameter[] DiscoverSpParameterSet(OracleConnection connection, string spName, bool includeReturnValueParameter, params object[] parameterValues)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");
            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");
            OracleCommand cmd = new OracleCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter[] discoveredParameters;
            connection.Open();
            OracleCommandBuilder.DeriveParameters(cmd);
            connection.Close();
            if (!includeReturnValueParameter)
            {
                //cmd.Parameters.RemoveAt(0)
            }

            discoveredParameters = new OracleParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value

            foreach (OracleParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }

            return discoveredParameters;

        }
        // DiscoverSpParameterSet

        // Deep copy of cached OracleParameter array
        private static OracleParameter[] CloneParameters(OracleParameter[] originalParameters)
        {

            int i;
            int j = originalParameters.Length - 1;
            OracleParameter[] clonedParameters = new OracleParameter[j + 1];

            for (i = 0; i <= j; i++)
            {
                clonedParameters[i] = (OracleParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }
        // CloneParameters

        #endregion

        #region "caching functions"

        // add parameter array to the cache
        // Parameters
        // -connectionString - a valid connection string for a OracleConnection
        // -commandText - the stored procedure name or T-Oracle command
        // -commandParameters - an array of OracleParamters to be cached
        public static void CacheParameterSet(string connectionString, string commandText, params OracleParameter[] commandParameters)
        {
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((commandText == null || commandText.Length == 0))
                throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }
        // CacheParameterSet

        // retrieve a parameter array from the cache
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -commandText - the stored procedure name or T-Oracle command
        // Returns: An array of OracleParamters
        public static OracleParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            if ((commandText == null || commandText.Length == 0))
                throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;
            OracleParameter[] cachedParameters = (OracleParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }
        // GetCachedParameterSet

        #endregion

        #region "Parameter Discovery Functions"
        // Retrieves the set of OracleParameters appropriate for the stored procedure.
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // Returns: An array of OracleParameters
        public static OracleParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }
        // GetSpParameterSet

        // Retrieves the set of OracleParameters appropriate for the stored procedure.
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connectionString - a valid connection string for a OracleConnection
        // -spName - the name of the stored procedure
        // -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results
        // Returns: An array of OracleParameters
        public static OracleParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            OracleParameter[] functionReturnValue = null;
            if ((connectionString == null || connectionString.Length == 0))
                throw new ArgumentNullException("connectionString");
            OracleConnection connection = new OracleConnection();
            try
            {
                connection = new OracleConnection(connectionString);
                functionReturnValue = GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
            finally
            {
                if ((connection != null))
                    connection.Dispose();
            }
            return functionReturnValue;
        }
        // GetSpParameterSet

        // Retrieves the set of OracleParameters appropriate for the stored procedure.
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connection - a valid OracleConnection object
        // -spName - the name of the stored procedure
        // -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results
        // Returns: An array of OracleParameters
        public static OracleParameter[] GetSpParameterSet(OracleConnection connection, string spName)
        {

            return GetSpParameterSet(connection, spName, false);
        }
        // GetSpParameterSet

        // Retrieves the set of OracleParameters appropriate for the stored procedure.
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connection - a valid OracleConnection object
        // -spName - the name of the stored procedure
        // -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results
        // Returns: An array of OracleParameters
        public static OracleParameter[] GetSpParameterSet(OracleConnection connection, string spName, bool includeReturnValueParameter)
        {
            OracleParameter[] functionReturnValue = null;
            if ((connection == null))
                throw new ArgumentNullException("connection");
            OracleConnection clonedConnection = new OracleConnection();
            try
            {
                clonedConnection = (OracleConnection)(((ICloneable)connection).Clone());
                functionReturnValue = GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
            finally
            {
                if ((clonedConnection != null))
                    clonedConnection.Dispose();
            }
            return functionReturnValue;
        }
        // GetSpParameterSet

        // Retrieves the set of OracleParameters appropriate for the stored procedure.
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connection - a valid OracleConnection object
        // -spName - the name of the stored procedure
        // -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results
        // Returns: An array of OracleParameters
        private static OracleParameter[] GetSpParameterSetInternal(OracleConnection connection, string spName, bool includeReturnValueParameter)
        {

            if ((connection == null))
                throw new ArgumentNullException("connection");

            OracleParameter[] cachedParameters;
            string hashKey;

            if ((spName == null || spName.Length == 0))
                throw new ArgumentNullException("spName");

            hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter == true ? ":include ReturnValue Parameter" : "").ToString();

            cachedParameters = (OracleParameter[])paramCache[hashKey];

            if ((cachedParameters == null))
            {
                OracleParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;

            }

            return CloneParameters(cachedParameters);

        }
        // GetSpParameterSet
        #endregion

    }
}
