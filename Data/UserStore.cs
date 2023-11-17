﻿using api_mod9.Models;
using api_mod9.Models.Dto;

namespace api_mod9.Data
{
    public static class UserStore
    {
        public static List<UserDTO> userList = new List<UserDTO>
    {
    new UserDTO{ Username = "admin", Password = "123" },
    };
    }
}