using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MACHINE_TEST_PRACTICE.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace MACHINE_TEST_PRACTICE.Controllers
{
    public class HomeController : Controller
    {

        string conn = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        string query = string.Empty;        
        SqlDataAdapter sqlDataAdapter = null;


        public ActionResult Index(UserModel userModel)
        {
            if(userModel.Id == 0)
            {
                ViewBag.UserDetailsList = GetUserDetails();
                return View();
            }
            else
            {
                ViewBag.UserDetailsList = GetUserDetails();
                using (sqlConnection = new SqlConnection(conn))
                {
                    query = $"SELECT * FROM users WHERE userId = {userModel.Id}";
                    sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable UserTable = new DataTable();
                    sqlDataAdapter.Fill(UserTable);

                    foreach (DataRow row in UserTable.Rows)
                    {
                        userModel = new UserModel()
                        {
                            Id = Convert.ToInt32(row["userId"]),
                            FullName = Convert.ToString(row["fullName"]),
                            Email = Convert.ToString(row["EMAIL"]),
                            MobNo = Convert.ToString(row["MOB_NO"]),
                            DateOfBirth = Convert.ToDateTime(row["DATEOFBIRTH"]),
                            Photo = Convert.ToString(row["photo"]),
                            CountryId = Convert.ToInt32(row["countryId"]),
                            StateId = Convert.ToInt32(row["stateId"]),
/*                            ProfessionalSkill = Convert.ToString(row["professional_skill"])
*/                        };
                    }
                    return View(userModel);

                }
            }
            
        }


        public List<UserModel> GetUserDetails()
        {
            List<UserModel> UserDetailList = new List<UserModel>();
            using(sqlConnection = new SqlConnection(conn))
            {
                query = "SELECT U.userId, U.fullName,U.EMAIL, U.MOB_NO, U.DATEOFBIRTH, U.photo, C.country_Name, S.state_Name , U.comm, U.atwup, U.dm, U.tm, U.sm, U.cr, U.lead, U.adapt  FROM users AS U \r\nINNER JOIN countries AS C ON U.countryId = C.countryId\r\nINNER JOIN states AS S ON U.stateId = S.stateId;";
                sqlCommand = new SqlCommand(query,sqlConnection);

                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable UserTable = new DataTable();
                sqlDataAdapter.Fill(UserTable);

                foreach(DataRow row in UserTable.Rows)
                {
                    UserModel user = new UserModel()
                    {
                        Id = Convert.ToInt32(row["userId"]),
                        FullName = Convert.ToString(row["fullName"]),
                        Email = Convert.ToString(row["EMAIL"]),
                        MobNo = Convert.ToString(row["MOB_NO"]),
                        DateOfBirth = Convert.ToDateTime(row["DATEOFBIRTH"]),
                        Photo = Convert.ToString(row["photo"]),
                        CountryName = Convert.ToString(row["country_Name"]),
                        StateName = Convert.ToString(row["state_Name"]),
                        comm = Convert.ToBoolean(row["comm"]),
                        atwup = Convert.ToBoolean(row["atwup"]),
                        dm = Convert.ToBoolean(row["dm"]),
                        tm = Convert.ToBoolean(row["tm"]),
                        sm = Convert.ToBoolean(row["sm"]),
                        cr = Convert.ToBoolean(row["cr"]),
                        lead = Convert.ToBoolean(row["lead"]),
                        adapt = Convert.ToBoolean(row["adapt"]),
                    };
                    UserDetailList.Add(user);
                }
            }
            return UserDetailList;
        }


        [HttpPost]
        public ActionResult AddOrUpdate(UserModel user)
        {            
            if (user.Id == 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                fileName += DateTime.Now.ToString("yymmssfff") + extension;
                user.Photo = "~/Content/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                user.ImageFile.SaveAs(fileName);

                string query = "INSERT INTO users (FullName, Email, MOB_NO, DateOfBirth, Photo, CountryId, StateId, professional_skill) " +
                                           "VALUES (@FullName, @Email, @MOB_NO, @DateOfBirth, @Photo, @CountryId, @StateId, @professional_skill)";
                using (sqlConnection = new SqlConnection(conn))
                {
                    sqlCommand = new SqlCommand(query,sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@FullName", user.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@MOB_NO", user.MobNo);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@Photo", user.Photo);
                    sqlCommand.Parameters.AddWithValue("@CountryId", user.CountryId);
                    sqlCommand.Parameters.AddWithValue("@StateId", user.StateId);
/*                    sqlCommand.Parameters.AddWithValue("@professional_skill", user.ProfessionalSkill);
*/
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                return RedirectToAction("Index");
            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                fileName += DateTime.Now.ToString("yymmssfff") + extension;
                user.Photo = "~/Content/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                user.ImageFile.SaveAs(fileName);

/*                query = $"UPDATE users SET fullName ='{user.FullName}', EMAIL = '{user.Email}', MOB_NO = '{user.MobNo}', DATEOFBIRTH = '02-06-2001', countryId = {user.CountryId}, stateId = {user.StateId}, professional_skill = '{user.ProfessionalSkill}' WHERE userId = {user.Id}";
*/
                using (sqlConnection = new SqlConnection(conn))
                {
                    sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                return RedirectToAction("Index");
            }
        }
    }
}