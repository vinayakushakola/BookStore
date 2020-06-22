//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Contains Method which add admin details into the database & Fetch
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using BookStoreRepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        private static readonly string _admin = "Admin";

        public AdminRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sql Connection
        /// </summary>
        private void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDBConnection");
            conn = new SqlConnection(sqlConnectionString);
        }

        /// <summary>
        /// Add admin details into the database
        /// </summary>
        /// <param name="adminDetails">Admin Registration Details</param>
        /// <returns>If data added successully, return response data else null or exception</returns>
        public async Task<AdminRegistrationResponse> AdminRegistration(AdminRegistrationRequest adminDetails)
        {
            try
            {
                AdminRegistrationResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("AddAdminDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", adminDetails.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", adminDetails.LastName);
                    cmd.Parameters.AddWithValue("@Email", adminDetails.Email);
                    cmd.Parameters.AddWithValue("@Password", adminDetails.Password);
                    cmd.Parameters.AddWithValue("@IsActive", true);
                    cmd.Parameters.AddWithValue("@UserRole", _admin);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        responseData = new AdminRegistrationResponse
                        {
                            AdminID = Convert.ToInt32(dataReader["AdminID"]),
                            FirstName = dataReader["FirstName"].ToString(),
                            LastName = dataReader["LastName"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                            UserRole = dataReader["UserRole"].ToString(),
                            CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
                            ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"])
                        };
                    }
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
