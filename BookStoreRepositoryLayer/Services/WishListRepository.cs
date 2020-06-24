//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Contains Method which add wish list details into the database 
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
    public class WishListRepository : IWishListRepository
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public WishListRepository(IConfiguration configuration)
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
        /// <returns>If Data Fetched Successfully return Response Data else null or Exception</returns>
        public async Task<WishListsResponnse> GetListOfWishList(int userID)
        {
            try
            {
                WishListsResponnse wishLists = null;
                List<WishListResponse2> wishList = new List<WishListResponse2>();
                List<BookResponse> bookList = new List<BookResponse>();
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("GetListOfWishListByUserID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        WishListResponse2 wish = new WishListResponse2
                        {
                            WishListID = Convert.ToInt32(dataReader["WishListID"]),
                            Name = dataReader["Name"].ToString()
                        };
                        bookList = await GetListOfBooksInWishList(wish.WishListID);
                        wish.Books = bookList;
                        wishList.Add(wish);
                    }
                    wishLists = new WishListsResponnse
                    {
                        WishLists = wishList
                    };
                };
                return wishLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// It Fetches List of Books in a Specific Wish List from the database
        /// </summary>
        /// <param name="wishListID">WishList-ID</param>
        /// <returns>If Data Fetched Successfully return Response Data else null or Exception</returns>
        public async Task<List<BookResponse>> GetListOfBooksInWishList(int wishListID)
        {
            try
            {
                List<BookResponse> bookList = new List<BookResponse>();
                SQLConnection();
                using(SqlCommand cmd = new SqlCommand("GetListOfBooksByWishListID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WishListID", wishListID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    bookList = ListBookResponseModel(dataReader);
                }
                return bookList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Create New Wish List in the database
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="wishList">Wish List Data</param>
        /// <returns>If data added successfully return Response Data else null or Exception</returns>
        public async Task<WishListResponse> CreateNewWishList(int userID, WishListRequest wishList)
        {
            try
            {
                WishListResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("CreateNewWishList", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Name", wishList.Name);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        responseData = new WishListResponse
                        {
                            WishListID = Convert.ToInt32(dataReader["WishListID"]),
                            Name = dataReader["Name"].ToString()
                        };
                    }
                };
                return responseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Book into Wish List
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="wishList">Wish List Data</param>
        /// <returns>If Data Added Successfully return Response Data else null or Bad Request</returns>
        public async Task<BookResponse> AddBookIntoWishList(int userID, WishListBookRequest wishListBook)
        {
            try
            {
                BookResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("AddBookIntoWishList", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@WishListID", wishListBook.WishListID);
                    cmd.Parameters.AddWithValue("@BookID", wishListBook.BookID);

                    conn.Open();
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    responseData = BookResponseModel(dataReader);
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete Book From the Wish List in the database
        /// </summary>
        /// <param name="userID">User-ID</param>
        /// <param name="wishList">Wish List Data</param>
        /// <returns>If Data Deleted Successfull return true else false or Exception</returns>
        public async Task<bool> DeleteBookFromWishList(int userID, WishListBookRequest wishListBook)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("DeleteBookFromWishList", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@WishListID", wishListBook.WishListID);
                    cmd.Parameters.AddWithValue("@BookID", wishListBook.BookID);

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
        /// Book Response Method
        /// </summary>
        /// <param name="dataReader">Sql Data Reader</param>
        /// <returns>It return Book Response Data</returns>
        private BookResponse BookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                BookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new BookResponse
                    {
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
        private List<BookResponse> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<BookResponse> bookList = new List<BookResponse>();
                BookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new BookResponse
                    {
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
