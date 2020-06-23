//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Contains Method which add book details into the database 
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
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public BookRepository(IConfiguration configuration)
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
        /// Get List of Books from the database
        /// </summary>
        /// <returns>If Data Fetched Successfully return Resonse Data else null or Exception</returns>
        public async Task<List<BookResponse>> GetListOfBooks()
        {
            try
            {
                List<BookResponse> bookList = null;
                SQLConnection();
                bookList = new List<BookResponse>();
                using (SqlCommand cmd = new SqlCommand("GetListOfBooks", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
        /// Add Book Details into the database
        /// </summary>
        /// <param name="adminID">Admin-ID</param>
        /// <param name="email">Email</param>
        /// <param name="book">Book Data</param>
        /// <returns>If data added successfull return Response Data else nnull or Exception</returns>
        public async Task<BookResponse> AddBook(int adminID, BookRequest bookDetails)
        {
            try
            {
                BookResponse responseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("AddBookDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminID", adminID);
                    cmd.Parameters.AddWithValue("@Name", bookDetails.Name);
                    cmd.Parameters.AddWithValue("@Author", bookDetails.Author);
                    cmd.Parameters.AddWithValue("@Language", bookDetails.Language);
                    cmd.Parameters.AddWithValue("@Category", bookDetails.Category);
                    cmd.Parameters.AddWithValue("@ISBN", bookDetails.ISBN);
                    cmd.Parameters.AddWithValue("@Pages", bookDetails.Pages);

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
            catch(Exception ex)
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
