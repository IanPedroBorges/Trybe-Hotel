using System.Net.Http;
using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
         private readonly HttpClient _client;
        public GeoService(HttpClient client)
        {
            _client = client;
        }

        // 11. Desenvolva o endpoint GET /geo/status
        public async Task<object> GetGeoStatus()
        {
            var client = _client;
            var request = new HttpRequestMessage(HttpMethod.Get, "https://nominatim.openstreetmap.org/status.php?format=json");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "aspnet-user-agent");
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<object>();
                return result!;
            }
            return null!;

        }
        
        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "aspnet-user-agent");
            var response = await _client.SendAsync(request);
            if(response.IsSuccessStatusCode){
                var result = await response.Content.ReadFromJsonAsync<List<GeoDtoResponse>>();
                var responseDto = new GeoDtoResponse();
                foreach (var item in result!)
                {
                    responseDto.lat = item.lat;
                    responseDto.lon = item.lon;
                }
                return responseDto;
            }
            return default!;
             
        }

        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var myGeo = await GetGeoLocation(geoDto);
            var Hotels = repository.GetHotels();
            List<GeoDtoHotelResponse> myListGeo = new List<GeoDtoHotelResponse>();
            foreach (var hotel in Hotels)
            {
                var hotelGeo =  await GetGeoLocation(new GeoDto { Address = hotel.address, City = hotel.cityName, State = hotel.state });
                myListGeo.Add(new GeoDtoHotelResponse(){
                    Address = hotel.address,
                    CityName = hotel.cityName,
                    HotelId = hotel.hotelId,
                    Name = hotel.name,
                    State = hotel.state,
                    Distance = CalculateDistance(myGeo.lat!, myGeo.lon!, hotelGeo.lat!, hotelGeo.lon!)
                });
            }
            return myListGeo;
        }

       

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree) {
            return degree * Math.PI / 180;
        }

    }
}