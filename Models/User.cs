using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class User
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public int? RoleId { get; set; } // 0 is user 1 is shipper 2 is admin
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserImg { get; set; }

        public User()
        {

        }

        public User(string? userId, string? name, int? roleId, string? address, string? phone, string? email, string? password, string? userImg)
        {
            UserId = userId;
            Name = name;
            RoleId = roleId;
            Address = address;
            Phone = phone;
            Email = email;
            Password = password;
            UserImg = userImg;
        }

        public override string? ToString()
        {
            return $"UserId = {UserId}, Name = {Name}, \n" +
                $"RoleId = {RoleId}, Address = {Address}, \n" +
                $"Phone = {Phone}, Email = {Email}";
        }
    }
}
