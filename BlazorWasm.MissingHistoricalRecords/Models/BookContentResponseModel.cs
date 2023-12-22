namespace BlazorWasm.MissingHistoricalRecords.Models;

public class BookContentResponseModel
{
    public BookModel Book { get; set; } = null!;
    public List<BookContentModel> BookContent { get; set; } = null!;
    public int TotalPage { get; set; }
}
