using Motoroal.CodingExercise.Repository.Interfaces;
using Motoroal.CodingExercise.Repository.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Motoroal.CodingExercise.Repository.Repos
{
    public class MarsRoverPhotoRepo : IMarsRoverPhotoRepo
    {        
        public async Task<List<MarsRoverPhoto>> GetMarsRoverPhotosByDate(string strEarthDate)
        {
            try
            {
               UriBuilder builder = new UriBuilder("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos");
                builder.Query = $"earth_date={strEarthDate}&api_key={Constants.APIKey}";

                using (var httpClient = new HttpClient())
                {
                    var httpResponseMessage = httpClient.GetAsync(builder.Uri).Result;

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var str = await httpResponseMessage.Content.ReadAsStringAsync();
                        var photo = await httpResponseMessage.Content.ReadAsAsync<Photo>();

                        if(photo != null)
                        {
                            return photo.photos;
                        }                        
                    }
                    else
                    {
                        throw new Exception($"#GetMarsRoverPhotosByDate :: NASA API to get Mars Photos is not success for Earh Date: {strEarthDate}. Response Message: {httpResponseMessage.ToString()} ");
                    }
                    return new List<MarsRoverPhoto>();
                }                
            }
            catch(Exception ex)
            {
                throw;
            }            
        }
    }
}
