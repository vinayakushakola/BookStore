//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Contains Method which add user details into the database 
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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        private static readonly string _user = "User";

        public UserRepository(IConfiguration configuration)
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
        /// Add User Details into the database
        /// </summary>
        /// <param name="userDetails">User Registration Request</param>
        /// <returns>If data added successully, return response data else null or exception</returns>
        public async Task<RegistrationResponse> UserRegistration(RegistrationRequest userDetails)
        {
            try
            {
                RegistrationResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("AddUserDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", userDetails.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", userDetails.LastName);
                    cmd.Parameters.AddWithValue("@Mobile", userDetails.Mobile);
                    cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                    cmd.Parameters.AddWithValue("@Password", userDetails.Password);
                    cmd.Parameters.AddWithValue("@IsActive", true);
                    cmd.Parameters.AddWithValue("@UserRole", _user);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        responseData = new RegistrationResponse
                        {
                            UserID = Convert.ToInt32(dataReader["UserID"]),
                            FirstName = dataReader["FirstName"].ToString(),
                            LastName = dataReader["LastName"].ToString(),
                            Mobile = dataReader["Mobile"].ToString(),
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

        public Task<RegistrationResponse> UserLogin(LoginRequest loginDetails)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResponse> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(ResetPasswordRequest resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
