using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    public SqlDataAdapter adpt;
    public SqlCommand cmd;
    public SqlConnection con;
    public string command;
    public string[] Parameters;
    public string[] Values;

    public DAL()
    {
        con = new SqlConnection("Data Source=.;Initial Catalog=ERP;Integrated Security=True");
    }

    public DAL(string commandText)
    {
        con = new SqlConnection("Data Source=.;Initial Catalog=ERP;Integrated Security=True");
        command = commandText;
    }

    public DataTable GetData()
    {
        DataTable dtData = new DataTable();
        adpt = new SqlDataAdapter(command, con);
        adpt.Fill(dtData);

        return dtData;
    }

    public DataTable GetData(string[] parameters, string[] values)
    {
        int index = 0;
        DataTable dtData = new DataTable();
        adpt = new SqlDataAdapter(command, con);

        if (parameters.Length != values.Length)
        {
            throw new Exception("Paramters and values count not match");
        }

        foreach (string parameter in parameters)
        {
            adpt.SelectCommand.Parameters.Add(new SqlParameter(parameter, values[index]));
            index++;
        }

        adpt.Fill(dtData);
        return dtData;
    }


    public bool ExecuteQuery()
    {
        cmd = new SqlCommand(command, con);

        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool ExecuteQuery(string[] parameters, string[] values)
    {
        int index = 0;
        cmd = new SqlCommand(command, con);

        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }

        if (parameters.Length != values.Length)
        {
            throw new Exception("Paramters and values count not match");
        }

        foreach (string parameter in parameters)
        {
            cmd.Parameters.Add(new SqlParameter(parameter, values[index]));
            index++;
        }

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool ExecuteSP(string[] parameters, string[] values)
    {
        int index = 0;
        cmd = new SqlCommand(command, con);

        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }

        if (parameters.Length != values.Length)
        {
            throw new Exception("Paramters and values count not match");
        }

        foreach (string parameter in parameters)
        {
            cmd.Parameters.Add(new SqlParameter(parameter, values[index]));
            index++;
        }

        cmd.CommandType = CommandType.StoredProcedure;
        return cmd.ExecuteNonQuery() > 0;
    }
}