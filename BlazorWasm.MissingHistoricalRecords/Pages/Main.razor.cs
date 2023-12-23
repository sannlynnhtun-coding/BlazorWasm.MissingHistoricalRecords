using Microsoft.JSInterop;
using static MudBlazor.CategoryTypes;

namespace BlazorWasm.MissingHistoricalRecords.Pages;

public partial class Main
{
    private BookResponseModel? _data { get; set; }
    private BookContentResponseModel? _bookDetail { get; set; }
    private BookModel? Book { get; set; }
    private EnumFormType _formType = EnumFormType.List;
    public int spacing { get; set; } = 2;
    private int _pageNo { get; set; } = 1;
    private int? _totalPage { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //_data = await _service.GetBookList();
            //StateHasChanged();
            //await LoadJavaScript();
            await PageBookChanged(1);
        }
    }

    private async Task GoToContent(BookModel item)
    {
        Book = item;
        _bookDetail = await _service.BookDetail(item);
        _formType = EnumFormType.Detail;
        _totalPage = CalculatePageNo(_bookDetail.TotalPage);
        StateHasChanged();
    }

    private async Task PageChanged(int pageNo)
    {
        if (Book is null) return;
        _pageNo = pageNo;
        _bookDetail = await _service.BookDetail(Book, pageNo);
        StateHasChanged();
    }

    private int? CalculatePageNo(int? contentCount)
    {
        int? result = contentCount / 2;
        if (contentCount % 2 > 0)
            result++;
        return result;
    }

    private async Task Back()
    {
        _data = await _service.GetBookList();
        _bookDetail = null;
        Book = null;
        _pageNo = 1;
        _formType = EnumFormType.List;
        StateHasChanged();
        await LoadJavaScript();
    }

    private async Task LoadJavaScript()
    {
        await Task.Delay(500);
        await JsRuntime.InvokeVoidAsync("scrollTop");
        await JsRuntime.InvokeVoidAsync("loadJs", "theme/js/main.js");
    }

    private async Task PageBookChanged(int pageNo)
    {
        _data = await _service.GetBookList(pageNo);
        StateHasChanged();
        await LoadJavaScript();
    }
}
