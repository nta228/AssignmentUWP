using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T2009M1HelloUWP.Entities;
using Windows.Storage;

namespace T2009M1HelloUWP.Services
{
    public class AccountService
    {
        // Thực hiện đăng ký người dùng với api, nhận tham số là một đối tượng của lớp Account
        // Chuyển đối tượng về định dạng json sau đó gửi dữ liệu lên api với method POST.
        public async Task<bool> RegisterAsync(Account account) {
            try
            {
                var accountJson = Newtonsoft.Json.JsonConvert.SerializeObject(account);
                HttpClient httpClient = new HttpClient();
                Debug.WriteLine(accountJson);
                var httpContent = new StringContent(accountJson, Encoding.UTF8, "application/json");
                var requestConnection =
                    await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/accounts", httpContent); 
                    return true;
                }             
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<Credential> LoginAsync(LoginInformation loginInformation) {
            try
            {
                var accountJson = Newtonsoft.Json.JsonConvert.SerializeObject(loginInformation);
                HttpClient httpClient = new HttpClient();
                Console.WriteLine(accountJson);
                var httpContent = new StringContent(accountJson, Encoding.UTF8, "application/json");
                // thực hiện gửi dữ liệu sử dụng await, async
                var requestConnection =
                    await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/accounts/authentication", httpContent); 
                Console.WriteLine(requestConnection.StatusCode);                                                                                  
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    var credential = Newtonsoft.Json.JsonConvert.DeserializeObject<Credential>(content);
                    await WriteTokenToFile(content);                    
                    return credential;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
       
        public async Task<Account> GetInformationAsync() {
            var credential = await LoadAccessTokenFromFile();
            if (credential == null) {
                return null;
            }            
            try
            {               
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {credential.access_token}");
                var requestConnection =
                    await httpClient.GetAsync("https://music-i-like.herokuapp.com/api/v1/accounts"); 
                Console.WriteLine(requestConnection.StatusCode);                                                                                    
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    Debug.WriteLine("Getting information");
                    Debug.WriteLine(content);
                    var account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(content);
                    Console.WriteLine(content);
                    return account;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<Account> GetInformationByAccessTokenAsync(string accessToken)
        {            
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var requestConnection =
                    await httpClient.GetAsync("https://music-i-like.herokuapp.com/api/v1/accounts");
                Console.WriteLine(requestConnection.StatusCode);                                                                                 
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    var account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(content);
                    Console.WriteLine(content);
                    return account;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<Credential> LoadAccessTokenFromFile()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync("milt.txt");
                var fileContent = await FileIO.ReadTextAsync(storageFile);
                var credential = Newtonsoft.Json.JsonConvert.DeserializeObject<Credential>(fileContent);
                return credential;
            }
            catch (Exception e) {
                return null;
            }           
        }

        private async Task WriteTokenToFile(string content)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("milt.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, content);
        }       

        public void Update() { 

        }

  
    }
}
