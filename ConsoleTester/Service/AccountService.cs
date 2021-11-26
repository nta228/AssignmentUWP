using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester.Service
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
                // Tạo dữ liệu thô để gửi đi.           
                Console.WriteLine(accountJson);
                // đóng gói dữ liệu, dán nhãn UTF8, dán format json.
                var httpContent = new StringContent(accountJson, Encoding.UTF8, "application/json");
                // thực hiện gửi dữ liệu sử dụng await, async
                var requestConnection =
                    await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/accounts", httpContent); // gặp vấn đề về độ trễ mạng, băng thông, đường truyền.
                                                                                                                   // chờ phản hồi, lấy kết quả
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.Created) {
                    //var content = await requestConnection.Content.ReadAsStringAsync();
                    //Console.WriteLine("Finish program");
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
                var accountJson = Newtonsoft.Json.JsonConvert.SerializeObject(loginInformation); // stringtify
                HttpClient httpClient = new HttpClient();
                // Tạo dữ liệu thô để gửi đi.           
                Console.WriteLine(accountJson);
                // đóng gói dữ liệu, dán nhãn UTF8, dán format json.
                var httpContent = new StringContent(accountJson, Encoding.UTF8, "application/json");
                // thực hiện gửi dữ liệu sử dụng await, async
                var requestConnection =
                    await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/accounts/authentication", httpContent); // gặp vấn đề về độ trễ mạng, băng thông, đường truyền.
                Console.WriteLine(requestConnection.StatusCode);                                                                                     // chờ phản hồi, lấy kết quả
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // lấy content dạng string.
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    // parse content sang lớp Credential.
                    var credential = Newtonsoft.Json.JsonConvert.DeserializeObject<Credential>(content);
                    //Console.WriteLine("Finish program");
                    Console.WriteLine(content);
                    return credential;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public void GetInformation() { 
        }

        public void Update() { 

        }
    }
}
