using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Motoroal.CodingExercise.Repository.Interfaces;
using Motoroal.CodingExercise.Repository.Models;

namespace Motorola.CodingExercise.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarsRoverPhotoController : ControllerBase
    {
        #region Instance Variables
        private readonly IMarsRoverPhotoRepo _marsRoverPhotoRepo;
        #endregion

        #region Contructors
        /// <summary>
        /// MarsRover Photo Controller
        /// </summary>
        /// <param name="marsRoverPhotoRepo"></param>
        public MarsRoverPhotoController(IMarsRoverPhotoRepo marsRoverPhotoRepo)
        {
            _marsRoverPhotoRepo = marsRoverPhotoRepo;
        }
        #endregion


        #region GetMarsRoverPhotos
        [HttpGet]        
        public async Task<IActionResult> GetMarsRoverPhotos()
        {
            try
            {
                var marsRoverPhotos = new List<MarsRoverPhoto>();

                string path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\dates.txt");

                StreamReader file = new StreamReader(path);
                string line;

                while ((line = await file.ReadLineAsync()) != null)
                {
                    DateTime earthDate;
                    if (DateTime.TryParse(line, out earthDate))
                    {
                        if (!DateTime.MinValue.Equals(earthDate))
                        {
                            string strEarthDate = earthDate.ToString("yyyy-MM-dd");
                            var photos = await _marsRoverPhotoRepo.GetMarsRoverPhotosByDate(strEarthDate);
                            marsRoverPhotos.AddRange(photos);
                            await DownloadFiles(strEarthDate, photos);
                        }                        
                    }
                }                
                return Ok(marsRoverPhotos);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private async Task DownloadFiles(string earthDate, List<MarsRoverPhoto> photos)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);                            
            }

            foreach (var photo in photos)
            {
                using (WebClient webClient = new WebClient())
                {
                    string imgFilePath = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}\{photo.id.ToString()}.JPG");
                    if (!System.IO.File.Exists(imgFilePath))
                    {
                        await Task.Run(() => { webClient.DownloadFileAsync(new Uri(photo.img_src), imgFilePath); });
                    }
                }
            }

        }
             
        #endregion
    }
}
