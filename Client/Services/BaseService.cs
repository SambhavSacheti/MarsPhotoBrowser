using System.Net.Http;
using Data.Models;

namespace Client.Services
{
    public abstract record BaseService( MarsPhotoBrowserConfiguration MarsPhotoBrowserConfig,HttpClient httpClient);
}