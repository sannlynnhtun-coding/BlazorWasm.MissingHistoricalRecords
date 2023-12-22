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
            Data = bookList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
            TotalPage = totalPageCount
        };
        return data;
    }

    public async Task<BookContentResponseModel?> BookDetail(BookModel requestModel, int pageNo = 1, int pageSize = 2)
    {
        try
        {
            var book = await GetData<BookModel>($"book-json/books.json");
            var contentList = await GetData<BookContentModel>($"book-json/{requestModel.BookId}.json");
            var count = contentList?.Count();
            var totalPageCount = count / pageSize;
            var result = count % pageSize;
            if (result > 0)
                totalPageCount++;
            var model = new BookContentResponseModel
            {
                Book = book!.FirstOrDefault(x => x.BookId == requestModel.BookId)!,
                BookContent = contentList!
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                TotalPage = Convert.ToInt32(totalPageCount)
            };
            return model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public async Task<List<T>?> GetData<T>(string fileName)
    {
        return await _httpClient.GetFromJsonAsync<List<T>>(fileName);
    }
}
