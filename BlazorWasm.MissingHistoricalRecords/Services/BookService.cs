using BlazorWasm.MissingHistoricalRecords.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorWasm.MissingHistoricalRecords.Services;

public class BookService
{
    private readonly HttpClient _httpClient;
    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BookResponseModel?> GetBookList(int pageNo = 1, int pageSize = 5)
    {
        var bookList = await GetData<BookModel>("book-json/books.json");
        var count = bookList?.Count();
        var totalPageCount = count / pageSize;
        var result = count % pageSize;
        if (result > 0)
            totalPageCount++;
        var data = new BookResponseModel
        {
            Data = bookList.Skip((pageNo-1)*pageSize).Take(pageSize).ToList(),
            TotalPage = totalPageCount
        };
        return data;
    }
    public async Task<List<T>?> GetData<T>(string fileName)
    {
        return await _httpClient.GetFromJsonAsync<List<T>>(fileName);
    }
}
