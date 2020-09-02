using Motoroal.CodingExercise.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motoroal.CodingExercise.Repository.Interfaces
{
    public interface IMarsRoverPhotoRepo
    {
        Task<List<MarsRoverPhoto>> GetMarsRoverPhotosByDate(string strEarthDate);
    }
}
