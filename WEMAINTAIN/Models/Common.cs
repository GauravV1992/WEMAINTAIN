using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace WEMAINTAIN.Models
{
    public static class Common
    {
        public static string GetMMDDYYYDate(string date)
        {
            string[] dateSplit = date.Split('/');
            return dateSplit[1] + "/" + dateSplit[0] + "/" + dateSplit[2];

        }
        public static List<string> GetErrorListFromModelState
                                              (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
        public static string GetAccessToken(HttpContext context)
        {
            string accessToken = string.Empty;
            if (context.Request.Cookies["access_token"] != null)
            {
                accessToken = context.Request.Cookies["access_token"];
            }
            return accessToken;
            //var id = context.User.Claims.First(c => c.Type == "Id");
            //var email = context.User.Claims.First(c => c.Type == "Email");
            //var mobileNo = context.User.Claims.First(c => c.Type == "MobileNo");
            //var Address = context.User.Claims.First(c => c.Type == "Address");
            //return new LoginResponse
            //{
            //    Id = Convert.ToInt16(id.Value),
            //    Email = email.Value,
            //    MobileNo = mobileNo.Value,
            //    Address = Address.Value,
            //    accessToken = toekn
            //};
        }
        public static string GetExtention(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            return fileInfo.Extension;
        }
        public static void UplaodFile(IFormFile file, string folderName, string fileName)
        {

            var FolderName = @"wwwroot\" + folderName + "";
            var PathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileNameWithPath = Path.Combine(PathToSave, fileName + fileInfo.Extension);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

       
    }
}
