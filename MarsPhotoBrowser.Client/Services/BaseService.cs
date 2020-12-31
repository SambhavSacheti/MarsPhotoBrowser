using System.Net.Http;
using MarsPhotoBrowser.Data.Models;

namespace MarsPhotoBrowser.Client.Services
{
    public abstract record BaseService( MarsPhotoBrowserConfiguration MarsPhotoBrowserConfig,HttpClient httpClient);
}