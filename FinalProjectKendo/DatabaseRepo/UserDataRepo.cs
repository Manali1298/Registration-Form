using FinalProjectKendo.DataContext;
using FinalProjectKendo.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.DatabaseRepo
{
    public interface IUserProfileRepo
    {
        int RegisterUser(UserData model);
        bool signIn(string u_email, string u_password);
        int GetUserId(string u_email);


    }
        public class UserDataRepo: IUserProfileRepo
    {
        ApplicationDbContext db;
        NpgsqlCommand cmd = null;
        public UserDataRepo()
        {
            db = new ApplicationDbContext();
        }

        //code for Registration
        public int RegisterUser(UserData model)
        {
            

            String connections = ("Server=localhost;Port=5432;Database=RegisterDB;User Id=postgres;Password=datapostgre");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            connection.Open();
            string query = "INSERT INTO public.mg_datauser(u_name, u_email,u_mob, u_password) VALUES(@u_name,@u_email,@u_mob,@u_password)";
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_name", model.u_name);
            cmd.Parameters.AddWithValue("@u_email", model.u_email);
            cmd.Parameters.AddWithValue("@u_mob", model.u_mob);

            cmd.Parameters.AddWithValue("@u_password", model.u_password);
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return i;
        }
        //code for login
        public bool signIn(string u_email, string u_password)
        {
            String connections = ("Server=localhost;Port=5432;Database=RegisterDB;User Id=postgres;Password=datapostgre");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
        
            string query = "SELECT u_email,u_password FROM public.mg_datauser where u_email=@u_email AND u_password=@u_password";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_email", u_email);
            cmd.Parameters.AddWithValue("@u_password", u_password);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public List<stateData> GetAllstate()
        {
            var state = db.allstate.ToList();
            return state;

        }
        public List<cityData> GetAllcity(int state_id)
        {
           
            var city = db.allcity.Where(x => x.state_id.Equals(state_id)).ToList();
            return city;
            //List<cityData> obj=new List<cityData>();
            //var citydata = (
            //from city in db.allcity.Where(x => x.state_id.Equals(state_id)).ToList()
            //select new
            //{
            //    city_id = city.city_id,
            //    city_name = city.city_name
            //}).ToList();

            //foreach (var cityobj in citydata)
            //{
            //    var cityObj = new cityData();
            //    cityObj.city_id = cityobj.city_id;
            //    cityObj.city_name = cityObj.city_name;
            //    obj.Add(cityObj);
            //}
            //return obj;
        }
        //public UserData GetUserDetails(int nid)
        //{
        //    UserData news = db.UserInfos.Where(x => x.u_id == nid).FirstOrDefault();
        //    return news;
        //}
        //public stateData Get_State(int  state_id)
        //{
        //    String connections = ("Server=localhost;Port=5432;Database=RegisterDB;User Id=postgres;Password=datapostgre");
        //    NpgsqlConnection connection = new NpgsqlConnection();
        //    connection.ConnectionString = connections;
        //    //SqlCommand com = new SqlCommand("Select * from mg_state ", connection);
        //    //com.Parameters.AddWithValue("@catid", country_id);
        //    //SqlDataAdapter da = new SqlDataAdapter(com);
        //    //DataSet ds = new DataSet();
        //    //da.Fill(ds);
        //    //return ds;

        //    stateData model = new stateData();
        //    string query = string.Format("SELECT state_name FROM public.mg_state where state_id={0}", state_id);
        //    connection.Open();
        //    cmd = new NpgsqlCommand(query, connection);

        //    try
        //    {
        //        NpgsqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                model.state_id = Convert.ToInt32(dr["state_id"]);
        //                model.state_name = dr["state_name"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return model;


        //}

        //Get all City
        public cityData Get_City(int state_id)
        {
            String connections = ("Server=localhost;Port=5432;Database=RegisterDB;User Id=postgres;Password=datapostgre");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            cityData model = new cityData();

            string query = "SELECT city_name FROM public.mg_city where state_id=@state_id";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);

            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.state_id = Convert.ToInt32(dr["state_id"]);
                        model.city_name = dr["city_name"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return model;

            //SqlCommand com = new SqlCommand("Select * from mg_city where State_id=@state_id", con);
            //com.Parameters.AddWithValue("@stateid", state_id);
            //SqlDataAdapter da = new SqlDataAdapter(com);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //return ds;
        }
        public int GetUserId(string u_email)
        {
            String connections = ("Server=localhost;Port=5432;Database=RegisterDB;User Id=postgres;Password=datapostgre");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
          
            int id = 0;
            string query = "SELECT  u_id FROM public.mg_datauser WHERE u_email=@u_email";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_email", u_email);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = Convert.ToInt32(dr["u_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

    }
}
