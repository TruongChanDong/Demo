using Demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public partial class ListViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<User> _userList = new();

        [ObservableProperty]
        public string? _message;

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:8080/"),
        };

        [RelayCommand]
        public async Task GetListItem()
        {
            var users = new List<User>();
            try
            {
                users = await httpClient.GetFromJsonAsync<List<User>>("user");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                users = null;
            }
            if (users != null)
            {
                UserList = new ObservableCollection<User>(users);
                Message = $"Succesfully get {users.Count} users !!";
            }
        }

    }
}
