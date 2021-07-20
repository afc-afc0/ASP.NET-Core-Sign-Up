using Dapper;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using BC = BCrypt.Net.BCrypt;
using static DataLibrary.DataAccess.SqlDataAccess;

namespace DataLibrary.Logic
{
    public static class UserProcessor
    {    
        public static int InsertUser(string userName, string email, string password)
        {
            UserModel userModel = new UserModel()
            {
                userid = userName,
                email = email,
                passwordhash = BC.EnhancedHashPassword(password)
            };

            string sqlQuery = $@"insert into users (userId, email, passwordhash) values (@userid, @email, @passwordhash);";
            return InsertData(sqlQuery, userModel);
        }


        #region GET METHODS
        public static List<UserModel> GetUsers()
        {
            string sqlQuery = "select userid, email, passwordhash from users";

            List<UserModel> result = LoadData<UserModel>(sqlQuery);
            return result.Count > 0 ? result : null;
        }

        public static UserModel GetUserModelByEmail(string email)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("email", email);
            return GetModel(pair);
        }

        public static UserModel GetUserModelByUserId(string userId)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("userid", userId);
            return GetModel(pair);
        }

        private static UserModel GetModel(KeyValuePair<string, string> pair)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{pair.Key}", pair.Value);
            string sqlQuery = $"select userid, email, passwordhash from users where {pair.Key} = @{pair.Key}";

            UserModel model = FindData<UserModel>(sqlQuery, parameters);
            PrintUserModel(model);

            return model ?? null;
        }
        #endregion


        #region VALIDATION
        public static bool ValidatePasswordWithUserId(string userid, string password)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("userid" , userid);
            return ValidatePassword(pair, password);
        }

        public static bool ValidatePasswordWithEmail(string email, string password)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("email", email);
            return ValidatePassword(pair, password);
        }

        private static bool ValidatePassword(KeyValuePair<string, string> pair, string password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{pair.Key}", pair.Value);//if key is email gives us : "@email" , <value>
            string sqlQuery = $"select passwordhash from users where {pair.Key} = @{pair.Key}";

            UserModel model = FindData<UserModel>(sqlQuery, parameters);

            return model != null && BC.EnhancedVerify(password, model.passwordhash);
        }
        #endregion

        public static void PrintUserModel(UserModel model)
        {
            Console.WriteLine("model id = " + model.userid + ", model email = " + model.email + ", model password = " + model.passwordhash);
        }

        public static void PrintUserModels(List<UserModel> models)
        {
            foreach (UserModel model in models)
                PrintUserModel(model);
        }
    }
}
