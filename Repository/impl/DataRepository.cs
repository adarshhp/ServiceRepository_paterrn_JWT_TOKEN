using TVM.DbContexts;
using TVM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;



namespace TVM.Repository.impl
{
    public class DataRepository : IDataRepository
    {
        public readonly DataDbContext Context;
        private IConfiguration _config;
        public DataRepository(DataDbContext context, IConfiguration config)
        {
            Context = context;
            _config = config;
        }


        public Response PostLogin(Data data)
        {
            Response ff = new Response();
            try
            {
                var userExists = Context.Datas.Any(u => u.Id == data.Id);

                if (userExists)
                {
                /*  Context.Datas.Add(data);
                   Context.SaveChanges();*/

                    ff.Code = 200;
                    ff.Description = "OK";
                    string userIdAsString = data.Id.ToString();
                    ff.Jwt_token = GenerateJwtToken(userIdAsString); 
                }
                else
                {
                    ff.Code = 404;
                    ff.Description = "User not found";
                }
            }
            catch (Exception ex)
            {
                ff.Code = 520;
                ff.Description = "Failed: " + ex.Message;
            }

            return ff;
        }
        public async Task<IEnumerable<Data>> GetData()
        {
            var Datas = await Context.Datas.ToListAsync();
            return Datas;
        }


        //updating user values based on the user_id supplied
        public Response PostData(Data data)
        {
            Response response = new Response();
            try
            {
                var datafromdb = Context.Datas.Find(data.Id);

                    datafromdb.Email = data.Email;
                    datafromdb.Gender= data.Gender;
                    datafromdb.Firstname = data.Firstname;
                    datafromdb.Lastname = data.Lastname;
                    datafromdb.Marital_status = data.Marital_status;
                    datafromdb.Pancard_no = data.Pancard_no;
                    datafromdb.Temp_address = data.Temp_address;
                    datafromdb.Aadhar_no    = data.Aadhar_no;
                    datafromdb.Pnt_address  = data.Pnt_address;
                    datafromdb.Phone_no    = data.Phone_no;
                datafromdb.Blood_group = data.Blood_group;
                   



                Context.SaveChanges();

                response.Code = 200;
                response.Description = "OK";
                return response;
            }
            catch 
            {
                response.Code = 520;
                response.Description = "failed";
                return response;
            }
        }

        public string GenerateJwtToken(string userId)
        {
              var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var secToken = new JwtSecurityToken(_config["Jwt:Issuer"],
                                                _config["Jwt:Issuer"],
                                                null,
                                                expires: DateTime.Now.AddMinutes(60),
                                                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(secToken);
            return token;
        }


        public async Task<ActionResult<Data>> GetData(int id)
        {
            var data = await Context.Datas.FindAsync(id);


            return new JsonResult(data);
        }


    }
}
