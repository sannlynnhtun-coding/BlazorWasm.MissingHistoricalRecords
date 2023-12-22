namespace BlazorWasm.MissingHistoricalRecords.Models;

public class BookResponseModel
{
    public List<BookModel> Data { get; set; }
    public int? TotalPage { get; set; }
}