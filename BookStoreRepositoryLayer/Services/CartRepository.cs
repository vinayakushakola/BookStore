//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Contains Method which add cart details into the database 
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using BookStoreRepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Services
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public CartRepository(IConfiguration configuration)
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
        /// Fetch List of Books from the Database
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <returns>If Data Fetched Successfull return Response Data else null or Exception</returns>
        public async Task<List<CartBookResponse>> GetListOfBooksInCart(int userID)
        {
            try
            {
                List<CartBookResponse> bookList = null;
                SQLConnection();
                bookList = new List<CartBookResponse>();
                using (SqlCommand cmd = new SqlCommand("GetListOfBooksInCart", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    bookList = ListBookResponseModel(dataReader);
                };
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Book into Cart
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Added Successfully return Response Data else null or Bad Request</returns>
        public async Task<CartBookResponse> AddBookIntoCart(int userID, CartRequest cart)
        {
            try
            {
                CartBookResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("AddBookIntoCart", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BookID", cart.BookID);
                    cmd.Parameters.AddWithValue("@IsUsed", false);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    responseData = BookResponseModel(dataReader);
                };
                return responseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Book From the Cart in the database
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Deleted Successfull return true else false or Exception</returns>
        public async Task<bool> DeleteBookFromCart(int userID, CartRequest cart)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("DeleteBookFromCart", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BookID", cart.BookID);

                    conn.Open();
                    int count = await cmd.ExecuteNonQueryAsync();
                    if (count >= 0)
                    {
                        return true;
                    }
                };
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Details of Purchase
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="purchase">Purchase Data</param>
        /// <returns>If Data added successfully return Response Data else null or Exception</returns>
        public async Task<PurchaseResponse> Purchase(int userID, int cartID, PurchaseRequest purchase)
        {
            try
            {
                PurchaseResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("AddPurchaseDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CartID", cartID);
                    cmd.Parameters.AddWithValue("@IsUsed", true);
                    cmd.Parameters.AddWithValue("@BookID", purchase.BookID);
                    cmd.Parameters.AddWithValue("@Address", purchase.Address);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        responseData = new PurchaseResponse
                        {
                            PurchaseID = Convert.ToInt32(dataReader["PurchaseID"]),
                            BookID = Convert.ToInt32(dataReader["BookID"]),
                            Address = dataReader["Address"].ToString()
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

        /// <summary>
        /// Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return Book Response Data</returns>
        public static CartBookResponse BookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                CartBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new CartBookResponse
                    {
                        CartID = Convert.ToInt32(dataReader["CartID"]),
                        BookID = Convert.ToInt32(dataReader["BookID"]),
                        Name = dataReader["Name"].ToString(),
                        Author = dataReader["Author"].ToString(),
                        Language = dataReader["Language"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        ISBN = dataReader["ISBN"].ToString(),
                        Pages = dataReader["Pages"].ToString(),
                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// List of Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return List of Book Response Data</returns>
        private List<CartBookResponse> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<CartBookResponse> bookList = new List<CartBookResponse>();
                CartBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new CartBookResponse
                    {
                        CartID = Convert.ToInt32(dataReader["CartID"]),
                        BookID = Convert.ToInt32(dataReader["BookID"]),
                        Name = dataReader["Name"].ToString(),
                        Author = dataReader["Author"].ToString(),
                        Language = dataReader["Language"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        ISBN = dataReader["ISBN"].ToString(),
                        Pages = dataReader["Pages"].ToString(),
                    };
                    bookList.Add(responseData);
                }
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
