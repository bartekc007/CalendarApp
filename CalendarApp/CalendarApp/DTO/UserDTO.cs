using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarApp.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public UserDTO() { }
        public List<string> friendsID { get; set; }
        public string EventId { get; set; }
        public UserDTO(string id, string username,List<string> friendsId)
        {
            this.Id = id;
            this.UserName = username;
            this.friendsID = friendsId;
        }
        public UserDTO(string id, string username)
        {
            this.Id = id;
            this.UserName = username;
            this.friendsID = new List<string>();
        }

    }
}