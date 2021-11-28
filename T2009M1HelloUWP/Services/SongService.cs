using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T2009M1HelloUWP.Entities;

namespace T2009M1HelloUWP.Services
{
    public class SongService
    {
        private const string ApiBaseUrl = "https://music-i-like.herokuapp.com";
        private const string ApiSongPath = "/api/v1/songs";
        
        public async Task<List<Song>> GetLatestSongAsync() {
            Debug.WriteLine("Start getting songs from api.");
            List<Song> result = new List<Song>();
            try
            {
                HttpClient httpClient = new HttpClient();
                Debug.WriteLine("Sending request.");
                var requestConnection =
                    await httpClient.GetAsync(ApiBaseUrl + ApiSongPath);                                                                                             // chờ phản hồi, lấy kết quả
                Debug.WriteLine("Got connection. " + requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Got data.");
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(content);                   
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Got error.");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.Data);
            }
            Debug.WriteLine("Return result.");
            return result;
        }

        public async Task<Song> CreateSongAsync(Song song)
        {
            throw new NotImplementedException();
        }
    }
}
